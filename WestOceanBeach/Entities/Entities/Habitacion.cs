﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Habitacion
    {
        public int CodigoTipoHabitacion { get; set; }
        public string TipoHabitacion { get; set; }
        public string NumeroHabitacion { get; set; }
        public int Estado { get; set; } // bit 1 ó 0, ocupada, desocupada
        public int Disponibilidad { get; set; } // bit 1 ó 0, disponible, fuera de servicio
        public string Descripcion { get; set; }
        public int CantidadPersonas { get; set; }  
        public int TarifaDiaria { get; set; }

    }
}
