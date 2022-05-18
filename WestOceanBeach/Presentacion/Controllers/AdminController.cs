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
using System.Web.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Presentacion.Controllers
{
    public class AdminController : Controller
    {
        public IConfiguration Configuration { get; }
        HttpClient client = new HttpClient();
        private readonly IWebHostEnvironment _iwebhost;// get the project access


        public AdminController(IWebHostEnvironment _web) { 
        
            _iwebhost = _web;
        
        
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            // Home, Facilidades, Sobre Nosotros y Contacto 
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response1 = await client.GetAsync("https://localhost:44386/SitioGeneral/obtenerSitioGeneral");
            string resultado1 = await response1.Content.ReadAsStringAsync();
            SitioGeneral sitioGeneral1 = JsonConvert.DeserializeObject<SitioGeneral>(resultado1);

            ViewBag.Home = sitioGeneral1.HOME;
            ViewBag.CercaDe = sitioGeneral1.SOBRE_NOSOTROS;
            ViewBag.Contacto = sitioGeneral1.CONTACTO;



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
            var response2 = await client.GetAsync("https://localhost:44386/SitioGeneral/obtenerHome");
            string resultado = await response2.Content.ReadAsStringAsync();
            var sitioGeneral2 = JsonConvert.DeserializeObject<SitioGeneral>(resultado);

            ViewBag.home = sitioGeneral2.HOME;

            return View("Index");
        }// metodo
        
        
        [HttpPost]
        public async Task<ActionResult> EditarSobreNosotros(string sobreNosotros) {

            

            SitioGeneral sitio= new SitioGeneral();

            sitio.SOBRE_NOSOTROS= sobreNosotros;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response2 = await client.PostAsJsonAsync("https://localhost:44386/SitioGeneral/editarSobreNosotros",sitio);
            string resultado = await response2.Content.ReadAsStringAsync();
            var response3 = JsonConvert.DeserializeObject<string>(resultado);



            return Json(new { success = true, message = response3 });


        }

        [HttpPost]
        public async Task<IActionResult> EjemploImagen(IFormFile file) {

            Imagenes ic= new Imagenes();




            var saveimg = Path.Combine(_iwebhost.WebRootPath, "imagenes", file.FileName);//la ruta de mi proyecto imagenes
            var stream = new FileStream(saveimg, FileMode.Create);// Creo en un nuevo archivo esa ruta
            await file.CopyToAsync(stream);// agrego
            ic.name = file.FileName; //nombre imagen
            ic.full_path = "imagenes/"+ic.name;// ruta imagen se guardo,aqui seria llamar a la base de datos y luego viewbag recupero el path y ya sabe donde esta.

            ViewBag.Message = "Se cambio la imagen";
           
           

        
         return View("Index");
        
        
        }



        [HttpPost]
        public async Task<IActionResult> EditarHome(SitioGeneral sitioGeneral)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://localhost:44386/SitioGeneral/EditarHome", sitioGeneral);

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
