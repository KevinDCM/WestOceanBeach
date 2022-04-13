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

namespace AccessData.Data
{
    public class ClienteData
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public ClienteData()
        {
            sqlConnection = new SqlConnection("Data Source=163.178.107.10;Initial Catalog=WestISTest;Persist Security Info=True;User ID=laboratorios;Password=KmZpo.2796;Pooling=False");
            sqlCommand = new SqlCommand();
        }
        
        public string insertarCliente(Cliente datosCliente)
        {

            string salida = "NULL";
            sqlConnection.Open();
            sqlCommand = new SqlCommand("sp_insertarCliente", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@cedula", datosCliente.cedula);
            sqlCommand.Parameters.AddWithValue("@nombre", datosCliente.nombre);
            sqlCommand.Parameters.AddWithValue("@correo", datosCliente.correo);

            sqlCommand.ExecuteNonQuery();
    
            sqlConnection.Close();

            return salida;
        }
        
    }
}
