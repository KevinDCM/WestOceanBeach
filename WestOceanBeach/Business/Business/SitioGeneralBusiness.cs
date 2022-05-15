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

        public string editarFacilidades(SitioGeneral sitioGeneral)
        {
            return sitio.editarFacilidades(sitioGeneral);
        }

        public SitioGeneral obtenerFacilidades()
        {
            return sitio.obtenerFacilidades();
        }

        public String editarSobreNosostros(SitioGeneral sito) {

            return sitio.editarSobreNosotros(sito);
        
        }


    }
}
