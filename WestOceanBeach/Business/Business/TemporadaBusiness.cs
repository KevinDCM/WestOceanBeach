using System;
using System.Collections.Generic;
using System.Text;
using AccessData.Data;
using Entities.Entities;
namespace Business.Business
{
    public class TemporadaBusiness
    {
        private static TemporadaData temporadadata;

        public TemporadaBusiness() {
          
        
            temporadadata=new TemporadaData();
        }

        public string obtenerTemporadas()
        {
            return temporadadata.obtenerTemporadas();

        }

        public string EditarTemporada(Temporada temporada)
        {
            return temporadadata.EditarTemporada(temporada);
        }

    }
}
