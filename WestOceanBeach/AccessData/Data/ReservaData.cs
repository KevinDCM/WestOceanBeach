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
        }// const

        public string buscarHabitaciones(Reserva reserva)
        {
            string salida = "";

            System.Text.StringBuilder habitaciones = new System.Text.StringBuilder();

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_buscarHabitaciones", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@tipoHabitacion", reserva.tipoHabitacion);

            sqlCommand.ExecuteNonQuery();
            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                    ArrayList itm = new ArrayList(allColumns);

                    habitaciones.Append(itm[0].ToString());
                    habitaciones.Append(",");
                }// for

            };
            sqlConnection.Close();

            salida = habitaciones.ToString();

            return salida;
        }// buscarHabitaciones

        public string reservarHabitacion(Reserva reserva)
        {
            string salida = "";

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_reservarHabitacion", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@fechaI", reserva.fechaIS);
            sqlCommand.Parameters.AddWithValue("@fechaF", reserva.fechaFS);
            sqlCommand.Parameters.AddWithValue("@numHabitacion", reserva.numHabitacion);
            sqlCommand.Parameters.AddWithValue("@nombre", reserva.nombre);

            sqlCommand.ExecuteNonQuery();
            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Reserva reservaS = new Reserva();

                    DataRow dr = dt.Rows[i];
                    string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                    ArrayList itm = new ArrayList(allColumns);

                    reservaS.nombre = itm[0].ToString();

                    salida = reservaS.nombre;

                }// for

            };
            sqlConnection.Close();

            return salida;
        }// reservarHabitacion

    }// fin clase

}// fin


