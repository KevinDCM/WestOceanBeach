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
    public class PublicidadController : Controller
    {
        public IConfiguration Configuration { get; }
        HttpClient client = new HttpClient();

        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Publicidad publicidad)
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://localhost:44386/Publicidad/Edit", publicidad);

            string resultado = await response.Content.ReadAsStringAsync();

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPublicidad()
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync("https://localhost:44386/Publicidad/GetAll");
            string resultado = await response.Content.ReadAsStringAsync();

            return Ok(resultado);

        }

    }
}