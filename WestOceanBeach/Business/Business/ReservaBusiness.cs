using AccessData.Data;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Business
{
    public class ReservaBusiness
    {
        private static ReservaData dataReserva = new ReservaData();

        public string RealizarReserva(Reserva reserva)
        {
            return dataReserva.RealizarReserva(reserva);
        }
    }
}