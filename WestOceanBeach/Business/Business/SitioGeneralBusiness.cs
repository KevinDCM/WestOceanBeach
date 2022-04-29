using AccessData.Data;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Business
{
    public class SitioGeneralBusiness
    {
        private SitioGeneralData sitio;

        public SitioGeneralBusiness() {

           sitio = new SitioGeneralData();
        
        
        }


        public SitioGeneral ObtenerSitioGeneral() { 
        
        
            return sitio.obtenerSitioGeneral();
        
        }

    }
}
