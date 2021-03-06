using AccessData.Data;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Business
{
    public class HabitacionBusiness
    {
        private HabitacionData dataHabitacion;

        public HabitacionBusiness()
        {
            dataHabitacion = new HabitacionData();
        }

        public List<Habitacion> ObtenerHabitacionesDisponibles(int tipoHabitacion)
        {
            return dataHabitacion.ObtenerHabitacionesDisponibles(tipoHabitacion);

        }
       

          public List<Habitacion> ObtenerHabitacionesDisponiblesPorFechaTipo(Habitacion habitacion)
        {
            return dataHabitacion.ObtenerHabitacionesDisponiblesPorFechaTipo(habitacion);

        }

        public List<Habitacion> ObtenerHabitacionesDisponiblesPorFecha(Habitacion habitacion)
        {
            return dataHabitacion.ObtenerHabitacionesDisponiblesPorFecha(habitacion);

        }

        public float ObtenerTarifaDiaria(int tipoHabitacion)
        {
            return dataHabitacion.ObtenerTarifaDiaria(tipoHabitacion);

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

        public List<Habitacion> estadoActualHabitacion()
        {
            return dataHabitacion.estadoActualHabitacion();
    }//

        public string ValidarHabitacionDisponible(Habitacion habitacion)
        {
            return dataHabitacion.ValidarHabitacionDisponible(habitacion);
        }
    }
}