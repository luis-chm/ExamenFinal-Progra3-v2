using Examen2_Programacion_II.Class;
using ExamenFinal.CapaDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ExamenFinal.Capa_Presentacion.Clientes;

namespace ExamenFinal.CapaLogica
{
    public class Cliente_OP
    {
        public int AgregarCliente(ClsCliente cliente)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("GestionarClientes", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@accion", "agregar");
                    cmd.Parameters.AddWithValue("@cliente_nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@cliente_email", cliente.Email);
                    cmd.Parameters.AddWithValue("@cliente_telefono", cliente.Telefono);

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
        public static int BorrarCliente(int idCliente)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("GestionarClientes", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@accion", "borrar");
                    cmd.Parameters.AddWithValue("@cliente_id", idCliente);

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
        public static int ModificarCliente(int idCliente, string nombre, string email, string telefono)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("GestionarClientes", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@accion", "modificar");
                    cmd.Parameters.AddWithValue("@cliente_id", idCliente);
                    cmd.Parameters.AddWithValue("@cliente_nombre", nombre);
                    cmd.Parameters.AddWithValue("@cliente_email", email);
                    cmd.Parameters.AddWithValue("@cliente_telefono", telefono);


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
        public static DataTable ConsultarClientePorID(int idCliente)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GestionarClientes", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@accion", "consultar");
                cmd.Parameters.AddWithValue("@cliente_id", idCliente);
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



    }//fin clase
}//fin namespace