using Business.Business;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HabitacionController : ControllerBase
    {
        [HttpGet]
        [Route("ObtenerHabitacionesDisponibles/{tipoHabitacion}")]
        public List<Habitacion> ObtenerHabitacionesDisponibles(int tipoHabitacion)
        {
            return new HabitacionBusiness().ObtenerHabitacionesDisponibles(tipoHabitacion);
        }

        [HttpGet]
        [Route("obtenerHabitacionesTemporada")]
        public string obtenerHabitacionesTemporada()
        {
            return new HabitacionBusiness().obtenerHabitacionesTemporada();
        }


        [HttpPost]

        [Route("Habitacion_Junior")]
        public Habitacion Habitacion_Junior()
        {
            return new HabitacionBusiness().Habitacion_Junior();
        }// fin m

        [HttpGet]

        [Route("Habitacion_Estandar")]
        public Habitacion Habitacion_Estandar()
        {
            return new HabitacionBusiness().Habitacion_Estandar();
        }// fin m

        [HttpGet]

        [Route("Habitacion_Suite")]
        public Habitacion Habitacion_Suite()
        {
            return new HabitacionBusiness().Habitacion_Suite();
        }// fin m

        [Route("EditarHabitacion")]
        public string EditarHabitacion(Habitacion habitacion)
        {
            return new HabitacionBusiness().editarHabitacion(habitacion);
        }//

        [Route("EditarHabitacionImagen")]
        public string EditarHabitacionImagen(Habitacion habitacion)
        {
            return new HabitacionBusiness().editarHabitacionImagen(habitacion);
        }//

       

        [Route("estadoActualHabitacion")]
        public List<Habitacion> estadoActualHabitacion()
        {
            return new HabitacionBusiness().estadoActualHabitacion();
        }// fin m


    }
}