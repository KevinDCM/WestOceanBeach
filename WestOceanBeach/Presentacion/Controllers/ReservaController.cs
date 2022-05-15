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
    public class ReservaController : Controller
    {
        public IConfiguration Configuration { get; }
        HttpClient client = new HttpClient();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RealizarReserva([FromBody] Reserva reserva)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            reserva.fechaI = Convert.ToDateTime(reserva.fechaIS);
            reserva.fechaF = Convert.ToDateTime(reserva.fechaFS);

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://localhost:44386/Reserva/RealizarReserva", reserva);

            string resultado = await response.Content.ReadAsStringAsync();

            return Ok(resultado);  
        }
    }
}
