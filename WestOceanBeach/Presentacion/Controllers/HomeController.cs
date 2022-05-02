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

            var response02 = await client.GetAsync("https://localhost:44386/ofertas/OfertasActuales");
            string resultado02 = await response02.Content.ReadAsStringAsync();

            var respContent = await response.Content.ReadAsStringAsync();

            var ofertas = JsonConvert.DeserializeObject<Ofertas>(respContent);

            @ViewBag.ofertas01 = ofertas.cantidad_personas;
            @ViewBag.ofertas02 = ofertas.descuento;
            @ViewBag.ofertas03 = ofertas.fecha_inicio;
            @ViewBag.ofertas04 = ofertas.fecha_final;
             return View();
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

    }// fin clase
}// fin
