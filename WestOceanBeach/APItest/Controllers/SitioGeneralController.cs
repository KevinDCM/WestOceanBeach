using Business.Business;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        [HttpPost]

        [Route("editarRutaImgHome")]
        public string editarRutaImgHome(Imagenes Imagen)
        {
            return new SitioGeneralBusiness().editarRutaImgHome(Imagen);
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



        [HttpGet]
        [Route("ObtenerImagenesHome")]
        public List<Imagenes> ObtenerImagenesHome()
        {
            return new SitioGeneralBusiness().ObtenerImagenesHome();
        }

    }// fin clase

}
