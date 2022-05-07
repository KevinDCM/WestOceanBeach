using AccessData.Data;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Business
{
    public class HabitacionBusiness
    {
        private static HabitacionData dataHabitacion = new HabitacionData();

        public List<Habitacion> ObtenerHabitacionesDisponibles(int tipoHabitacion)
        {
            return dataHabitacion.ObtenerHabitacionesDisponibles(tipoHabitacion);

        }

        public string obtenerHabitacionesTemporada()
        {
            return dataHabitacion.obtenerHabitacionesTemporada();

        }

    }
}