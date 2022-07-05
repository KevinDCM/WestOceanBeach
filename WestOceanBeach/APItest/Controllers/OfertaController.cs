using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Entities;
using Business.Business;
using System.Collections.Generic;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class OfertaController : ControllerBase
    {
        [HttpGet]
        [Route("obtenerOfertaSobresalientes")]
        public List<Oferta> obtenerOfertaSobresalientes()

        {
            
            return new OfertaBusiness().obtenerOfertaSobresaliente();
        }// fin m



        [HttpPost]
        [Route("crearOfertaEspecial")]
        public string crearOfertaEspecial(Oferta oferta)

        {

            return new OfertaBusiness().crearOfertaEspecial(oferta);
        }// fin m

        [HttpPost]
        [Route("consultaGeneralOfertasEspeciales")]
        public List<Oferta> consultaGeneralOfertasEspeciales()

        {

            return new OfertaBusiness().consultaGeneralOfertasEspeciales();
        }// fin m


        [HttpPost]
        [Route("consultaGeneralOfertasEspecialesTipoHabitacion")]
        public List<Oferta> consultaGeneralOfertasEspecialesTipoHabitacion(Oferta oferta)

        {

            return new OfertaBusiness().consultaGeneralOfertasEspecialesTipoHabitacion(oferta);
        }// fin m

        [HttpPost]
        [Route("eliminarOfertaEspecial")]
        public string eliminarOfertaEspecial(Oferta oferta) {

            return new OfertaBusiness().eliminarOfertaEspecial(oferta);


        }


        [HttpPost]
        [Route("actualizarOfertaEspecial")]
        public string actualizarOfertaEspecial(Oferta oferta) {

            return new OfertaBusiness().actualizarOfertaEspecial(oferta);


        }


    }
}
