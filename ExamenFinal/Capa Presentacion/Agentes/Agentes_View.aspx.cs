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
using System.Security.Claims;

namespace ExamenFinal.Capa_Presentacion.Agentes
{
    public partial class Agentes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarGridAgentes();
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
        protected void LlenarGridAgentes()
        {
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                string query = "LlenarGridAgentes";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvAgentes.DataSource = dt;
                            gvAgentes.DataBind();
                        }
                    }
                }
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(temail.Text) ||
                string.IsNullOrWhiteSpace(tTelefono.Text) ||
                string.IsNullOrWhiteSpace(tPassword.Text))
            {
                alertas("Por favor, complete todos los campos.");
                return;
            }
            ClsAgente agente = new ClsAgente()
            {
                Nombre = txtNombre.Text,
                Email = temail.Text,
                Telefono = tTelefono.Text,
                Contrasena = tPassword.Text
            };

            Agente_OP agenteOP = new Agente_OP();

            int resultado = agenteOP.AgregarAgente(agente);

            if (resultado > 0)
            {
                alertas("Agente ingresado con éxito");
                txtNombre.Text = string.Empty;
                temail.Text = string.Empty;
                tTelefono.Text = string.Empty;
                tPassword.Text = string.Empty;
                LlenarGridAgentes();
            }
            else
            {
                alertas("Error al ingresar Agente");
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = Agente_OP.BorrarAgente(int.Parse(tAgentID.Text));

                if (resultado > 0)
                {
                    alertas("El Agente ha sido eliminado con éxito");
                    tAgentID.Text = string.Empty;
                    LlenarGridAgentes();
                }
                else
                {
                    alertas("Error al eliminar Agente, Primero debes desligarlo de una venta");
                    tAgentID.Text = string.Empty;
                }
            }
            catch (FormatException)
            {
                alertas("Por favor, ingrese un ID de agente válido");
                tAgentID.Text = string.Empty;
            }
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = Agente_OP.ModificarAgente(int.Parse(tAgentID.Text),txtNombre.Text, temail.Text, tTelefono.Text, tPassword.Text);

                if (resultado > 0)
                {
                    alertas("El Agente ha sido modificado con éxito");
                    tAgentID.Text = string.Empty;
                    txtNombre.Text = string.Empty;
                    temail.Text = string.Empty;
                    tTelefono.Text = string.Empty;
                    tPassword.Text = string.Empty;
                    LlenarGridAgentes();
                }
                else
                {
                    alertas("Error al modificar Agente");
                }
            }
            catch (FormatException)
            {
                alertas("Por favor, ingrese un ID de agente válido");
            }
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                int idAgente = int.Parse(txConsultarID.Text);
                DataTable dt = Agente_OP.ConsultarAgentePorID(idAgente);
                gvAgentes.DataSource = dt;
                gvAgentes.DataBind();
                txConsultarID.Text = string.Empty;
            }
            catch (FormatException)
            {
                alertas("Por favor, ingrese un ID de agente válido");
            }
        }
        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }




    }//fin class
}//fin namespace