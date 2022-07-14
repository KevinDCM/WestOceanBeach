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
    public class LoginController : Controller
    {
        public IConfiguration Configuration { get; }
        HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://localhost:44386/SitioGeneral/Login", login);

            string resultado = await response.Content.ReadAsStringAsync();

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync("https://localhost:44386/SitioGeneral/Logout");
            string resultado = await response.Content.ReadAsStringAsync();

            return Ok(resultado);

        }

    }
}