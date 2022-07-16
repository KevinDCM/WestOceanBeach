using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Entities.Entities;

namespace AccessData.Data
{
    public class OfertaData
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public OfertaData()
        {
            sqlConnection = new SqlConnection("Data Source=163.178.107.10;Initial Catalog=WestOceanBeach;Persist Security Info=True;User ID=laboratorios;Password=Uy&)&nfC7QqQau.%278UQ24/=%;Pooling=False");
            sqlCommand = new SqlCommand();
        }// const


        public string crearOfertaEspecial(Oferta oferta) {

            string printOutput = "";



            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_CREAR_OFERTA_ESPECIAL", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@DESCUENTO",oferta.descuento) ;
            sqlCommand.Parameters.AddWithValue("@FECHAINICIO", oferta.fecha_inicio);
            sqlCommand.Parameters.AddWithValue("@FECHAFINAL", oferta.fecha_final);
            sqlCommand.Parameters.AddWithValue("@TIPO_HABITACION", oferta.tipo_habitacion);
            //obtiene el print
            sqlConnection.InfoMessage += (object obj, SqlInfoMessageEventArgs e) => {
                printOutput += e.Message;
            };

            sqlCommand.ExecuteNonQuery();


            sqlConnection.Close();

            return printOutput;




        }

        public List<Oferta> consultaGeneralOfertasEspeciales() {


            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_CONSULTA_GENERAL_OFERTAS_ESPECIALES", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            List<Oferta> ofertas = new List<Oferta>();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand)) //permite llenar registro base de datos
            {
                DataTable dt = new DataTable(); //representacion de una tabla base de datos 
                adapter.Fill(dt);//llena registro 

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    
                    int IDOferta= Convert.ToInt32(dt.Rows[i]["ID_OFERTA"]);
                    float DESCUENTO = Convert.ToInt32(dt.Rows[i]["DESCUENTO"]);
                    DateTime FECHA_INICIO = Convert.ToDateTime(dt.Rows[i]["FECHA_INICIO"]);
                    DateTime FECHA_FINAL = Convert.ToDateTime(dt.Rows[i]["FECHA_FINAL"]);
                    string TIPOHABITACION = Convert.ToString(dt.Rows[i]["TIPO_HABITACION"]);

                    Oferta OFERTA = new Oferta();
                    OFERTA.Id= IDOferta;
                    OFERTA.cantidad_dias = 0;
                    OFERTA.descuento = (int)Math.Ceiling(DESCUENTO);
                    OFERTA.fecha_ini = FECHA_INICIO.ToString("dd/MM/yyyy");
                    OFERTA.fecha_fin = FECHA_FINAL.ToString("dd/MM/yyyy");
                    OFERTA.tipo_habitacion = TIPOHABITACION;

                    ofertas.Add(OFERTA);

                }

            };
            sqlConnection.Close();

            return ofertas;





        }

        public List<Oferta> consultaGeneralOfertasEspecialesTipoHabitacion(Oferta oferta) {


            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_CONSULTA_X_HABITACION_OFERTAS_ESPECIALES", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TipoHabitacion",oferta.tipo_habitacion);

            sqlCommand.ExecuteNonQuery();
            List<Oferta> ofertas = new List<Oferta>();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand)) //permite llenar registro base de datos
            {
                DataTable dt = new DataTable(); //representacion de una tabla base de datos 
                adapter.Fill(dt);//llena registro 

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    int IDOferta = Convert.ToInt32(dt.Rows[i]["ID_OFERTA"]);
                    float DESCUENTO = Convert.ToInt32(dt.Rows[i]["DESCUENTO"]);
                    DateTime FECHA_INICIO = Convert.ToDateTime(dt.Rows[i]["FECHA_INICIO"]);
                    DateTime FECHA_FINAL = Convert.ToDateTime(dt.Rows[i]["FECHA_FINAL"]);
                    string TIPOHABITACION = Convert.ToString(dt.Rows[i]["TIPO_HABITACION"]);

                    Oferta OFERTA = new Oferta();
                    OFERTA.Id = IDOferta;
                    OFERTA.cantidad_dias = 0;
                    OFERTA.descuento = (int)Math.Ceiling(DESCUENTO);
                    OFERTA.fecha_ini = FECHA_INICIO.ToString("dd/MM/yyyy");
                    OFERTA.fecha_fin = FECHA_FINAL.ToString("dd/MM/yyyy");
                    OFERTA.tipo_habitacion = TIPOHABITACION;

                    ofertas.Add(OFERTA);

                }

            };
            sqlConnection.Close();

            return ofertas;



        }




        public string eliminarOfertaEspecial(Oferta oferta) {


            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_ELIMINAR_ACT_OFERTAS_ESPECIALES", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@ID_OFERTA", oferta.Id);

            sqlCommand.ExecuteNonQuery();


            sqlConnection.Close();


            return "Eliminado correctamente.";
        }




        public string actualizarOfertaEspecial(Oferta oferta) {


            string printOutput = "";
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_ACT_OFERTA_ESPECIAL", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@ID_OFERTA", oferta.Id);
            sqlCommand.Parameters.AddWithValue("@DESCUENTO", oferta.descuento);
            sqlCommand.Parameters.AddWithValue("@FECHAINICIO", oferta.fecha_inicio);
            sqlCommand.Parameters.AddWithValue("@FECHAFINAL", oferta.fecha_final);
            sqlCommand.Parameters.AddWithValue("@TIPO_HABITACION", oferta.tipo_habitacion);
            sqlConnection.InfoMessage += (object obj, SqlInfoMessageEventArgs e) => {
                printOutput += e.Message;
            };
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return printOutput;


        }






        























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

                    //int CANTIDAD_PERSONAS = Convert.ToInt32(dt.Rows[i]["CANTIDAD_PERSONAS"]);
                    float DESCUENTO = Convert.ToInt32(dt.Rows[i]["DESCUENTO"]);
                    DateTime FECHA_INICIO = Convert.ToDateTime(dt.Rows[i]["FECHA_INICIO"]);
                    DateTime FECHA_FINAL = Convert.ToDateTime(dt.Rows[i]["FECHA_FINAL"]);
                    string TIPOHABITACION = Convert.ToString(dt.Rows[i]["TIPO_HABITACION"]);
                    int cantDias = Convert.ToInt32(dt.Rows[i]["CANTIDAD_DIAS"]);

                    Oferta OFERTA = new Oferta();
                    OFERTA.cantidad_dias = cantDias;
                    OFERTA.descuento = (int)Math.Ceiling(DESCUENTO);
                    OFERTA.fecha_inicio = FECHA_INICIO;
                    OFERTA.fecha_final = FECHA_FINAL;
                    OFERTA.tipo_habitacion = TIPOHABITACION;

                    ofertas.Add(OFERTA);

                }

            };
            sqlConnection.Close();

            return ofertas;


        }//obtenerOfertasSobresalientes


        public float GetDescuentoOferta(int id)
        {
            float descuentor = 0;

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_getDescuentoOferta", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@idOferta", id);

            sqlCommand.ExecuteNonQuery();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                float descuento = (float)Convert.ToInt32(dt.Rows[0]["DESCUENTO"]);

                descuentor = (int)descuento;

            };

            sqlConnection.Close();

            return descuentor;
        }


    }// fin clase

}// fin



