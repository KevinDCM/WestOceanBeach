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
            

                
                string HOME = dt.Rows[0]["HOME"].ToString();
                string SOBRE_NOSOTROS = dt.Rows[0]["SOBRENOSOTROS"].ToString();
                string CONTACTO = dt.Rows[0]["CONTACTO"].ToString();
                string COMO_LLEGAR = "Puntarenas";
          
                    sitio.SOBRE_NOSOTROS= SOBRE_NOSOTROS;
                    sitio.HOME =HOME;
                    sitio.FACILIDADES= "Prueba";
                    sitio.CONTACTO=CONTACTO;
                    sitio.COMO_LLEGAR=COMO_LLEGAR;

            };
            sqlConnection.Close();
            
           

            return sitio;        
        }// metodo

        public string editarFacilidades(SitioGeneral sitioGeneral)
        {
            string salida = "";
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_editarFacilidades", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("facilidades", sitioGeneral.FACILIDADES);

            sqlCommand.ExecuteNonQuery();
        
           sqlConnection.Close();

            return salida;
        }// metodo

        public SitioGeneral obtenerFacilidades()
        {
            
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_obtenerFacilidades", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            SitioGeneral sitio= new SitioGeneral();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataRow dr = dt.Rows[i];
                    string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                    ArrayList itm = new ArrayList(allColumns);

                    sitio.FACILIDADES = itm[0].ToString();
                }
            }

            return sitio;
        }// metodo

    }
}
