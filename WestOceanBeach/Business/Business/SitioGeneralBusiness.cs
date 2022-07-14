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

        public List<Imagenes> ObtenerImagenesHome()
        {
            return sitio.ObtenerImagenesHome();
        }

        public String editarRutaImgHome(Imagenes Imagen)
        {
            return sitio.editarRutaImgHome(Imagen);

        }

        public string Login(Login login)
        {
            return sitio.Login(login);
        }

        public string Logout()
        {
            return sitio.Logout();

        }
    }
}

