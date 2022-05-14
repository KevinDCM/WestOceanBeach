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
        [Route("RealizarReserva")]
        public string RealizarReserva(Reserva reserva)
        {
            return new ReservaBusiness().RealizarReserva(reserva);
        }
    }
}