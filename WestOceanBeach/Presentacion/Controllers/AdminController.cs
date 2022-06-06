using Entities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
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
            //------------------------------------------------------------------------------------------------
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            var response3 = await client.GetAsync("https://localhost:44386/Habitacion/Habitacion_Junior");
            string resultado3 = await response3.Content.ReadAsStringAsync();
            Habitacion Habitaciones = JsonConvert.DeserializeObject<Habitacion>(resultado3);

            ViewBag.Habitacion_junior_descripcion = Habitaciones.Descripcion;

            ViewBag.Habitacion_junior_ruta_imagen = Habitaciones.ruta_imagen;

            ViewBag.Habitacion_junior_tarifa_diaria = Habitaciones.TarifaDiaria;

            return View();
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

        }

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

            string mensaje = "";

            if (file != null && file.Length > 0) {

                Imagenes ic= new Imagenes();
                var saveimg = Path.Combine(_iwebhost.WebRootPath, "imagenes", file.FileName);//la ruta de mi proyecto imagenes
                var stream = new FileStream(saveimg, FileMode.Create);// Creo en un nuevo archivo esa ruta
                await file.CopyToAsync(stream);// agrego
                string exten_img = Path.GetExtension(file.FileName);

                if(exten_img == ".jpg"|| exten_img == ".png" || exten_img == ".gif"){

                    ic.Name = "ImgHome"; //nombre imagen
                    ic.Full_path = "imagenes/"+file.FileName; // ruta imagen se guardo,aqui seria llamar a la base de datos y luego viewbag recupero el path y ya sabe donde esta.
                    mensaje= "Se cambio la imagen con exito";
           
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response2 = await client. PostAsJsonAsync("https://localhost:44386/SitioGeneral/editarRutaImgHome", ic);
                    string resultado = await response2.Content.ReadAsStringAsync();
                    var response3 = JsonConvert.DeserializeObject<string>(resultado);

                } else {
                    mensaje= "El formato de la imagen no se encuentra entre las permitidas (jpg,png,gif)";
                }

            } else {
                mensaje = "No se ha seleccionado ningùn archivo.";
            }

            return Json(new { success = true, message = mensaje });
            //Llamar Api pasar objeto imagenes 
        }


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
