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
        
        public string EditarComoLlegar(SitioGeneral sitioGeneral)
        {
            return sitio.EditarComoLlegar(sitioGeneral);
        }

        public string EditarHome(SitioGeneral sitioGeneral)
        {
            return sitio.EditarHome(sitioGeneral);
        }

        public SitioGeneral obtenerFacilidades()
        {
            return sitio.obtenerFacilidades();
        }


        public String editarSobreNosostros(SitioGeneral sito) {

            return sitio.editarSobreNosotros(sito);
        
        }

        public SitioGeneral obtenerHome()
        {
            return sitio.obtenerHome();
        }


        private static SitioGeneralData sitio_general_data_02 = new SitioGeneralData();

        public List<Imagenes> ObtenerImagenesHome()
        {
            return sitio_general_data_02.ObtenerImagenesHome();
        }

        public String editarRutaImgHome(Imagenes Imagen)
        {

            return sitio.editarRutaImgHome(Imagen);

        }

    }
}

