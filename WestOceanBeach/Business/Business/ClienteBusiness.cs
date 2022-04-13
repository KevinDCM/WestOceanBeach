using AccessData.Data;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Business
{
    public class ClienteBusiness
    {
        private static ClienteData dataCliente = new ClienteData();

        public string insertarCliente(Cliente datosCliente)
        {
            return dataCliente.insertarCliente(datosCliente);
        }
    }
}
