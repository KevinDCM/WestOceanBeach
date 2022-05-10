using System;
using System.Collections.Generic;
using System.Text;
using AccessData.Data;
using Entities.Entities;
namespace Business.Business
{
    public class OfertaBusiness
    {
        private static OfertaData ofertaData;

        public OfertaBusiness() {
          
        
            ofertaData=new OfertaData();
        }


        public List<Oferta> obtenerOfertaSobresaliente() {

            return ofertaData.obtenerOfertaSobresaliente();
        
        
        }
    }
}
