using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Presentacion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IConfiguration Configuration { get; }
        HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // se llaman varios métodos de la API, para aprovechar el ViewBag del HomeController
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            

            var response = await client.GetAsync("https://localhost:44386/SitioGeneral/obtenerSitioGeneral");
            string resultado = await response.Content.ReadAsStringAsync();
            var sitioGeneral = JsonConvert.DeserializeObject<SitioGeneral>(resultado);

           
            ViewBag.Home = sitioGeneral.HOME;
            //ViewBag.Facilidades = sitioGeneral.FACILIDADES;
           
            string[] subs = sitioGeneral.SOBRE_NOSOTROS.Split('/');
            string[] subs2 = sitioGeneral.CONTACTO.Split(',');
            string[] subs3 = sitioGeneral.FACILIDADES.Split('/');

            @ViewBag.texto1=subs[0];
            @ViewBag.texto2 = subs[1];
            @ViewBag.texto3 = subs[2];
            @ViewBag.texto4 = subs[3];
            @ViewBag.contacto1=subs2[0];
            @ViewBag.contacto2 = subs2[1];
            @ViewBag.contacto3 = subs2[2];
            @ViewBag.contacto4 = subs2[3];
            @ViewBag.facilidades1 = subs3[0];
            @ViewBag.facilidades2 = subs3[1];
            @ViewBag.facilidades3 = subs3[2];
            @ViewBag.facilidades4 = subs3[3];

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var response02 = await client.GetAsync("https://localhost:44386/Oferta/obtenerOfertaSobresalientes");
            string resultado02 = await response02.Content.ReadAsStringAsync();

            List<Oferta> ofertas = JsonConvert.DeserializeObject<List<Oferta>>(resultado02);

            // hacer un for que concatene en un string las 5 ofertas recibidas

            string top5_ofertas = "| ";

            for(int i=0; i<ofertas.Count; i++)
            {

                top5_ofertas += "Oferta  #" + (i+1) + 
                ", Habitación: " + ofertas[i].tipo_habitacion +
                ", Descuento: " + ofertas[i].descuento +
                "%, Inicia: " + ofertas[i].fecha_inicio +
                ", Finaliza: " + ofertas[i].fecha_final;
                // top5_ofertas +=", Cantidad de personas: " + ofertas[i].cantidad_personas;

                top5_ofertas += "  |  ";
            }

            @ViewBag.top5_Ofertas = top5_ofertas;


            //HabitacionTemporada
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var responseHabitacionTemporada= await client.GetAsync("https://localhost:44386/Habitacion/obtenerHabitacionesTemporada");
            string resultadoHabitacionTemporada = await responseHabitacionTemporada.Content.ReadAsStringAsync();

            string[] habitaciones = resultadoHabitacionTemporada.Split('#');

            @ViewBag.habitacionImagen1=habitaciones[1];
            @ViewBag.nombre1 = habitaciones[2];
            @ViewBag.precioBaja1 = habitaciones[3];
            @ViewBag.fechaIniBaja1 = habitaciones[4];
            @ViewBag.fechaTermBaja1 = habitaciones[5];
            @ViewBag.precioAlta1 = habitaciones[6];
            @ViewBag.fechaIniAlta1 = habitaciones[7];
            @ViewBag.fechaTermAlta1 = habitaciones[8];


            @ViewBag.habitacionImagen2 = habitaciones[9];
            @ViewBag.nombre2 = habitaciones[10];
            @ViewBag.precioBaja2 = habitaciones[11];
            @ViewBag.fechaIniBaja2 = habitaciones[12];
            @ViewBag.fechaTermBaja2 = habitaciones[13];
            @ViewBag.precioAlta2 = habitaciones[14];
            @ViewBag.fechaIniAlta2 = habitaciones[15];
            @ViewBag.fechaTermAlta2 = habitaciones[16];

            @ViewBag.habitacionImagen3 = habitaciones[17];
            @ViewBag.nombre3 = habitaciones[18];
            @ViewBag.precioBaja3 = habitaciones[19];
            @ViewBag.fechaIniBaja3 = habitaciones[20];
            @ViewBag.fechaTermBaja3 = habitaciones[21];
            @ViewBag.precioAlta3 = habitaciones[22];
            @ViewBag.fechaIniAlta3 = habitaciones[23];
            @ViewBag.fechaTermAlta3 = habitaciones[24];


            // PUBLICIDAD   (programado para mostrar 4 elementos publicitarios)
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responsePublicidad = await client.GetAsync("https://localhost:44386/Publicidad/getPublicidadActiva");

            string responsePublicidadContent = await responsePublicidad.Content.ReadAsStringAsync();

            List<Publicidad> publicidad = JsonConvert.DeserializeObject<List<Publicidad>>(responsePublicidadContent);

            @ViewBag.public1 = publicidad[0].RutaImagen;
            @ViewBag.uri1 = publicidad[0].SiteUrl;

            @ViewBag.public2 = publicidad[1].RutaImagen;
            @ViewBag.uri2 = publicidad[1].SiteUrl;

            @ViewBag.public3 = publicidad[2].RutaImagen;
            @ViewBag.uri3 = publicidad[2].SiteUrl;

            @ViewBag.public4 = publicidad[3].RutaImagen;
            @ViewBag.uri4 = publicidad[3].SiteUrl;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> buscarHabitaciones(Reserva reserva)
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://localhost:44386/Reserva/obtenerHabitaciones", reserva);
            string resultado = await response.Content.ReadAsStringAsync();

            resultado = string.Join("", resultado.Split('"'));

            string[] sList = resultado.Split(",");
            List<SelectListItem> List = new List<SelectListItem>();
            foreach (string parte in sList)
            {
                List.Add(new SelectListItem { Text = parte, Value = parte });
            }// for

            ViewBag.H = List;

            ViewBag.fIn = reserva.fechaI.ToString("dd/MM/yyyy");
            ViewBag.fFn = reserva.fechaF.ToString("dd/MM/yyyy");

            return View("Index");
        }// buscarHabitaciones


        [HttpPost]
        public async Task<IActionResult> reservarHabitacion(Reserva reserva)
        {
            reserva.fechaI = Convert.ToDateTime(reserva.fechaIS);
            reserva.fechaF = Convert.ToDateTime(reserva.fechaFS);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://localhost:44386/Reserva/reservarHabitacion", reserva);
            string resultado = await response.Content.ReadAsStringAsync();

            resultado = string.Join("", resultado.Split('"'));

            // esto se imprime mal, pero funciona en la base de datos
            ViewBag.Respuesta = resultado;

            return View("Index");
        }// reservarHabitacion

    }
}