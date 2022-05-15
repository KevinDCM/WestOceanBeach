using Entities.Entities;
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
            // call stored procedure here...
            string salida = "Reserva realizada!";

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_RealizarReserva", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // parámetros del cliente
            sqlCommand.Parameters.AddWithValue("@param_CEDULA", reserva.Cedula);
            sqlCommand.Parameters.AddWithValue("@param_NOMBRE", reserva.Nombre);
            sqlCommand.Parameters.AddWithValue("@param_PRIMER_APELLIDO", reserva.PrimerApellido);
            sqlCommand.Parameters.AddWithValue("@param_SEGUNDO_APELLIDO", reserva.SegundoApellido);
            sqlCommand.Parameters.AddWithValue("@param_CORREO", reserva.Correo);
            sqlCommand.Parameters.AddWithValue("@param_TELEFONO", reserva.Telefono);
            sqlCommand.Parameters.AddWithValue("@param_EDAD", reserva.Edad);
            sqlCommand.Parameters.AddWithValue("@param_NACIONALIDAD", reserva.Nacionalidad);
            sqlCommand.Parameters.AddWithValue("@param_DIRECCION", reserva.Direccion);

            // parámetros de la reserva
            sqlCommand.Parameters.AddWithValue("@fechaI", reserva.fechaI);
            sqlCommand.Parameters.AddWithValue("@fechaF", reserva.fechaF);
            sqlCommand.Parameters.AddWithValue("@fechaIS", reserva.fechaIS);
            sqlCommand.Parameters.AddWithValue("@fechaFS", reserva.fechaFS);
            sqlCommand.Parameters.AddWithValue("@listaHabitacionesEscogidas", reserva.ListHabitaciones);
            sqlCommand.Parameters.AddWithValue("@param_IdOferta", reserva.IdOFerta);

            sqlCommand.ExecuteNonQuery();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {




            };
            sqlConnection.Close();

            return salida;
        }
    }
}