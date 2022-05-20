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

        public SitioGeneral obtenerHome()
        {

            sqlConnection.Open();
            sqlCommand = new SqlCommand("[SP_obtenerHome]", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            SitioGeneral sitio = new SitioGeneral();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataRow dr = dt.Rows[i];
                    string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                    ArrayList itm = new ArrayList(allColumns);

                    sitio.HOME = itm[0].ToString();
                }
            }

            return sitio;
        }// metodo

        public SitioGeneral obtenerFacilidades()
        {

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_obtenerFacilidades", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            SitioGeneral sitio = new SitioGeneral();

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

        public List<Imagenes> ObtenerImagenesHome()
        {

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_obtenerImgHome", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            List<Imagenes> imagenes = new List<Imagenes>();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string full_path = Convert.ToString(dt.Rows[i]["full_path"]);

                    Imagenes element = new Imagenes();
                    element.Full_path = full_path;

                    imagenes.Add(element);

                }

            };

            sqlConnection.Close();

            return imagenes;

        }
        public string editarFacilidades(SitioGeneral sitioGeneral)
        {
            string salida = "No se logro editar  el apartado facilidades ";
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_editarFacilidades", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("facilidades", sitioGeneral.FACILIDADES);


            int rowAfected = sqlCommand.ExecuteNonQuery();
            if (rowAfected == 1)
            {

                salida = "Se edito el apartado facilidades  con exito!";
            }


            sqlConnection.Close();

            return salida;
        }// metodo

        public string editarSobreNosotros(SitioGeneral sitio)
        {
            string salida = "No se logro editar  el apartado sobreNosotros ";
            sqlConnection.Open();
            sqlCommand = new SqlCommand("sp_update_SobreNosotros", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("SOBRENOSOTROS", sitio.SOBRE_NOSOTROS);


           int rowAfected = sqlCommand.ExecuteNonQuery();
           if (rowAfected == 1) {

                salida = "Se edito el apartado Acerca de  con exito!";
            }
            

            sqlConnection.Close();

            return salida;
        }// metodo



        public string editarRutaImgHome(Imagenes Imagen)
        {
            string salida = "No se logro editar la ruta de la imagen principal del Home ";
            sqlConnection.Open();
            sqlCommand = new SqlCommand("[Update_Imagenes]", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("ImgHome", Imagen.Name);


            int rowAfected = sqlCommand.ExecuteNonQuery();
            if (rowAfected == 1)
            {

                salida = "Se realizo el cambio de imagen con exito!";
            }


            sqlConnection.Close();

            return salida;
        }// metodo


        public string EditarHome(SitioGeneral home)
        {
            string salida = "No se logro editar  el apartado de Home ";
            sqlConnection.Open();
            sqlCommand = new SqlCommand("sp_update_Home", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("home",home.HOME);


            int rowAfected = sqlCommand.ExecuteNonQuery();
            if (rowAfected == 1)
            {

                salida = "Se edito el apartado de Home  con exito!";
            }


            sqlConnection.Close();

            return salida;
        }// metodo

    }
}
