using Entities.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AccessData.Data
{
    public class ReservaData
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public ReservaData()
        {
            sqlConnection = new SqlConnection("Data Source=163.178.107.10;Initial Catalog=WestOceanBeach;Persist Security Info=True;User ID=laboratorios;Password=Uy&)&nfC7QqQau.%278UQ24/=%;Pooling=False");
            sqlCommand = new SqlCommand();
        }

        public int ReservaConOferta(int id)
        {
            Int32 resultado = 0;

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_ValidarOferta", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            // parámetro: id de oferta
            sqlCommand.Parameters.AddWithValue("@paramidOferta",id);
            sqlCommand.ExecuteNonQuery();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {
                DataTable dt = new DataTable();
                dt.Dispose();
                adapter.Fill(dt);

                resultado = Convert.ToInt32(dt.Rows[0]["RESPONSE"]);
                // retorna un número de habitación disponible, o un No
            };

            sqlConnection.Close();
            return resultado;
        }

        public string RealizarReserva(Reserva reserva)
        {
            // call stored procedure here...
            string salida = "Reserva realizada!";

            sqlConnection.Open();
            sqlCommand = new SqlCommand("SP_RealizarReserva", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // parámetros del cliente
            sqlCommand.Parameters.AddWithValue("@param_CEDULA", reserva.Cedula);
            sqlCommand.Parameters.AddWithValue("@param_NOMBRE", reserva.Nombre);
            sqlCommand.Parameters.AddWithValue("@param_PRIMER_APELLIDO", reserva.PrimerApellido);
            sqlCommand.Parameters.AddWithValue("@param_SEGUNDO_APELLIDO", reserva.SegundoApellido);
            sqlCommand.Parameters.AddWithValue("@param_CORREO", reserva.Correo);
            sqlCommand.Parameters.AddWithValue("@param_TELEFONO", reserva.Telefono);
            sqlCommand.Parameters.AddWithValue("@param_EDAD", reserva.Edad);
            sqlCommand.Parameters.AddWithValue("@param_NACIONALIDAD", reserva.Nacionalidad);
            sqlCommand.Parameters.AddWithValue("@param_DIRECCION", reserva.Direccion);

            // parámetros de la reserva
            sqlCommand.Parameters.AddWithValue("@fechaI", reserva.fechaI);
            sqlCommand.Parameters.AddWithValue("@fechaF", reserva.fechaF);
            sqlCommand.Parameters.AddWithValue("@fechaIS", reserva.fechaIS);
            sqlCommand.Parameters.AddWithValue("@fechaFS", reserva.fechaFS);
            sqlCommand.Parameters.AddWithValue("@numHabitacion", reserva.NumHabitacion);
            sqlCommand.Parameters.AddWithValue("@param_IdOferta", reserva.IdOFerta);
            sqlCommand.Parameters.AddWithValue("@param_Temporada", reserva.Temporada);
            sqlCommand.Parameters.AddWithValue("@costototal", reserva.PrecioTotal);
            sqlCommand.Parameters.AddWithValue("@descuento", reserva.Descuento);


            sqlCommand.ExecuteNonQuery();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
            {




            };
            sqlConnection.Close();

            enviarCorreo(reserva.Correo, "Bienvenido a Hotel West Ocean Beach Resort",
                "Estimado(a) " + reserva.Nombre + " " + reserva.PrimerApellido + " " + reserva.SegundoApellido + ", esto es una notificación de reserva realizada, desde " + reserva.fechaIS + " hasta " + reserva.fechaFS + " donde ocupará la habitación #" + reserva.NumHabitacion + ". Su precio final fue de: $" + reserva.PrecioFinal);

            return salida;
        }

        public bool enviarCorreo(string destinatario, string asunto, string contenidoMsj)
        {

            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("2ea82bed3cdd10", "4024f0dc586c86"),
                EnableSsl = true
            };

            try
            {
                client.Send("bwestocean@gmail.com", destinatario, asunto, contenidoMsj);

                return true;
            }
            catch (SmtpException e)
            {
                Console.WriteLine("eror");
            }

            return false;


            /*

            // forma 1
             MailMessage message = new MailMessage();
             SmtpClient smtp = new SmtpClient();
             message.From = new MailAddress("bwestocean@gmail.com");
             message.To.Add(new MailAddress(correoEnviar));
             message.Subject = asunto;
             message.Body = contenidoMsj;
             smtp.Port = 587;
             smtp.Host = "smtp.gmail.com"; //for gmail host  
             smtp.EnableSsl = true;
             smtp.UseDefaultCredentials = false;
             smtp.Credentials = new NetworkCredential("bwestocean@gmail.com", "West.2022");
             smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


            // forma 2
             Mensaje mensaje = new Mensaje();
             mensaje.Para = correoEnviar;
             mensaje.De = "bwestocean@gmail.com";
             mensaje.Asunto = asunto;
             mensaje.Cuerpo = contenidoMsj;

             MailMessage ms = new MailMessage(mensaje.De, mensaje.Para);
             ms.Subject = mensaje.Asunto;
             ms.Body = mensaje.Cuerpo;

             SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);//protocolo de envio
             smtp.UseDefaultCredentials = false;

             smtp.Credentials = new NetworkCredential("bwestocean@gmail.com", "West.2022");
             smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
             smtp.EnableSsl = true;


            try
            {
                smtp.Send(message);
                return true;
            }
            catch (SmtpException e)
            {
                Console.WriteLine("eror");
            }
             */

        }
    }
}