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

        public string buscarHabitaciones(Reserva reserva)
        {
            return dataReserva.buscarHabitaciones(reserva);
        }// fin m

        public string reservarHabitacion(Reserva reserva)
        {
            return dataReserva.reservarHabitacion(reserva);
        }// fin m


    }// fin clase

}// fin