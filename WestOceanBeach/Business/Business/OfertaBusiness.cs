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

        public string crearOfertaEspecial(Oferta oferta) { 
        
        
            return ofertaData.crearOfertaEspecial(oferta);
        
        
        }

        public List<Oferta> consultaGeneralOfertasEspeciales() {


            return ofertaData.consultaGeneralOfertasEspeciales();
        
        
        }

        public List<Oferta> consultaGeneralOfertasEspecialesTipoHabitacion(Oferta oferta) {


            return ofertaData.consultaGeneralOfertasEspecialesTipoHabitacion(oferta);


        }

        public string eliminarOfertaEspecial(Oferta oferta) {


            return ofertaData.eliminarOfertaEspecial(oferta);


        }


        public string actualizarOfertaEspecial(Oferta oferta) {


            return ofertaData.actualizarOfertaEspecial(oferta);


        }





    }
}
