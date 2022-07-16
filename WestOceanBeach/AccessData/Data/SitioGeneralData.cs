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

            sqlConnection = new SqlConnection("Data Source=163.178.107.10;Initial Catalog=WestOceanBeach;Persist Security Info=True;User ID=laboratorios;Password=Uy&)&nfC7QqQau.%278UQ24/=%;Pooling=False");
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
                string COMO_LLEGAR = dt.Rows[0]["COMOLLEGAR"].ToString();
          
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

        public string EditarComoLlegar(SitioGeneral sitioGeneral)
        {
            string salida = "No se logró editar  el apartado cómo lllegar ";

            sqlConnection.Open();
            sqlCommand = new SqlCommand("sp_update_ComoLlegar", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Como_llegar", sitioGeneral.COMO_LLEGAR);


            int rowAfected = sqlCommand.ExecuteNonQuery();
            if (rowAfected == 1)
            {

                salida = "Se editó el apartado Cómo llegar,  con éxito!";
            }


            sqlConnection.Close();

            return salida;
        }

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
            sqlCommand.Parameters.AddWithValue("full_path", Imagen.Full_path);
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

        public string Login(Login login)
        {
            string salida = "No";

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_Adminlogin", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // parámetros del cliente
            sqlCommand.Parameters.AddWithValue("@param_nombre", login.NombreUsuario);
            sqlCommand.Parameters.AddWithValue("@param_password", login.Password);

            sqlCommand.ExecuteNonQuery();

            List<Administrador> list = new List<Administrador>();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                dt.Dispose();
                adapter.Fill(dt);


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Administrador admin = new Administrador();

                    admin.NombreUsuario = Convert.ToString(dt.Rows[i]["NOMBRE_USUARIO"]);
                    admin.Password = Convert.ToString(dt.Rows[i]["PASSWORD"]);
                    admin.activo = Convert.ToBoolean(dt.Rows[i]["activo"]);

                    if(admin.NombreUsuario.Equals(login.NombreUsuario) 
                        && admin.Password.Equals(login.Password) 
                        && admin.activo)    // case sensisitive
                    {
                        return "again";// ya está activo y son sus credenciales, puede ingresar de nuevo
                    }
                    if(admin.activo 
                        && !(admin.NombreUsuario.Equals(login.NombreUsuario)) 
                        && !(admin.Password.Equals(login.Password))) 
                    { 
                        return "otro"; // hay otro admin activo
                    }

                    list.Add(admin);

                };

            };

            // si no hay ninguno activo
            // verificar credenciales
            for (int i = 0; i < list.Count; i++)
            {
                bool nombre = list[i].NombreUsuario.Equals(login.NombreUsuario);
                bool pass = list[i].Password.Equals(login.Password);

                if (nombre && pass)
                {
                    salida = "Si"; // inicia sesión
                    break;
                }
            

            }

            sqlConnection.Close();

            marcarAdminComoActivo(login.NombreUsuario, login.Password);

            return salida;

        }

        private void marcarAdminComoActivo(string nombreUsuario, string password)
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_AdminActiveSession", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // parámetros del cliente
            sqlCommand.Parameters.AddWithValue("@param_nombre", nombreUsuario);
            sqlCommand.Parameters.AddWithValue("@param_password", password);

            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public string Logout( )
        {
            string salida = "Ok";

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_Adminlogout", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


            sqlCommand.ExecuteNonQuery();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {


            };
            sqlConnection.Close();

            return salida;

        }

    }
}
