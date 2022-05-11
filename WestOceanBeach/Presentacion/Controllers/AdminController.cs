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
using Entities.Entities;

namespace Presentacion.Controllers
{
    public class AdminController : Controller
    {
        public IConfiguration Configuration { get; }
        HttpClient client = new HttpClient();

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("https://localhost:44386/SitioGeneral/obtenerFacilidades");
            string resultado = await response.Content.ReadAsStringAsync();
            var sitioGeneral = JsonConvert.DeserializeObject<SitioGeneral>(resultado);

            ViewBag.facilidades = sitioGeneral.FACILIDADES;

            return View();
        }// metodo

        [HttpPost]
        public async Task<IActionResult> EditarFacilidades(SitioGeneral sitioGeneral)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://localhost:44386/SitioGeneral/editarFacilidades", sitioGeneral);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response2 = await client.GetAsync("https://localhost:44386/SitioGeneral/obtenerFacilidades");
            string resultado = await response2.Content.ReadAsStringAsync();
            var sitioGeneral2 = JsonConvert.DeserializeObject<SitioGeneral>(resultado);

            ViewBag.facilidades = sitioGeneral2.FACILIDADES;

            return View("Index");
        }// metodo

       
    }
}
