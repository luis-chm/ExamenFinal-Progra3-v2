using ExamenFinal.Capa_Presentacion.Agentes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExamenFinal.CapaDatos;
using ExamenFinal.CapaLogica;

namespace ExamenFinal.Capa_Presentacion.Clientes
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarGridClientes();
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
        protected void LlenarGridClientes()
        {
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                string query = "LlenarGridClientes";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvClientes.DataSource = dt;
                            gvClientes.DataBind();
                        }
                    }
                }
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(temail.Text) ||
                string.IsNullOrWhiteSpace(tTelefono.Text))
            {
                alertas("Por favor, complete todos los campos.");
                return;
            }
            ClsCliente cliente = new ClsCliente()
            {
                Nombre = txtNombre.Text,
                Email = temail.Text,
                Telefono = tTelefono.Text,
            };

            Cliente_OP cliente_OP = new Cliente_OP();

            int resultado = cliente_OP.AgregarCliente(cliente);

            if (resultado > 0)
            {
                alertas("Cliente ingresado con éxito");
                txtNombre.Text = string.Empty;
                temail.Text = string.Empty;
                tTelefono.Text = string.Empty;
                LlenarGridClientes();
            }
            else
            {
                alertas("Error al ingresar Cliente");
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = Cliente_OP.BorrarCliente(int.Parse(tClienteID.Text));

                if (resultado > 0)
                {
                    alertas("El Cliente ha sido eliminado con éxito");
                    tClienteID.Text = string.Empty;
                    LlenarGridClientes();
                }
                else
                {
                    alertas("Error al eliminar cliente, Primero debes desligarlo de una venta");
                    tClienteID.Text = string.Empty;
                }
            }
            catch (FormatException)
            {
                alertas("Por favor, ingrese un ID de cliente válido");
                tClienteID.Text = string.Empty;
            }
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = Cliente_OP.ModificarCliente(int.Parse(tClienteID.Text), txtNombre.Text, temail.Text, tTelefono.Text);

                if (resultado > 0)
                {
                    alertas("El cliente ha sido modificado con éxito");
                    tClienteID.Text = string.Empty;
                    txtNombre.Text = string.Empty;
                    temail.Text = string.Empty;
                    tTelefono.Text = string.Empty;
                    LlenarGridClientes();
                }
                else
                {
                    alertas("Error al modificar cliente");
                }
            }
            catch (FormatException)
            {
                alertas("Por favor, ingrese un ID de cliente válido");
            }
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCliente = int.Parse(txConsultarID.Text);
                DataTable dt = Cliente_OP.ConsultarClientePorID(idCliente);
                gvClientes.DataSource = dt;
                gvClientes.DataBind();
                txConsultarID.Text = string.Empty;
            }
            catch (FormatException)
            {
                alertas("Por favor, ingrese un ID de clientes válido");
            }
        }
        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

    }
}