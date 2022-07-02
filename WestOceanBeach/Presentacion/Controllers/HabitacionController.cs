using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Presentacion.Models;

namespace Presentacion.Controllers
{
    public class HabitacionController : Controller
    {
        public IConfiguration Configuration { get; }
        HttpClient client = new HttpClient();

        public async Task<IActionResult> IndexAsync()
        {
         
            return View();


        }

        [HttpGet]
        public async Task<IActionResult> ObtenerHabitacionesDisponibles(Habitacion Habitacion)
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(
                "https://localhost:44386/Habitacion/ObtenerHabitacionesDisponibles/" + Habitacion.CodigoTipoHabitacion);

            string content = await response.Content.ReadAsStringAsync();

            List<Habitacion> Habitaciones = JsonConvert.DeserializeObject<List<Habitacion>>(content);

            return Ok(Habitaciones);
        }


        [HttpPost]
        public async Task<IActionResult> ObtenerHabitacionesRangoFecha(string fechaLlegada, string fechaSalida)
        {
            Habitacion habitacion = new Habitacion();
            habitacion.fechaFS= fechaSalida;
            habitacion.fechaIS = fechaLlegada;
            

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response= await client.PostAsJsonAsync("https://localhost:44386/Habitacion/ObtenerHabitacionesDisponiblesPorFecha", habitacion);

       

            string content = await response.Content.ReadAsStringAsync();

            List<Habitacion> Habitaciones = JsonConvert.DeserializeObject<List<Habitacion>>(content);

            return Ok(Habitaciones);
        }



    }
}