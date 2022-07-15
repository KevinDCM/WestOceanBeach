using Entities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Presentacion.Controllers
{
    public class ValidacionesController : Controller
    {
        public IConfiguration Configuration { get; }
        HttpClient client02 = new HttpClient();
        private readonly IWebHostEnvironment _iwebhost;// get the project access

        public ValidacionesController(IWebHostEnvironment _web) {
            _iwebhost = _web;
        }

        [HttpGet]
      

        public IActionResult Index_Val()
        
        {
            return View();

        }


    }
}
