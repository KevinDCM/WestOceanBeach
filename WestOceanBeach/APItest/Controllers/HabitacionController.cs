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
    }
}