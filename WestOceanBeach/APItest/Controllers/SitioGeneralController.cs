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
        [Route("editarFacilidades")]
        public string editarFacilidades(SitioGeneral sitioGeneral)
        {
            return new SitioGeneralBusiness().editarFacilidades(sitioGeneral);
        }

        [HttpPost]
        [Route("editarSobreNosotros")]
        public string editarSobreNosostros(SitioGeneral sitio)
        {

                  
            return new SitioGeneralBusiness().editarSobreNosostros(sitio);
        }

        [HttpPost]
        [Route("EditarHome")]
        public string EditarHome(SitioGeneral sitioGeneral)
        {
            return new SitioGeneralBusiness().EditarHome(sitioGeneral);
        }
       


          [HttpGet]

          [Route("obtenerFacilidades")]
          public SitioGeneral obtenerFacilidades()
          {
             return new SitioGeneralBusiness().obtenerFacilidades();
          }// fin m

      

    }// fin clase

}
