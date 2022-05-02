using AccessData.Data;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Business
{
    public class OfertasBusiness
    {
        private  OfertasData Oferta;

        public OfertasBusiness() {

           Oferta = new OfertasData();
        
        }

        public Ofertas OfertasActuales()
        {


            return Oferta.OfertasActuales();

        }

    }
}
