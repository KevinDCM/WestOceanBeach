using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Entities;
using Business.Business;
using System.Collections.Generic;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class OfertController : ControllerBase
    {
        [HttpGet]
        [Route("obtenerOfertaSobresalientes")]
        public List<Oferta> obtenerOfertaSobresalientes()

        {
            
            return new OfertBusiness().obtenerOfertaSobresaliente();
        }// fin m


    }
}
