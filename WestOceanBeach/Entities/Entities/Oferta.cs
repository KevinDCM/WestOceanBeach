using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Oferta
    {
        public int Id { get; set; }
        public int cantidad_dias { get; set; }
        public string tipo_habitacion { get; set; }
        public int descuento { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_final { get; set; }

        public string fecha_ini { get; set; }
        public string fecha_fin { get; set; }

    }
}
