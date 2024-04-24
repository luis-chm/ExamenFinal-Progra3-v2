using ExamenFinal.CapaDatos;
using ExamenFinal.CapaLogica;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenFinal.Capa_Presentacion.Casas
{
    public partial class Casas_View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarGridCasas();
        }
        public void alertas(String texto)
        {
            string message = texto;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }
        protected void LlenarGridCasas()
        {
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                string query = "LlenarGridCasas";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gridCasas.DataSource = dt;
                            gridCasas.DataBind();
                        }
                    }
                }
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(tDireccion.Text) ||
                string.IsNullOrWhiteSpace(tCiudad.Text) ||
                string.IsNullOrWhiteSpace(tPrecio.Text))
            {
                alertas("Por favor, complete todos los campos.");
                return;
            }

            try
            {
                ClsCasa casa = new ClsCasa();
                casa.Direccion = tDireccion.Text;
                casa.Ciudad = tCiudad.Text;
                casa.Precio = decimal.Parse(tPrecio.Text);

                Casa_OP casa_OP = new Casa_OP();
                int resultado = casa_OP.AgregarCasa(casa);

                if (resultado > 0)
                {
                    alertas("Casa ingresada con éxito");
                    tDireccion.Text = string.Empty;
                    tCiudad.Text = string.Empty;
                    tPrecio.Text = string.Empty;
                    LlenarGridCasas();
                }
                else
                {
                    alertas("Error al ingresar casa");
                }
            }
            catch (FormatException)
            {
                alertas("El valor ingresado para el precio no es válido");
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = Casa_OP.BorrarCasa(int.Parse(tCasaID.Text));

                if (resultado > 0)
                {
                    alertas("La Casa ha sido eliminada con éxito");
                    tCasaID.Text = string.Empty;
                    LlenarGridCasas();
                }
                else
                {
                    alertas("Error al eliminar casa, Primero debes desligarla de una venta");
                    tCasaID.Text = string.Empty;
                }
            }
            catch (FormatException)
            {
                alertas("Por favor, ingrese un ID de casa válido");
                tCasaID.Text = string.Empty;
            }
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = Casa_OP.ModificarCasa(int.Parse(tCasaID.Text), tDireccion.Text, tCiudad.Text, (decimal.Parse(tPrecio.Text)));

                if (resultado > 0)
                {
                    alertas("La Casa ha sido modificado con éxito");
                    tCasaID.Text = string.Empty;
                    tDireccion.Text = string.Empty;
                    tCiudad.Text = string.Empty;
                    tPrecio.Text = string.Empty;
                    LlenarGridCasas();
                }
                else
                {
                    alertas("Error al modificar casa");
                }
            }
            catch (FormatException)
            {
                alertas("Por favor, ingrese un ID de casa válido");
            }
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCasa = int.Parse(txConsultarID.Text);
                DataTable dt = Casa_OP.ConsultarCasaPorID(idCasa);
                gridCasas.DataSource = dt;
                gridCasas.DataBind();
                txConsultarID.Text = string.Empty;
            }
            catch (FormatException)
            {
                alertas("Por favor, ingrese un ID de casa válido");
            }
        }
        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}