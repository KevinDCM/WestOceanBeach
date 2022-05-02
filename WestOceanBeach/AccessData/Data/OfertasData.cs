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
    public class OfertasData
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public OfertasData()
        {
            sqlConnection = new SqlConnection("Data Source=163.178.107.10;Initial Catalog=WestISTest;Persist Security Info=True;User ID=laboratorios;Password=KmZpo.2796;Pooling=False");
            sqlCommand = new SqlCommand();
        }

   
        public Ofertas OfertasActuales()
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_Ofertas_Recientes", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            Ofertas OFERTA = new Ofertas();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand)) //permite llenar registro base de datos
            {
                DataTable dt = new DataTable(); //representacion de una tabla base de datos 
                adapter.Fill(dt);//llena registro 
                Console.WriteLine("Estoy en data");

              
                int CANTIDAD_PERSONAS = Convert.ToInt32( dt.Rows[0]["CANTIDAD_PERSONAS"]);
                float DESCUENTO = Convert.ToInt32(dt.Rows[0]["DESCUENTO"]);
                DateTime FECHA_INICIO = Convert.ToDateTime(dt.Rows[0]["FECHA_INICIO"]);
                DateTime FECHA_FINAL =Convert.ToDateTime( dt.Rows[0]["FECHA_FINAL"]);


                OFERTA.cantidad_personas = CANTIDAD_PERSONAS;
                OFERTA.descuento = (int)Math.Ceiling( DESCUENTO);
                OFERTA.fecha_inicio = FECHA_INICIO;
                OFERTA.fecha_final = FECHA_FINAL;

              


            };
            sqlConnection.Close();






            return OFERTA;


        }




    }
}