using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using Entities.Entities;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace AccessData.Data
{
    public class ClienteData
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public ClienteData()
        {
            try
            {
                sqlConnection = new SqlConnection("Data Source=163.178.107.10;Initial Catalog=WestOceanBeach;Persist Security Info=True;User ID=laboratorios;Password=Uy&)&nfC7QqQau.%278UQ24/=%;Pooling=False");
                sqlCommand = new SqlCommand();
            }
             catch  (SmtpException e)
            {
                Console.WriteLine("Ha ocurrido un error, por favor intentarlo más tarde...");

            }
        }

        public string insertarCliente(Cliente datosCliente)
        {
              string salida = "NULL";

            return salida;
        }
        
    }
}
