using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Entities.Entities;

namespace AccessData.Data
{
    public class TemporadaData
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public TemporadaData()
        {
            sqlConnection = new SqlConnection("Data Source=163.178.107.10;Initial Catalog=WestOceanBeach;Persist Security Info=True;User ID=laboratorios;Password=Uy&)&nfC7QqQau.%278UQ24/=%;Pooling=False");
            sqlCommand = new SqlCommand();
        }// const

        public string obtenerTemporadas()
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_CONSULTAR_TEMPORADA", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            string temporadas = "";

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand)) //permite llenar registro base de datos
            {
                DataTable dt = new DataTable(); //representacion de una tabla base de datos 
                adapter.Fill(dt);//llena registro 

                string TIPO_TEMPORADA_01 = dt.Rows[0]["TIPO_TEMPORADA"].ToString();
                DateTime FECHA_INICIO_01= Convert.ToDateTime(dt.Rows[0]["FECHA_INICIO"]);
                DateTime FECHA_FINAL_01 = Convert.ToDateTime(dt.Rows[0]["FECHA_FINAL"]);

                var fechaIni1 = FECHA_INICIO_01.ToShortDateString();
                var fechaFn1 = FECHA_FINAL_01.ToShortDateString();

                   string TIPO_TEMPORADA_02 = dt.Rows[1]["TIPO_TEMPORADA"].ToString();
                DateTime FECHA_INICIO_02 = Convert.ToDateTime(dt.Rows[1]["FECHA_INICIO"]);
                DateTime FECHA_FINAL_02 = Convert.ToDateTime(dt.Rows[1]["FECHA_FINAL"]);

                var fechaIni2 = FECHA_INICIO_02.ToShortDateString();
                var fechaFn2 = FECHA_FINAL_02.ToShortDateString();

                 temporadas = ("info" + "#" + TIPO_TEMPORADA_01 + "#" + FECHA_INICIO_01 + "#" + FECHA_FINAL_01 + "#" + fechaIni1 + "#" + fechaFn1 + "#" + TIPO_TEMPORADA_02 + "#" + FECHA_INICIO_02 + "#" + FECHA_FINAL_02 + "#" + fechaIni2 + "#" + fechaFn2 +"#" + "info");

            };

            sqlConnection.Close();

            return temporadas;
            
        }


        public string EditarTemporada(Temporada temporada)
        {
            string salida = "No se logro realizar el cambio ";
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_ACTUALIZAR_TEMPORADA", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("TIPO_TEMPORADA", temporada.tipo_temporada);
            sqlCommand.Parameters.AddWithValue("FECHA_INICIO", temporada.fecha_inicio);
            sqlCommand.Parameters.AddWithValue("FECHA_FINAL", temporada.fecha_final);
            int rowAfected = sqlCommand.ExecuteNonQuery();
            if (rowAfected == 1)
            {

                salida = "Se edito la temporada  con exito!";
            }


            sqlConnection.Close();

            return salida;
        }// metodo


    }// fin clase

}// fin



