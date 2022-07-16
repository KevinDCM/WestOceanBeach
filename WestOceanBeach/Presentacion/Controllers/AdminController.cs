using Entities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Presentacion.Controllers
{
    public class AdminController : Controller
    {
        public IConfiguration Configuration { get; }
        HttpClient client = new HttpClient();
        private readonly IWebHostEnvironment _iwebhost;// get the project access

        public AdminController(IWebHostEnvironment _web) {
            _iwebhost = _web;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Home, Facilidades, Sobre Nosotros y Contacto 
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response1 = await client.GetAsync("https://localhost:44386/SitioGeneral/obtenerSitioGeneral");
            string resultado1 = await response1.Content.ReadAsStringAsync();
            SitioGeneral sitioGeneral1 = JsonConvert.DeserializeObject<SitioGeneral>(resultado1);

            ViewBag.Home = sitioGeneral1.HOME;
            ViewBag.CercaDe = sitioGeneral1.SOBRE_NOSOTROS;
            ViewBag.Contacto = sitioGeneral1.CONTACTO;
            ViewBag.comollegar = sitioGeneral1.COMO_LLEGAR;

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("https://localhost:44386/SitioGeneral/obtenerFacilidades");
            string resultado = await response.Content.ReadAsStringAsync();
            var sitioGeneral = JsonConvert.DeserializeObject<SitioGeneral>(resultado);

            ViewBag.facilidades = sitioGeneral.FACILIDADES;
            //-------------------------------------------Habitaciones Junior-----------------------------------------------------
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            var responseJ = await client.GetAsync("https://localhost:44386/Habitacion/Habitacion_Junior");
            string resultadoJJ = await responseJ.Content.ReadAsStringAsync();
            Habitacion Habitaciones_junior = JsonConvert.DeserializeObject<Habitacion>(resultadoJJ);

            ViewBag.Habitacion_Junior_descripcion = Habitaciones_junior.Descripcion;

            ViewBag.Habitacion_Junior_ruta_imagen = Habitaciones_junior.ruta_imagen;

            ViewBag.Habitacion_Junior_tarifa_diaria = Habitaciones_junior.TarifaDiaria;

            //-------------------------------------------Habitaciones Estandar-----------------------------------------------------
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            var response4 = await client.GetAsync("https://localhost:44386/Habitacion/Habitacion_Estandar");
            string resultado4 = await response4.Content.ReadAsStringAsync();
            Habitacion Habitaciones_estandar = JsonConvert.DeserializeObject<Habitacion>(resultado4);

            ViewBag.Habitacion_estandar_descripcion = Habitaciones_estandar.Descripcion;

            ViewBag.Habitacion_estandar_ruta_imagen = Habitaciones_estandar.ruta_imagen;

            ViewBag.Habitacion_estandar_tarifa_diaria = Habitaciones_estandar.TarifaDiaria;
            //-------------------------------------------Habitaciones Suite-----------------------------------------------------
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            var response5 = await client.GetAsync("https://localhost:44386/Habitacion/Habitacion_Suite");
            string resultado5 = await response5.Content.ReadAsStringAsync();
            Habitacion Habitaciones_suite = JsonConvert.DeserializeObject<Habitacion>(resultado5);

            ViewBag.Habitacion_suite_descripcion = Habitaciones_suite.Descripcion;

            ViewBag.Habitacion_suite_ruta_imagen = Habitaciones_suite.ruta_imagen;

            ViewBag.Habitacion_suite_tarifa_diaria = Habitaciones_suite.TarifaDiaria;

            //Imagen de HOME

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseImgHome = await client.GetAsync("https://localhost:44386/SitioGeneral/ObtenerImagenesHome");
            string responseImagenHomeContent = await responseImgHome.Content.ReadAsStringAsync();
            List<Imagenes> imagenes = JsonConvert.DeserializeObject<List<Imagenes>>(responseImagenHomeContent);
            @ViewBag.ImgHome = imagenes[0].Full_path;

            //---------------------temporadas
          
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTemporada = await client.GetAsync("https://localhost:44386/Temporada/obtenerTemporadas");
            string responsetemporada02 = await responseTemporada.Content.ReadAsStringAsync();
            string[] temporada = responsetemporada02.Split('#');
            @ViewBag.Tipo_temporada = temporada[1];
            @ViewBag.fecha_inicio = temporada[2];
            @ViewBag.fecha_final = temporada[3];//--Fecha inicio baja
            @ViewBag.tipo_temporada02 = temporada[6];
            @ViewBag.fecha_inicio_02 = temporada[7];//--
            @ViewBag.fecha_final_02 = temporada[8];//--

            return View();
        }

        [HttpGet]
        public async Task< IActionResult> estadoActualHabitaciones()
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            var response5 = await client.GetAsync("https://localhost:44386/Habitacion/estadoActualHabitacion");
            string resultado = await response5.Content.ReadAsStringAsync();
            List<Habitacion> estadoHabitaciones = JsonConvert.DeserializeObject<List<Habitacion>>(resultado);
            if (estadoHabitaciones is null)
            {
                @ViewBag.estadohotelactual = "No hay datos registrados";

            }
            else {
                return Ok(estadoHabitaciones);


            }
            return Ok(estadoHabitaciones);


        }




        [HttpPost]
        public async Task<ActionResult> EditarFacilidades(string facilidades)
        {
            SitioGeneral sitio = new SitioGeneral();

            sitio.FACILIDADES = facilidades;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response2 = await client.PostAsJsonAsync("https://localhost:44386/SitioGeneral/editarFacilidades", sitio);
            string resultado = await response2.Content.ReadAsStringAsync();
            var response3 = JsonConvert.DeserializeObject<string>(resultado);

            return Json(new { success = true, message = response3 });

        }// EditarFacilidades

        [HttpPost]
        public async Task<ActionResult> EditarHabitacion(string tarifa, string descripcion,string nombre)
        {
            Habitacion h = new Habitacion();

            h.Tarifa = tarifa;
            h.Descripcion = descripcion;
            h.TipoHabitacion = nombre;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response2 = await client.PostAsJsonAsync("https://localhost:44386/Habitacion/EditarHabitacion", h);
            string resultado = await response2.Content.ReadAsStringAsync();
            var response3 = JsonConvert.DeserializeObject<string>(resultado);

            return Json(new { success = true, message = response3 });

        }// editarHabitacion

        [HttpPost]
        public async Task<ActionResult> EditarComoLlegar(string comollegar)
        {
            SitioGeneral sitio = new SitioGeneral();

            sitio.COMO_LLEGAR = comollegar;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response2 = await client.PostAsJsonAsync("https://localhost:44386/SitioGeneral/EditarComoLlegar", sitio);
            string resultado = await response2.Content.ReadAsStringAsync();
            var response3 = JsonConvert.DeserializeObject<string>(resultado);

            return Json(new { success = true, message = response3 });
        }


        [HttpPost]
        public async Task<ActionResult> EditarSobreNosotros(string sobreNosotros) {

            SitioGeneral sitio = new SitioGeneral();

            sitio.SOBRE_NOSOTROS = sobreNosotros;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response2 = await client.PostAsJsonAsync("https://localhost:44386/SitioGeneral/editarSobreNosotros", sitio);
            string resultado = await response2.Content.ReadAsStringAsync();
            var response3 = JsonConvert.DeserializeObject<string>(resultado);

            return Json(new { success = true, message = response3 });

        }

        [HttpPost]
        public async Task<IActionResult> ChangeImageHome(IFormFile file) {

            string mensajeIH = "";

            if (file != null && file.Length > 0) {

                Imagenes ic= new Imagenes();
                var saveimg = Path.Combine(_iwebhost.WebRootPath, "imagenes", file.FileName);//la ruta de mi proyecto imagenes
                var stream = new FileStream(saveimg, FileMode.Create);// Creo en un nuevo archivo esa ruta
                await file.CopyToAsync(stream);// agrego
                string exten_img = Path.GetExtension(file.FileName);

                if(exten_img == ".jpg"|| exten_img == ".png" || exten_img == ".gif"){

                    ic.Name = "ImgHome"; //nombre imagen
                    ic.Full_path = "imagenes/"+file.FileName; // ruta imagen se guardo,aqui seria llamar a la base de datos y luego viewbag recupero el path y ya sabe donde esta.
                    mensajeIH= "Se cambio la imagen con exito";
           
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response2 = await client. PostAsJsonAsync("https://localhost:44386/SitioGeneral/editarRutaImgHome", ic);
                    string resultado = await response2.Content.ReadAsStringAsync();
                    var response3 = JsonConvert.DeserializeObject<string>(resultado);
                    mensajeIH = "Se ha cambiado la imagen con exito !";

                } else {
                    mensajeIH= "El formato de la imagen no se encuentra entre las permitidas (jpg,png,gif)";
                }

            } else {
                mensajeIH = "No se ha seleccionado ningùn archivo.";
            }

            return Json(new { success = true, message = mensajeIH });
            //Llamar Api pasar objeto imagenes 
        }//

        [HttpPost]
        public async Task<IActionResult> CambiarImgHabitacionEstandar(IFormFile file)
        {

            string mensajeIE = "";

            if (file != null && file.Length > 0)
            {

                Imagenes ic = new Imagenes();
                var saveimg = Path.Combine(_iwebhost.WebRootPath, "imagenes", file.FileName);//la ruta de mi proyecto imagenes
                var stream = new FileStream(saveimg, FileMode.Create);// Creo en un nuevo archivo esa ruta
                await file.CopyToAsync(stream);// agrego
                string exten_img = Path.GetExtension(file.FileName);

                if (exten_img == ".jpg" || exten_img == ".png" || exten_img == ".gif")
                {

                    ic.Name = "ImgEstandar"; //nombre imagen
                    ic.Full_path = "/imagenes/" + file.FileName; // ruta imagen se guardo,aqui seria llamar a la base de datos y luego viewbag recupero el path y ya sabe donde esta.
                    mensajeIE = "Se cambio la imagen con exito";

                    Habitacion h = new Habitacion();
                    h.ruta_imagen = ic.Full_path;
                    h.TipoHabitacion = "ESTANDAR";

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var responseE = await client.PostAsJsonAsync("https://localhost:44386/Habitacion/EditarHabitacionImagen", h);
                    string resultadoE = await responseE.Content.ReadAsStringAsync();
                    var responseEE = JsonConvert.DeserializeObject<string>(resultadoE);
                    mensajeIE = "Se ha cambiado la imagen con exito !";

                }
                else
                {
                    mensajeIE = "El formato de la imagen no se encuentra entre las permitidas (jpg,png,gif)";
                }

            }
            else
            {
                mensajeIE = "No se ha seleccionado ningùn archivo.";
            }

            return Json(new { success = true, message = mensajeIE });
            //Llamar Api pasar objeto imagenes 
        }// 

        public async Task<IActionResult> CambiarImgHabitacionJunior(IFormFile file)
        {

            string mensajeHJ = "";

            if (file != null && file.Length > 0)
            {

                Imagenes ic = new Imagenes();
                var saveimg = Path.Combine(_iwebhost.WebRootPath, "imagenes", file.FileName);//la ruta de mi proyecto imagenes
                var stream = new FileStream(saveimg, FileMode.Create);// Creo en un nuevo archivo esa ruta
                await file.CopyToAsync(stream);// agrego
                string exten_img = Path.GetExtension(file.FileName);

                if (exten_img == ".jpg" || exten_img == ".png" || exten_img == ".gif")
                {

                    ic.Name = "ImgJunior"; //nombre imagen
                    ic.Full_path = "/imagenes/" + file.FileName; // ruta imagen se guardo,aqui seria llamar a la base de datos y luego viewbag recupero el path y ya sabe donde esta.
                    mensajeHJ = "Se cambio la imagen con exito";

                    Habitacion h = new Habitacion();
                    h.ruta_imagen = ic.Full_path;
                    h.TipoHabitacion = "JUNIOR";

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var responseJ = await client.PostAsJsonAsync("https://localhost:44386/Habitacion/EditarHabitacionImagen", h);
                    string resultadoJ = await responseJ.Content.ReadAsStringAsync();
                    var responseJJ = JsonConvert.DeserializeObject<string>(resultadoJ);
                    //mensaje = responseJJ;
                    mensajeHJ = "Se ha cambiado la imagen con exito !";

                }
                else
                {
                    mensajeHJ = "El formato de la imagen no se encuentra entre las permitidas (jpg,png,gif)";
                }

            }
            else
            {
                mensajeHJ = "No se ha seleccionado ningún archivo.";
            }

            return Json(new { success = true, message = mensajeHJ });
            //Llamar Api pasar objeto imagenes 
        }// 

        public async Task<IActionResult> CambiarImgHabitacionSuite(IFormFile file)
        {

            string mensajeHS = "";

            if (file != null && file.Length > 0)
            {

                Imagenes ic = new Imagenes();
                var saveimg = Path.Combine(_iwebhost.WebRootPath, "imagenes", file.FileName);//la ruta de mi proyecto imagenes
                var stream = new FileStream(saveimg, FileMode.Create);// Creo en un nuevo archivo esa ruta
                await file.CopyToAsync(stream);// agrego
                string exten_img = Path.GetExtension(file.FileName);

                if (exten_img == ".jpg" || exten_img == ".png" || exten_img == ".gif")
                {

                    ic.Name = "ImgStandar"; //nombre imagen
                    ic.Full_path = "/imagenes/" + file.FileName; // ruta imagen se guardo,aqui seria llamar a la base de datos y luego viewbag recupero el path y ya sabe donde esta.
                    mensajeHS = "Se cambio la imagen con exito";

                    Habitacion h = new Habitacion();
                    h.ruta_imagen = ic.Full_path;
                    h.TipoHabitacion = "SUITE";

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var responseS = await client.PostAsJsonAsync("https://localhost:44386/Habitacion/EditarHabitacionImagen", h);
                    string resultadoSS = await responseS.Content.ReadAsStringAsync();
                    var responseSS = JsonConvert.DeserializeObject<string>(resultadoSS);
                    //mensaje = responseSS;
                    mensajeHS = "Se ha cambiado la imagen con exito !";

                }
                else
                {
                    mensajeHS = "El formato de la imagen no se encuentra entre las permitidas (jpg,png,gif)";
                }

            }
            else
            {
                mensajeHS = "No se ha seleccionado ningùn archivo.";
            }

            return Json(new { success = true, message = mensajeHS });
            //Llamar Api pasar objeto imagenes 
        }// 

        [HttpPost]
        public async Task<ActionResult> EditarHome(string home)
        {
            SitioGeneral sitio = new SitioGeneral();

            sitio.HOME = home;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response4 = await client.PostAsJsonAsync("https://localhost:44386/SitioGeneral/EditarHome", sitio);
            string resultado = await response4.Content.ReadAsStringAsync();
            var response5 = JsonConvert.DeserializeObject<string>(resultado);

            return Json(new { success = true, message = response5 });
            
        }// metodo 
         //Editar temporadas
        /*   public async Task<ActionResult> EditarTemporada(string tipo_temporada, DateTime Fecha_Inicio, DateTime Fecha_Final)
           {
               Temporada temporada = new Temporada();


               temporada.tipo_temporada = tipo_temporada;
               temporada.fecha_inicio = Fecha_Inicio;
               temporada.fecha_final = Fecha_Final;
               client.DefaultRequestHeaders.Accept.Clear();
               client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               var responseTT = await client.PostAsJsonAsync("https://localhost:44386/Temporada/EditarTemporada", temporada);
               string resultado = await responseTT.Content.ReadAsStringAsync();
               var responseT = JsonConvert.DeserializeObject<string>(resultado);

               return Json(new { success = true, message = responseT });

           }// metodo */


        [HttpPost]
        public async Task<IActionResult> EditarTemporadas(string tipoTemporada, string fechaInicio, string fechaFinal) 
        {

            Temporada Temporada_Admin = new Temporada();

            Temporada_Admin.tipo_temporada = tipoTemporada;
            Temporada_Admin.fecha_inicio = Convert.ToDateTime(fechaInicio);
            Temporada_Admin.fecha_final = Convert.ToDateTime(fechaFinal);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
         
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://localhost:44386/Temporada/EditarTemporadas", Temporada_Admin);

            string resultado = await response.Content.ReadAsStringAsync();

            return Ok(resultado);
        }

        public bool  enviarCorreo(string correoEnviar,string asunto, string contenidoMsj) {

            Mensaje mensaje = new Mensaje();
            mensaje.Para = correoEnviar;
            mensaje.De = "bwestocean@gmail.com";
            mensaje.Asunto = asunto;
            mensaje.Cuerpo = contenidoMsj;

            MailMessage ms = new MailMessage(mensaje.De, mensaje.Para);
            ms.Subject = mensaje.Asunto;
            ms.Body = mensaje.Cuerpo;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);//protocolo de envio
            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new NetworkCredential("bwestocean@gmail.com","West.2022");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            
            try {
                smtp.Send(ms);
                return true;
            } catch (SmtpException e) {
                Console.WriteLine("eror");
            }

           return false;
        
        }


    }
}
