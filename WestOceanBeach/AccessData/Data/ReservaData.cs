﻿using Entities.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AccessData.Data
{
    public class ReservaData
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public ReservaData()
        {
            sqlConnection = new SqlConnection("Data Source=163.178.107.10;Initial Catalog=WestOceanBeach;Persist Security Info=True;User ID=laboratorios;Password=KmZpo.2796;Pooling=False");
            sqlCommand = new SqlCommand();
        }

        public string RealizarReserva(Reserva reserva)
        {
            string salida = "";

            // call stored procedure here...
           
            return salida;
        }
    }
}