using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Entities.Entities;

namespace AccessData.Data
{
    public class OfertData
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public OfertData()
        {
            sqlConnection = new SqlConnection("Data Source=163.178.107.10;Initial Catalog=WestOceanBeach;Persist Security Info=True;User ID=laboratorios;Password=KmZpo.2796;Pooling=False");
            sqlCommand = new SqlCommand();
        }// const

        public List<Oferta> obtenerOfertaSobresaliente()
        {

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_ofertasSobresalientes", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            List<Oferta> ofertas= new List<Oferta>();

            

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand)) //permite llenar registro base de datos
            {
                DataTable dt = new DataTable(); //representacion de una tabla base de datos 
                adapter.Fill(dt);//llena registro 


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    int CANTIDAD_PERSONAS = Convert.ToInt32(dt.Rows[i]["CANTIDAD_PERSONAS"]);
                    float DESCUENTO = Convert.ToInt32(dt.Rows[i]["DESCUENTO"]);
                    DateTime FECHA_INICIO = Convert.ToDateTime(dt.Rows[i]["FECHA_INICIO"]);
                    DateTime FECHA_FINAL = Convert.ToDateTime(dt.Rows[i]["FECHA_FINAL"]);

                    Oferta OFERTA = new Oferta();
                    OFERTA.cantidad_personas = CANTIDAD_PERSONAS;
                    OFERTA.descuento = (int)Math.Ceiling(DESCUENTO);
                    OFERTA.fecha_inicio = FECHA_INICIO;
                    OFERTA.fecha_final = FECHA_FINAL;

                    ofertas.Add(OFERTA);
                    /*int alt = Convert.ToInt32(dt.Rows[i]["CANTIDAD_PERSONAS"];

                    ofertas[i].cantidad_personas = Convert.ToInt32(dt.Rows[i]["CANTIDAD_PERSONAS"]);

                    ofertas[i].descuento = Convert.ToInt32(dt.Rows[i]["DESCUENTO"]);
                    ofertas[i].fecha_inicio = Convert.ToDateTime(dt.Rows[i]["FECHA_INICIO"]);
                    ofertas[i].fecha_final = Convert.ToDateTime(dt.Rows[0]["FECHA_FINAL"]);
                    */




                }

            };
            sqlConnection.Close();






            return ofertas;


        }//obtenerOfertasSobresalientes

    }// fin clase

}// fin



