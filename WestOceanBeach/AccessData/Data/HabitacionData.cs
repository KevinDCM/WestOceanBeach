using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public string obtenerHabitacionTemporada()
        {



            sqlConnection.Open();
            sqlCommand = new SqlCommand("sp_habitacionTemporada", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            string habitacionesTemporada="";

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand)) //permite llenar registro base de datos
            {
                DataTable dt = new DataTable(); //representacion de una tabla base de datos 
                adapter.Fill(dt);//llena registro 
              


                string imagen1 = dt.Rows[0]["imagen"].ToString();
                string habitacion1 = dt.Rows[0]["HABITACION"].ToString();
                string precioTempBaja1 = dt.Rows[0]["PRECIOTEMPORADABAJA"].ToString();
                string fechaIniBaja1 = dt.Rows[0]["fechaIniciaBaja"].ToString();
                string fechaFinBaja1= dt.Rows[0]["fechaTerminaBaja"].ToString();
                
                string precioTempAlta1 = dt.Rows[0]["PRECIOTEMPORADAALTA"].ToString();
                string fechaIniAlta1 = dt.Rows[0]["fechaIniciaAlta"].ToString();
                string fechaFinAlta1 = dt.Rows[0]["fechaTerminaAlta"].ToString();
                


               string imagen2 = dt.Rows[1]["imagen"].ToString();
                string habitacion2 = dt.Rows[1]["HABITACION"].ToString();
                string precioTempBaja2 = dt.Rows[1]["PRECIOTEMPORADABAJA"].ToString();
                string fechaIniBaja2 = dt.Rows[1]["fechaIniciaBaja"].ToString();
                string fechaFinBaja2 = dt.Rows[1]["fechaTerminaBaja"].ToString();

                string precioTempAlta2 = dt.Rows[1]["PRECIOTEMPORADAALTA"].ToString();
                string fechaIniAlta2 = dt.Rows[1]["fechaIniciaAlta"].ToString();
                string fechaFinAlta2 = dt.Rows[1]["fechaTerminaAlta"].ToString();


                string imagen3 = dt.Rows[2]["imagen"].ToString();
                string habitacion3 = dt.Rows[2]["HABITACION"].ToString();
                string precioTempBaja3 = dt.Rows[2]["PRECIOTEMPORADABAJA"].ToString();
                string fechaIniBaja3 = dt.Rows[2]["fechaIniciaBaja"].ToString();
                string fechaFinBaja3 = dt.Rows[2]["fechaTerminaBaja"].ToString();

                string precioTempAlta3 = dt.Rows[2]["PRECIOTEMPORADAALTA"].ToString();
                string fechaIniAlta3 = dt.Rows[2]["fechaIniciaAlta"].ToString();
                string fechaFinAlta3 = dt.Rows[2]["fechaTerminaAlta"].ToString();



                habitacionesTemporada =(imagen1 + "#" + habitacion1 + "#" + precioTempBaja1 + "#" + fechaIniBaja1 + "#" + fechaFinBaja1 + "#" + precioTempAlta1 + "#" + fechaIniAlta1 + "#" + fechaFinAlta1+ "#" + imagen2 + "#" + habitacion2 + "#" + precioTempBaja2 + "#" + fechaIniBaja2 + "#" + fechaFinBaja2 + "#" + precioTempAlta2 + "#" + fechaIniAlta2 + "#" + fechaFinAlta2 + "#"+ imagen3 + "#" + habitacion3 + "#" + precioTempBaja3 + "#" + fechaIniBaja3 + "#" + fechaFinBaja3 + "#" + precioTempAlta3 + "#" + fechaIniAlta3 + "#" + fechaFinAlta3);
                

            };
            sqlConnection.Close();






            return habitacionesTemporada;


        }





    }
}
