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
        public Imagenes Imagenes { get; private set; }

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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Home, Facilidades, Sobre Nosotros y Contacto 
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync("https://localhost:44386/SitioGeneral/obtenerSitioGeneral");
            string resultado = await response.Content.ReadAsStringAsync();
            SitioGeneral sitioGeneral = JsonConvert.DeserializeObject<SitioGeneral>(resultado);

            ViewBag.Home = sitioGeneral.HOME;
            ViewBag.CercaDe = sitioGeneral.SOBRE_NOSOTROS;
            ViewBag.Contacto= sitioGeneral.CONTACTO;
            ViewBag.ComoLLegar = sitioGeneral.COMO_LLEGAR;

            //Imagen de HOME

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseImgHome = await client.GetAsync("https://localhost:44386/SitioGeneral/ObtenerImagenesHome");
            string responseImagenHomeContent = await responseImgHome.Content.ReadAsStringAsync();
            List<Imagenes> imagenes = JsonConvert.DeserializeObject<List<Imagenes>>(responseImagenHomeContent);
            @ViewBag.ImgHome = imagenes[0].Full_path;
         



            //FACILIDADES
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseF = await client.GetAsync("https://localhost:44386/SitioGeneral/obtenerFacilidades");
            string resultadoF = await responseF.Content.ReadAsStringAsync();
            var sitioGeneralF = JsonConvert.DeserializeObject<SitioGeneral>(resultadoF);

            ViewBag.Facilidades = sitioGeneralF.FACILIDADES;


            // Ofertas que se muestran en el header (top 5)
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var response02 = await client.GetAsync("https://localhost:44386/Oferta/obtenerOfertaSobresalientes");
            string resultado02 = await response02.Content.ReadAsStringAsync();

            List<Oferta> ofertas = JsonConvert.DeserializeObject<List<Oferta>>(resultado02);

            string top_ofertas = "| ";

            for(int i=0; i<ofertas.Count; i++)
            {

                top_ofertas += "Oferta  #" + (i+1) + 
                ", Habitación: " + ofertas[i].tipo_habitacion +
                ", Descuento: " + ofertas[i].descuento +
                "%, Inicio oferta: " + ofertas[i].fecha_inicio +
                ", Fin oferta: " + ofertas[i].fecha_final +
                ", Cantidad de noches: " + ofertas[i].cantidad_dias;

                top_ofertas += "  |  ";
            }

            @ViewBag.top5_Ofertas = top_ofertas;


            // Tarifas de habitaciones según Temporada
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


            // PUBLICIDAD   (programado para mostrar solo 4 elementos publicitarios)
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
    }
}