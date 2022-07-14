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
        [Route("EditarTemporadas")]
        public string EditarTemporadas(Temporada temporada)
        {
            return new TemporadaBusiness().EditarTemporadas(temporada);
        }


        [HttpGet]
        [Route("GetTemporadaActual")]
        public Temporada GetTemporadaActual()
        {
            return new TemporadaBusiness().GetTemporadaActual();
        }


    }
}
