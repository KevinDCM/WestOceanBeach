using Entities.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AccessData.Data
{
   public class SitioGeneralData
    {

        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        public SitioGeneralData() {



            sqlConnection = new SqlConnection("Data Source=163.178.107.10;Initial Catalog=WestOceanBeach;Persist Security Info=True;User ID=laboratorios;Password=KmZpo.2796;Pooling=False");
            sqlCommand = new SqlCommand();



        }

        public SitioGeneral obtenerSitioGeneral()
        {
            


            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_obtenerSitioGeneral", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            SitioGeneral sitio= new SitioGeneral();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand)) //permite llenar registro base de datos
            {
                DataTable dt = new DataTable(); //representacion de una tabla base de datos 
                adapter.Fill(dt);//llena registro 
                Console.WriteLine("Estoy en data");

                
                string HOME = dt.Rows[0]["HOME"].ToString();
                string SOBRE_NOSOTROS = dt.Rows[0]["SOBRENOSOTROS"].ToString();
                string FACILIDADES = dt.Rows[0]["FACILIDADES"].ToString();
                string CONTACTO = dt.Rows[0]["CONTACTO"].ToString();
                string COMO_LLEGAR = "Puntarenas";
          
                    sitio.SOBRE_NOSOTROS= SOBRE_NOSOTROS;
                    sitio.HOME =HOME;
                    sitio.FACILIDADES=FACILIDADES;
                    sitio.CONTACTO=CONTACTO;
                    sitio.COMO_LLEGAR=COMO_LLEGAR;


               

            };
            sqlConnection.Close();

       




            return sitio;        
        
        
        }




    }
}
