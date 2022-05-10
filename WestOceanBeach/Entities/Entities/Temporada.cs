using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Temporada
    {
        public string tipo_temporada { get; set; }
        public int descuento_temporada { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_final { get; set; }
    }
}
