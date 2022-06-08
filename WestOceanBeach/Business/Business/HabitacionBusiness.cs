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
        public Habitacion Habitacion_Junior()
        {
            return dataHabitacion.Habitacion_Junior();

        }
        public Habitacion Habitacion_Estandar()
        {
            return dataHabitacion.Habitacion_Estandar();

        }
        public Habitacion Habitacion_Suite()
        {
            return dataHabitacion.Habitacion_Suite();

        }

        public string editarHabitacion(Habitacion habitacion)
        {
            return dataHabitacion.editarHabitacion(habitacion);
        }// editarHabitacion

        public string editarHabitacionImagen(Habitacion habitacion)
        {
            return dataHabitacion.editarHabitacionImagen(habitacion);
        }//

    }
}