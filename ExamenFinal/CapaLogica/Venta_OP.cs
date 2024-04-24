using Examen2_Programacion_II.Class;
using ExamenFinal.CapaDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ExamenFinal.Capa_Presentacion.Ventas;

namespace ExamenFinal.CapaLogica
{
    public class Venta_OP
    {
        public int AgregarVenta(ClsVentas ventas)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("GestionarVentas", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@accion", "agregar");
                    cmd.Parameters.AddWithValue("@venta_id", ventas.IDVenta);
                    cmd.Parameters.AddWithValue("@agente_id", ventas.IDAgente);
                    cmd.Parameters.AddWithValue("@cliente_id", ventas.IDCliente);
                    cmd.Parameters.AddWithValue("@casa_id", ventas.IDCasa);
                    cmd.Parameters.AddWithValue("@fecha", ventas.Fecha);
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
        public static int BorrarVenta(int idVenta)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("GestionarVentas", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@accion", "borrar");
                    cmd.Parameters.AddWithValue("@venta_id", idVenta);

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
        public static int ModificarVenta(int idVenta, int idAgente, int idCliente, int idCasa, DateTime Fecha)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("GestionarVentas", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@accion", "modificar");
                    cmd.Parameters.AddWithValue("@venta_id", idVenta);
                    cmd.Parameters.AddWithValue("@agente_id", idAgente);
                    cmd.Parameters.AddWithValue("@cliente_id", idCliente);
                    cmd.Parameters.AddWithValue("@casa_id", idCasa);
                    cmd.Parameters.AddWithValue("@fecha", Fecha);

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
        public static DataTable ConsultarVentaPorID(int idVenta)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GestionarVentas", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@accion", "consultar");
                cmd.Parameters.AddWithValue("@venta_id", idVenta);
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
    }//fin clasw
}//fin namespace