using Business.Business;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservaController : ControllerBase
    {
        [HttpPost]
        [Route("obtenerHabitaciones")]
        public string obtenerHabitaciones(Reserva reserva)
        {
            return new ReservaBusiness().buscarHabitaciones(reserva);
        }// fin m

        [HttpPost]
        [Route("reservarHabitacion")]
        public string reservarHabitacion(Reserva reserva)
        {
            return new ReservaBusiness().reservarHabitacion(reserva);
        }// fin m

    }// fin clase

}// fin

