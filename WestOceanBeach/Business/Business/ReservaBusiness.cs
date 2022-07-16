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
            if (reserva.Edad >= 18)
                return dataReserva.RealizarReserva(reserva);
            else
                return ("Menores de edad no pueden hacer reservaciones.");
        }

        public int ReservaConOferta(int id)
        {
            return dataReserva.ReservaConOferta(id);
         
        }
    }
}