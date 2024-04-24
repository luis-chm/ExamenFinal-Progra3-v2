using Examen2_Programacion_II.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using ExamenFinal.CapaDatos;

namespace ExamenFinal.CapaLogica
{
    public class Login_OP
    {
        public static int ValidarLogin(ClsAgente agente)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ValidarLogin", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@email", agente.Email));
                    cmd.Parameters.Add(new SqlParameter("@contrasena", agente.Contrasena));

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            retorno = 1;
                           agente.IDAgente = rdr.GetInt32(0);
                           agente.Nombre = rdr[1].ToString();
                           agente.Email = rdr[2].ToString();
                        }
                        else
                        {
                            retorno = -1;
                        }
                    }
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
    }
}