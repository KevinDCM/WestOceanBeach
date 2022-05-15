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


        [HttpPost]
        [Route("EditarHome")]
        public string EditarHome(SitioGeneral sitioGeneral)
        {
            return new SitioGeneralBusiness().EditarHome(sitioGeneral);
        }
        [HttpPost]
           [Route("editarFacilidades")]
           public string EditarFacilidades(SitioGeneral  sitioGeneral)
           {
             return new SitioGeneralBusiness().editarFacilidades(sitioGeneral);
           }


          [HttpGet]
          [Route("obtenerFacilidades")]
          public SitioGeneral obtenerFacilidades()
          {
             return new SitioGeneralBusiness().obtenerFacilidades();
          }// fin m

        [HttpGet]
        [Route("obtenerHome")]
        public SitioGeneral obtenerHome()
        {
            return new SitioGeneralBusiness().obtenerHome();
        }// fin m



    }// fin clase
    
}
