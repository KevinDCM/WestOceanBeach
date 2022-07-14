using AccessData.Data;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Business
{
    public class PublicidadBusiness
    {
        private static PublicidadData dataPublicidad = new PublicidadData();

        public List<Publicidad> getPublicidadActiva( )
        {
            return dataPublicidad.getPublicidadActiva();
        }

        public List<Publicidad> GetAll()
        {
            return dataPublicidad.GetAll();

        }

        public string Edit(Publicidad publicidad)
        {
            return dataPublicidad.Edit(publicidad);

        }
    }
}