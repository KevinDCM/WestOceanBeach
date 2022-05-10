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
    public class HabitacionData
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        public HabitacionData()
        {
            sqlConnection = new SqlConnection("Data Source=163.178.107.10;Initial Catalog=WestOceanBeach;Persist Security Info=True;User ID=laboratorios;Password=KmZpo.2796;Pooling=False");
            sqlCommand = new SqlCommand();
        }

        public List<Habitacion> ObtenerHabitacionesDisponibles(int tipoHabitacion)
        {

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_buscarHabitaciones", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            // pendiente pasar por parámetro rango de fechas también
            sqlCommand.Parameters.AddWithValue("@tipoHabitacion", tipoHabitacion);
            sqlCommand.ExecuteNonQuery();

            List<Habitacion> Habitaciones = new List<Habitacion>();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    int numeroHabitacion = Convert.ToInt32(dt.Rows[i]["numero_habitacion"]);

                    Habitacion element = new Habitacion
                    {
                        NumeroHabitacion = numeroHabitacion
                    };

                    Habitaciones.Add(element);

                }

            };

            sqlConnection.Close();

            return Habitaciones;
        }

        public string obtenerHabitacionesTemporada()
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("sp_habitacionTemporada", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            string habitacionesTemporada = "";

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand)) //permite llenar registro base de datos
            {
                DataTable dt = new DataTable(); //representacion de una tabla base de datos 
                adapter.Fill(dt);//llena registro 

                string imagen1 = dt.Rows[0]["imagen"].ToString();
                string habitacion1 = dt.Rows[0]["HABITACION"].ToString();
                string precioTempBaja1 = dt.Rows[0]["PRECIOTEMPORADABAJA"].ToString();
                DateTime fechaIniBaja1 = Convert.ToDateTime(dt.Rows[0]["fechaIniciaBaja"]);
                DateTime fechaFinBaja1 = Convert.ToDateTime(dt.Rows[0]["fechaTerminaBaja"]);

                var fechaIniBaj1 = fechaIniBaja1.ToShortDateString();
                var fechaFnBaj1 = fechaFinBaja1.ToShortDateString();

                string precioTempAlta1 = dt.Rows[0]["PRECIOTEMPORADAALTA"].ToString();
                DateTime fechaIniAlta1 = Convert.ToDateTime(dt.Rows[0]["fechaIniciaAlta"]);
                DateTime fechaFinAlta1 = Convert.ToDateTime(dt.Rows[0]["fechaTerminaAlta"]);

                var fechaInAlta1 = fechaIniAlta1.ToShortDateString();
                var fechaFnAlta1 = fechaIniAlta1.ToShortDateString();

                string imagen2 = dt.Rows[1]["imagen"].ToString();
                string habitacion2 = dt.Rows[1]["HABITACION"].ToString();
                string precioTempBaja2 = dt.Rows[1]["PRECIOTEMPORADABAJA"].ToString();
                DateTime fechaIniBaja2 = Convert.ToDateTime(dt.Rows[1]["fechaIniciaBaja"]);

                DateTime fechaFinBaja2 = Convert.ToDateTime(dt.Rows[1]["fechaTerminaBaja"]);

                var fechaIniBaj2 = fechaIniBaja2.ToShortDateString();
                var fechaFnBaj2 = fechaFinBaja2.ToShortDateString();

                string precioTempAlta2 = dt.Rows[1]["PRECIOTEMPORADAALTA"].ToString();
                DateTime fechaIniAlta2 = Convert.ToDateTime(dt.Rows[1]["fechaIniciaAlta"]);
                DateTime fechaFinAlta2 = Convert.ToDateTime(dt.Rows[1]["fechaTerminaAlta"]);

                var fechaInAlta2 = fechaIniAlta2.ToShortDateString();
                var fechaFnAlta2 = fechaFinAlta2.ToShortDateString();


                string imagen3 = dt.Rows[2]["imagen"].ToString();
                string habitacion3 = dt.Rows[2]["HABITACION"].ToString();
                string precioTempBaja3 = dt.Rows[2]["PRECIOTEMPORADABAJA"].ToString();
                DateTime fechaIniBaja3 = Convert.ToDateTime(dt.Rows[2]["fechaIniciaBaja"]);

                DateTime fechaFinBaja3 = Convert.ToDateTime(dt.Rows[2]["fechaTerminaBaja"]);

                var fechaIniBaj3 = fechaIniBaja3.ToShortDateString();
                var fechaFnBaj3 = fechaFinBaja3.ToShortDateString();

                string precioTempAlta3 = dt.Rows[2]["PRECIOTEMPORADAALTA"].ToString();
                DateTime fechaIniAlta3 = Convert.ToDateTime(dt.Rows[2]["fechaIniciaAlta"]);
                DateTime fechaFinAlta3 = Convert.ToDateTime(dt.Rows[2]["fechaTerminaAlta"]);

                var fechaInAlta3 = fechaIniAlta3.ToShortDateString();
                var fechaFnAlta3 = fechaFinAlta3.ToShortDateString();

                habitacionesTemporada = ("info" + "#" + imagen1 + "#" + habitacion1 + "#" + precioTempBaja1 + "#" + fechaIniBaj1 + "#" + fechaFnBaj1 + "#" + precioTempAlta1 + "#" + fechaInAlta1 + "#" + fechaFnAlta2 + "#" + imagen2 + "#" + habitacion2 + "#" + precioTempBaja2 + "#" + fechaIniBaj2 + "#" + fechaFnBaj2 + "#" + precioTempAlta2 + "#" + fechaInAlta2 + "#" + fechaFnAlta2 + "#" + imagen3 + "#" + habitacion3 + "#" + precioTempBaja3 + "#" + fechaIniBaj3 + "#" + fechaFnBaj3 + "#" + precioTempAlta3 + "#" + fechaInAlta3 + "#" + fechaFnAlta3 + "#" + "info");

            };

            sqlConnection.Close();

            return habitacionesTemporada;

        }
    }
}