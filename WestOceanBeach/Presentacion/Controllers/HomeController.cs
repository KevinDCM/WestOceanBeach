using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        public IActionResult Index()
        {
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
