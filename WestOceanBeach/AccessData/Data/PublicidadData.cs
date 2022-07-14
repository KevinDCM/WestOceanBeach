using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AccessData.Data
{
    public class PublicidadData
    {

        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public PublicidadData()
        {
            sqlConnection = new SqlConnection("Data Source=163.178.107.10;Initial Catalog=WestOceanBeach;Persist Security Info=True;User ID=laboratorios;Password=Uy&)&nfC7QqQau.%278UQ24/=%;Pooling=False");
            //sqlCommand = new SqlCommand();
        }

        public string Edit(Publicidad publicidad)
        {
            sqlConnection.Close();
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_ActivarPublicidad", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@param_nombre", publicidad.NombreEmpresa);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();


            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_DesactivarPublicidad", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@param_nombre", publicidad.NombreEmpresa2);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return "Ok";
        }

        public List<Publicidad> GetAll()
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_GetAllPublicidad", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.ExecuteNonQuery();
            List<Publicidad> publicidad = new List<Publicidad>();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string nombre_empresa = Convert.ToString(dt.Rows[i]["NOMBRE_EMPRESA"]);
                    Boolean activo = Convert.ToBoolean(dt.Rows[i]["ACTIVO"]);

                    Publicidad element = new Publicidad();
                    element.NombreEmpresa = nombre_empresa;

                    if(activo)
                        element.Activo = 1;
                    else
                        element.Activo = 0;

                    publicidad.Add(element);

                }

            };

            sqlConnection.Close();

            return publicidad;
        }

        public List<Publicidad> getPublicidadActiva()
        {

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_GetPublicidadActiva", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            List<Publicidad> publicidad = new List<Publicidad>();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt); 

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string nombre_empresa = Convert.ToString(dt.Rows[i]["NOMBRE_EMPRESA"]);
                    string ruta_imagen = Convert.ToString(dt.Rows[i]["RUTA_IMAGEN"]);
                    string site_url = Convert.ToString(dt.Rows[i]["SITE_URL"]);

                    Publicidad element = new Publicidad();
                    element.NombreEmpresa = nombre_empresa;
                    element.RutaImagen = ruta_imagen;
                    element.SiteUrl = site_url;

                    publicidad.Add(element);

                }

            };

            sqlConnection.Close();

            return publicidad;

        }
    }
}