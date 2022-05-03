using Business.Business;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class SitioGeneralController : ControllerBase
        {
            [HttpGet]
            [Route("obtenerSitioGeneral")]
            public SitioGeneral obtenerSitioGeneral()

            {
                return new SitioGeneralBusiness().ObtenerSitioGeneral();
            }// fin m
         
        }// fin clase
    
}
