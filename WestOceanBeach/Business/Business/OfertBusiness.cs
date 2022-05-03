using System;
using System.Collections.Generic;
using System.Text;
using AccessData.Data;
using Entities.Entities;
namespace Business.Business
{
    public class OfertBusiness
    {
        private static OfertData ofertaData;

        public OfertBusiness() {
          
        
            ofertaData=new OfertData();
        }


        public List<Oferta> obtenerOfertaSobresaliente() {

            return ofertaData.obtenerOfertaSobresaliente();
        
        
        }
    }
}
