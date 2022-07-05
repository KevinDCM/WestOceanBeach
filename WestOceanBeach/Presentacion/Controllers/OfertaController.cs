using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Presentacion.Controllers
{
    public class OfertaController : Controller
    {
        public IConfiguration Configuration { get; }
        HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerHabitacionesDisponibles()
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(
                "https://localhost:44386/Oferta/consultaGeneralOfertasEspeciales");

            string content = await response.Content.ReadAsStringAsync();

            List<Oferta> Habitaciones = JsonConvert.DeserializeObject<List<Oferta>>(content);

            return Ok(Habitaciones);
        }




        [HttpPost]
        public async Task<IActionResult> ObtenerOfertasEspecialesGeneral()
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(
                "https://localhost:44386/Oferta/consultaGeneralOfertasEspeciales");

            string content = await response.Content.ReadAsStringAsync();

            List<Oferta> ofertas = JsonConvert.DeserializeObject<List<Oferta>>(content);

            return Ok(ofertas);
        }



        [HttpPost]
        public async Task<IActionResult> ObtenerOfertasEspecialesGeneralTipoHabitacion(string tipoHabitacion)
        {
            Oferta oferta = new Oferta();
            oferta.tipo_habitacion=tipoHabitacion;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync("https://localhost:44386/Oferta/consultaGeneralOfertasEspecialesTipoHabitacion", oferta);

            string content = await response.Content.ReadAsStringAsync();

            List<Oferta> ofertas = JsonConvert.DeserializeObject<List<Oferta>>(content);

            return Ok(ofertas);
        }



        [HttpPost]
        public async Task<IActionResult> crearOfertaEspecial(int descuento,string fechaInicio,string fechaFinal,string tipoHabitacion)
        {


            Oferta oferta= new Oferta();
            oferta.descuento = descuento;
            oferta.fecha_inicio = Convert.ToDateTime(fechaInicio);
            oferta.fecha_final = Convert.ToDateTime(fechaFinal);
            oferta.tipo_habitacion = tipoHabitacion;




            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync("https://localhost:44386/Oferta/crearOfertaEspecial", oferta);

            string resultado = await response.Content.ReadAsStringAsync();
            var response3 = JsonConvert.DeserializeObject<string>(resultado);

            return Json(new { success = true, message = response3 });

           

         
        }


        [HttpPost]
        public async Task<IActionResult> actualizarOfertaEspecial(int id,int descuento, string fechaInicio, string fechaFinal, string tipoHabitacion)
        {


            Oferta oferta = new Oferta();
            oferta.Id = id;
            oferta.descuento = descuento;
            oferta.fecha_inicio = Convert.ToDateTime(fechaInicio);
            oferta.fecha_final = Convert.ToDateTime(fechaFinal);
            oferta.tipo_habitacion = tipoHabitacion;




            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync("https://localhost:44386/Oferta/actualizarOfertaEspecial", oferta);

            string resultado = await response.Content.ReadAsStringAsync();
            var response3 = JsonConvert.DeserializeObject<string>(resultado);

            return Json(new { success = true, message = response3 });




        }




        [HttpPost]
        public async Task<IActionResult> eliminarOfertaEspecial(int id)
        {


            Oferta oferta = new Oferta();
            oferta.Id = id;
         




            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync("https://localhost:44386/Oferta/eliminarOfertaEspecial", oferta);

            string resultado = await response.Content.ReadAsStringAsync();
            var response3 = JsonConvert.DeserializeObject<string>(resultado);

            return Json(new { success = true, message = response3 });




        }






    }
}
