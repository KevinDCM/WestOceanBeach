using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Ofertas
    {
        public Ofertas()
        {

        }
        public int cantidad_personas { get; set; }
        public int descuento { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_final { get; set; }



    }

}
