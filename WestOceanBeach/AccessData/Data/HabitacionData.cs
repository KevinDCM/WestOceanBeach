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
    public class HabitacionData
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        public HabitacionData()
        {
            try
            {
                sqlConnection = new SqlConnection("Data Source=163.178.107.10;Initial Catalog=WestOceanBeach;Persist Security Info=True;User ID=laboratorios;Password=Uy&)&nfC7QqQau.%278UQ24/=%;Pooling=False");
                sqlCommand = new SqlCommand();
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("{0} First exception caught.", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Last exception caught.", ex.Message);
            }
           
        }

        public List<Habitacion> ObtenerHabitacionesDisponiblesPorFechaTipo(Habitacion habitacion)
        {

          
                DateTime Inicio = Convert.ToDateTime(habitacion.fechaIS);
                DateTime Final = Convert.ToDateTime(habitacion.fechaFS);

                sqlConnection.Open();
                sqlCommand = new SqlCommand("SP_Habitaciones_Disponibles_Tipo", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@Inicio", Inicio);
                sqlCommand.Parameters.AddWithValue("@Final", Final);
                sqlCommand.Parameters.AddWithValue("@tipo", habitacion.TipoHabitacion);


                sqlCommand.ExecuteNonQuery();
                List<Habitacion> habitaciones = new List<Habitacion>();
                try
                {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Habitacion temp = new Habitacion();


                        temp.NumeroHabitacion = Convert.ToInt32(dt.Rows[i]["NUMERO_HABITACION"]);
                        temp.TipoHabitacion = dt.Rows[i]["NOMBRE_TIPO"].ToString();
                        temp.TarifaDiaria = Convert.ToDecimal((dt.Rows[i]["TARIFA_DIARIA"]));



                        habitaciones.Add(temp);

                    }

                };

                sqlConnection.Close();

               

            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("{0} First exception caught.", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Last exception caught.", ex.Message);
            }

            return habitaciones;
        }

        public float ObtenerTarifaDiaria(int tipoHabitacion)
        {
            // call stored procedure here...
            float result = 0;
         try
           {
               
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_getMontoACancelar", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // adjuntar parámetros del procedimiento almacenado
            sqlCommand.Parameters.AddWithValue("@tipoHabitacion", tipoHabitacion);

            sqlCommand.ExecuteNonQuery();
            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                {
                    DataTable dt = new DataTable();
                    dt.Dispose();
                    adapter.Fill(dt);

                    result = (float)Convert.ToDouble(dt.Rows[0]["TARIFA_DIARIA"]);

                };

                sqlConnection.Close();
            }
            catch (SqlException exp)
            {
               
                throw new InvalidOperationException("Data could not be read", exp);
            }
            

            return result;
        }

        public string ValidarHabitacionDisponible(Habitacion habitacion)
        {
             string salida = "No";
            try
            {
                // call stored procedure here...
               

                sqlConnection.Open();
                sqlCommand = new SqlCommand("SP_ValidarHabitacionDisponible", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                // adjuntar parámetros del procedimiento almacenado
                sqlCommand.Parameters.AddWithValue("@param_fechaIS", habitacion.fechaIS);
                sqlCommand.Parameters.AddWithValue("@param_fechaFS", habitacion.fechaFS);
                sqlCommand.Parameters.AddWithValue("@numHabitacion", habitacion.NumeroHabitacion);

                sqlCommand.ExecuteNonQuery();

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                {
                    DataTable dt = new DataTable();
                    dt.Dispose();
                    adapter.Fill(dt);

                    salida = Convert.ToString(dt.Rows[0]["RESPONSE"]);

                };

                sqlConnection.Close();

            }
            catch (SqlException exp)
            {
                // Log what you need from here.
                throw new InvalidOperationException("Data could not be read", exp);
            }
          
            return salida;
        }

        public List<Habitacion> ObtenerHabitacionesDisponiblesPorFecha(Habitacion habitacion)
        {
            List<Habitacion> habitaciones;
            try
            {
                DateTime Inicio = Convert.ToDateTime(habitacion.fechaIS);
                DateTime Final = Convert.ToDateTime(habitacion.fechaFS);

                sqlConnection.Open();
                sqlCommand = new SqlCommand("SP_Habitaciones_Disponibles_General", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@Inicio", Inicio);
                sqlCommand.Parameters.AddWithValue("@Final", Final);



                sqlCommand.ExecuteNonQuery();
                habitaciones = new List<Habitacion>();

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                {

                    DataTable dt = new DataTable();
                    dt.Dispose();

                    adapter.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Habitacion temp = new Habitacion();


                        temp.NumeroHabitacion = Convert.ToInt32(dt.Rows[i]["NUMERO_HABITACION"]);
                        temp.TipoHabitacion = dt.Rows[i]["NOMBRE_TIPO"].ToString();
                        temp.TarifaDiaria = Convert.ToDecimal((dt.Rows[i]["TARIFA_DIARIA"]));



                        habitaciones.Add(temp);

                    }


                };

                sqlConnection.Close();
            }
            catch (SqlException exp)
            {
                // Log what you need from here.
                throw new InvalidOperationException("Data could not be read", exp);
            }
           
         
            return habitaciones;

        }

        public List<Habitacion> ObtenerHabitacionesDisponibles(int tipoHabitacion)
        {

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_buscarHabitaciones", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


            sqlCommand.Parameters.AddWithValue("@tipoHabitacion", tipoHabitacion);
            sqlCommand.ExecuteNonQuery();

            List<Habitacion> Habitaciones = new List<Habitacion>();


            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    int numeroHabitacion = Convert.ToInt32(dt.Rows[i]["numero_habitacion"]);

                    Habitacion element = new Habitacion
                    {
                        NumeroHabitacion = numeroHabitacion
                    };

                    Habitaciones.Add(element);

                }

            };

            sqlConnection.Close();
            return Habitaciones;
        }

        public string obtenerHabitacionesTemporada()
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("sp_habitacionTemporada", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            string habitacionesTemporada = "";

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand)) //permite llenar registro base de datos
            {
                DataTable dt = new DataTable(); //representacion de una tabla base de datos 
                adapter.Fill(dt);//llena registro 

                string imagen1 = dt.Rows[0]["imagen"].ToString();
                string habitacion1 = dt.Rows[0]["HABITACION"].ToString();
                string precioTempBaja1 = dt.Rows[0]["PRECIOTEMPORADABAJA"].ToString();
                DateTime fechaIniBaja1 = Convert.ToDateTime(dt.Rows[0]["fechaIniciaBaja"]);
                DateTime fechaFinBaja1 = Convert.ToDateTime(dt.Rows[0]["fechaTerminaBaja"]);

                var fechaIniBaj1 = fechaIniBaja1.ToShortDateString();
                var fechaFnBaj1 = fechaFinBaja1.ToShortDateString();

                string precioTempAlta1 = dt.Rows[0]["PRECIOTEMPORADAALTA"].ToString();
                DateTime fechaIniAlta1 = Convert.ToDateTime(dt.Rows[0]["fechaIniciaAlta"]);
                DateTime fechaFinAlta1 = Convert.ToDateTime(dt.Rows[0]["fechaTerminaAlta"]);

                var fechaInAlta1 = fechaIniAlta1.ToShortDateString();
                var fechaFnAlta1 = fechaIniAlta1.ToShortDateString();

                string imagen2 = dt.Rows[1]["imagen"].ToString();
                string habitacion2 = dt.Rows[1]["HABITACION"].ToString();
                string precioTempBaja2 = dt.Rows[1]["PRECIOTEMPORADABAJA"].ToString();
                DateTime fechaIniBaja2 = Convert.ToDateTime(dt.Rows[1]["fechaIniciaBaja"]);

                DateTime fechaFinBaja2 = Convert.ToDateTime(dt.Rows[1]["fechaTerminaBaja"]);

                var fechaIniBaj2 = fechaIniBaja2.ToShortDateString();
                var fechaFnBaj2 = fechaFinBaja2.ToShortDateString();

                string precioTempAlta2 = dt.Rows[1]["PRECIOTEMPORADAALTA"].ToString();
                DateTime fechaIniAlta2 = Convert.ToDateTime(dt.Rows[1]["fechaIniciaAlta"]);
                DateTime fechaFinAlta2 = Convert.ToDateTime(dt.Rows[1]["fechaTerminaAlta"]);

                var fechaInAlta2 = fechaIniAlta2.ToShortDateString();
                var fechaFnAlta2 = fechaFinAlta2.ToShortDateString();

                string imagen3 = dt.Rows[2]["imagen"].ToString();
                string habitacion3 = dt.Rows[2]["HABITACION"].ToString();
                string precioTempBaja3 = dt.Rows[2]["PRECIOTEMPORADABAJA"].ToString();
                DateTime fechaIniBaja3 = Convert.ToDateTime(dt.Rows[2]["fechaIniciaBaja"]);

                DateTime fechaFinBaja3 = Convert.ToDateTime(dt.Rows[2]["fechaTerminaBaja"]);

                var fechaIniBaj3 = fechaIniBaja3.ToShortDateString();
                var fechaFnBaj3 = fechaFinBaja3.ToShortDateString();

                string precioTempAlta3 = dt.Rows[2]["PRECIOTEMPORADAALTA"].ToString();
                DateTime fechaIniAlta3 = Convert.ToDateTime(dt.Rows[2]["fechaIniciaAlta"]);
                DateTime fechaFinAlta3 = Convert.ToDateTime(dt.Rows[2]["fechaTerminaAlta"]);

                var fechaInAlta3 = fechaIniAlta3.ToShortDateString();
                var fechaFnAlta3 = fechaFinAlta3.ToShortDateString();

                habitacionesTemporada = ("info" + "#" + imagen1 + "#" + habitacion1 + "#" + precioTempBaja1 + "#" + fechaIniBaj1 + "#" + fechaFnBaj1 + "#" + precioTempAlta1 + "#" + fechaInAlta1 + "#" + fechaFnAlta2 + "#" + imagen2 + "#" + habitacion2 + "#" + precioTempBaja2 + "#" + fechaIniBaj2 + "#" + fechaFnBaj2 + "#" + precioTempAlta2 + "#" + fechaInAlta2 + "#" + fechaFnAlta2 + "#" + imagen3 + "#" + habitacion3 + "#" + precioTempBaja3 + "#" + fechaIniBaj3 + "#" + fechaFnBaj3 + "#" + precioTempAlta3 + "#" + fechaInAlta3 + "#" + fechaFnAlta3 + "#" + "info");

            };

            sqlConnection.Close();

            return habitacionesTemporada;

        }


        public Habitacion Habitacion_Junior()
        {
            Habitacion tipo_junior;
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SP_HABITACIONES_JUNIOR", sqlConnection);
                sqlCommand.ExecuteNonQuery();
                 tipo_junior = new Habitacion();

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                        ArrayList itm = new ArrayList(allColumns);

                        string Descripcion = Convert.ToString(dt.Rows[i]["DESCRIPCION"]);
                        string Ruta_Imagen = Convert.ToString(dt.Rows[i]["RUTA_IMAGEN"]);
                        decimal TarifaDiaria = Convert.ToDecimal(dt.Rows[i]["TARIFA_DIARIA"]);

                        tipo_junior.Descripcion = Descripcion;
                        tipo_junior.ruta_imagen = Ruta_Imagen;
                        tipo_junior.TarifaDiaria = TarifaDiaria;

                    }
                }
                sqlConnection.Close();
            }
            catch (SqlException exp)
            {
                // Log what you need from here.
                throw new InvalidOperationException("Mensaje de error:", exp);
            }
            

            return tipo_junior;
        }// metodo



        public Habitacion Habitacion_Suite()
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("[SP_HABITACIONES_SUITE]", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            Habitacion tipo_suite = new Habitacion();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                    ArrayList itm = new ArrayList(allColumns);

                    string Descripcion = Convert.ToString(dt.Rows[i]["DESCRIPCION"]);
                    string Ruta_Imagen = Convert.ToString(dt.Rows[i]["RUTA_IMAGEN"]);
                    decimal TarifaDiaria = Convert.ToDecimal(dt.Rows[i]["TARIFA_DIARIA"]);

                    tipo_suite.Descripcion = Descripcion;
                    tipo_suite.ruta_imagen = Ruta_Imagen;
                    tipo_suite.TarifaDiaria = TarifaDiaria;

                }
            }
            sqlConnection.Close();

            return tipo_suite;
        }// metodo


        public Habitacion Habitacion_Estandar()
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("[SP_HABITACIONES_ESTANDAR]", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            Habitacion tipo_estandar = new Habitacion();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataRow dr = dt.Rows[i];
                    string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                    ArrayList itm = new ArrayList(allColumns);

                    string Descripcion = Convert.ToString(dt.Rows[i]["DESCRIPCION"]);
                    string Ruta_Imagen = Convert.ToString(dt.Rows[i]["RUTA_IMAGEN"]);
                    decimal TarifaDiaria = Convert.ToDecimal(dt.Rows[i]["TARIFA_DIARIA"]);

                    tipo_estandar.Descripcion = Descripcion;
                    tipo_estandar.ruta_imagen = Ruta_Imagen;
                    tipo_estandar.TarifaDiaria = TarifaDiaria;

                }
            }
            sqlConnection.Close();

            return tipo_estandar;
        }// metodo

        public string editarHabitacion(Habitacion habitacion)
        {
            string salida;
           
             salida = "No se logro editar la habitacion ";
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_update_tipoHabitacion", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("tarifa", decimal.Parse(habitacion.Tarifa));
            sqlCommand.Parameters.AddWithValue("descripcion", habitacion.Descripcion);
            sqlCommand.Parameters.AddWithValue("nombre", habitacion.TipoHabitacion);
 
            if (sqlCommand.ExecuteNonQuery() == 1)
            {

                salida = "Se edito el tipo de habitacion con exito!";
            }

            sqlConnection.Close();

           
            return salida;
        }//editarHabitacion

        public string editarHabitacionImagen(Habitacion habitacion)
        {
            string salida = "No se logro editar la habitacion ";
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_update_tipoHabitacionImagen", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("ruta", habitacion.ruta_imagen);
            sqlCommand.Parameters.AddWithValue("nombre", habitacion.TipoHabitacion);

            int rowAfected = sqlCommand.ExecuteNonQuery();
            if (rowAfected == 1)
            {
                salida = "Se edito la imagen correctamente!";
            }// if

            sqlConnection.Close();

            return salida;
        }//editarHabitacion

        public List<Habitacion> estadoActualHabitacion()
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("estado_actual_habitacion", sqlConnection);
            sqlCommand.ExecuteNonQuery();
          
            List<Habitacion> habitaciones = new List<Habitacion>();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    int numero = Convert.ToInt32(dt.Rows[i]["Numero"]);
                    string tipo = Convert.ToString(dt.Rows[i]["Tipo"]);
                    string estado = Convert.ToString(dt.Rows[i]["Estado"]);
                    List<string> tempList = new List<string>();

                    Habitacion habitacionTemp = new Habitacion();
                    habitacionTemp.NumeroHabitacion = numero;
                    habitacionTemp.TipoHabitacion = tipo;
                    habitacionTemp.Encuentra= estado;
                    habitaciones.Add(habitacionTemp);

                }
            }
            sqlConnection.Close();
            return habitaciones;

        }// metodo

    }
}