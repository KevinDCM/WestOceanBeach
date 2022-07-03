using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Entities;
using Business.Business;
using System.Collections.Generic;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TemporadaController : ControllerBase
    {
        [HttpGet]
        [Route("obtenerTemporadas")]
   
        public string obtenerTemporadas()
        {
            return new TemporadaBusiness().obtenerTemporadas();
        }

        [HttpPost]
        [Route("EditarTemporada")]
        public string EditarTemporada(Temporada temporada)
        {
            return new TemporadaBusiness().EditarTemporada(temporada);
        }


    }
}
