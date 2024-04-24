using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ExamenFinal.CapaDatos;
using Examen2_Programacion_II.Class;
using System.Configuration;

namespace ExamenFinal.CapaLogica
{
    public class Agente_OP
    {
        public int AgregarAgente(ClsAgente agente)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("GestionarAgentes", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@accion", "agregar");
                    cmd.Parameters.AddWithValue("@agente_nombre", agente.Nombre);
                    cmd.Parameters.AddWithValue("@agente_email", agente.Email);
                    cmd.Parameters.AddWithValue("@agente_telefono", agente.Telefono);
                    cmd.Parameters.AddWithValue("@agente_contrasena", agente.Contrasena);

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
                Conn.Dispose();
            }
            return retorno;
        }
        public static int BorrarAgente(int idAgente)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("GestionarAgentes", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@accion", "borrar");
                    cmd.Parameters.AddWithValue("@agente_id", idAgente);

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
                Conn.Dispose();
            }
            return retorno;
        }
        public static int ModificarAgente(int idagente, string nombre, string email, string telefono, string password)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("GestionarAgentes", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@accion", "modificar");
                    cmd.Parameters.AddWithValue("@agente_id", idagente);
                    cmd.Parameters.AddWithValue("@agente_nombre", nombre);
                    cmd.Parameters.AddWithValue("@agente_email", email);
                    cmd.Parameters.AddWithValue("@agente_telefono", telefono);
                    cmd.Parameters.AddWithValue("@agente_contrasena", password);

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
                Conn.Dispose();
            }
            return retorno;
        }
        public static DataTable ConsultarAgentePorID(int idAgente)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GestionarAgentes", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@accion", "consultar");
                cmd.Parameters.AddWithValue("@agente_id", idAgente);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
    }

}
