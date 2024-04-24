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

namespace ExamenFinal.Capa_Presentacion.Ventas
{
    public partial class Ventas_View1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGridVentas();
                LlenarAgentes();
                LlenarClientes();
                LlenarCasas();
            }
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
        protected void LlenarGridVentas()
        {
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                string query = "LlenarGridVentas";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvVentas.DataSource = dt;
                            gvVentas.DataBind();
                        }
                    }
                }
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dropIDAgente.SelectedValue) ||
               string.IsNullOrWhiteSpace(dropIDCliente.SelectedValue) ||
               string.IsNullOrWhiteSpace(dropIDCasa.SelectedValue) ||
               string.IsNullOrWhiteSpace(txtFecha.Text))
            {
                alertas("Por favor, complete todos los campos.");
                return;
            }
            try
            {
                ClsVentas ventas = new ClsVentas()
                {
                    IDAgente = Convert.ToInt32(dropIDAgente.SelectedValue),
                    IDCliente = Convert.ToInt32(dropIDCliente.SelectedValue),
                    IDCasa = Convert.ToInt32(dropIDCasa.SelectedValue),
                    Fecha = Convert.ToDateTime(txtFecha.Text)
                };

                Venta_OP venta_OP = new Venta_OP();
                int resultado = venta_OP.AgregarVenta(ventas);

                if (resultado > 0)
                {
                    alertas("Venta ingresada con éxito");
                    tVentaID.Text = string.Empty;
                    dropIDAgente.SelectedValue = string.Empty;
                    dropIDCliente.SelectedValue = string.Empty;
                    dropIDCasa.SelectedValue = string.Empty;
                    txtFecha.Text = string.Empty;
                    LlenarGridVentas();
                }
                else
                {
                    alertas("Error al ingresar venta");
                }
            }
            catch (FormatException)
            {
                alertas("El valor ingresado para la fecha no es válido");
            }
        }
       protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = Venta_OP.BorrarVenta(int.Parse(tVentaID.Text));

                if (resultado > 0)
                {
                    alertas("La venta ha sido eliminada con éxito");
                    tVentaID.Text = string.Empty;
                    LlenarGridVentas();
                }
                else
                {
                    alertas("Error al eliminar venta");
                    tVentaID.Text = string.Empty;
                }
            }
            catch (FormatException)
            {
                alertas("Por favor, ingrese un ID de venta válido");
                tVentaID.Text = string.Empty;
            }
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = Venta_OP.ModificarVenta(
                    int.Parse(tVentaID.Text),
                    int.Parse(dropIDAgente.SelectedValue),
                    int.Parse(dropIDCliente.SelectedValue),
                    int.Parse(dropIDCasa.SelectedValue),
                    DateTime.Parse(txtFecha.Text));

                if (resultado > 0)
                {
                    alertas("La venta ha sido modificado con éxito");
                    tVentaID.Text = string.Empty;
                    dropIDAgente.SelectedValue = string.Empty;
                    dropIDCliente.SelectedValue = string.Empty;
                    dropIDCasa.SelectedValue = string.Empty;
                    txtFecha.Text = string.Empty;
                    LlenarGridVentas();
                }
                else
                {
                    alertas("Error al modificar venta");
                }
            }
            catch (FormatException)
            {
                alertas("Por favor, ingrese un ID de venta válido");
            }
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                int idVenta = int.Parse(txConsultarID.Text);
                DataTable dt = Venta_OP.ConsultarVentaPorID(idVenta);
                gvVentas.DataSource = dt;
                gvVentas.DataBind();
                txConsultarID.Text = string.Empty;
            }
            catch (FormatException)
            {
                alertas("Por favor, ingrese un ID de venta válido");
            }
        }
        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        #region GRID PARA DATABLES
        protected void LlenarAgentes()
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT IDAgente, Nombre FROM Agentes", con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            DataTable dtModified = new DataTable();
                            dtModified.Columns.Add("IDAgente");
                            dtModified.Columns.Add("TipoYDescripcion", typeof(string));
                            dtModified.Rows.Add("", "Seleccione un agente");

                            foreach (DataRow row in dt.Rows)
                            {
                                string IDAgente = row["IDAgente"].ToString();
                                string Nombre = row["Nombre"].ToString();
                                string IDyNombre = $"ID: {IDAgente} - Nombre: {Nombre}";
                                dtModified.Rows.Add(IDAgente, IDyNombre);
                            }

                            dropIDAgente.DataSource = dtModified;
                            dropIDAgente.DataTextField = "TipoYDescripcion";
                            dropIDAgente.DataValueField = "IDAgente";
                            dropIDAgente.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        protected void LlenarClientes()
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT IDCliente, Nombre FROM Clientes", con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            DataTable dtModified = new DataTable();
                            dtModified.Columns.Add("IDCliente");
                            dtModified.Columns.Add("TipoYDescripcion", typeof(string));
                            dtModified.Rows.Add("", "Seleccione un cliente");

                            foreach (DataRow row in dt.Rows)
                            {
                                string IDCliente = row["IDCliente"].ToString();
                                string Nombre = row["Nombre"].ToString();
                                string IDyNombre = $"ID: {IDCliente} - Nombre: {Nombre}";
                                dtModified.Rows.Add(IDCliente, IDyNombre);
                            }

                            dropIDCliente.DataSource = dtModified;
                            dropIDCliente.DataTextField = "TipoYDescripcion";
                            dropIDCliente.DataValueField = "IDCliente";
                            dropIDCliente.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        protected void LlenarCasas()
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT IDCasa, Direccion FROM Casas", con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            DataTable dtModified = new DataTable();
                            dtModified.Columns.Add("IDCasa");
                            dtModified.Columns.Add("TipoYDescripcion", typeof(string));
                            dtModified.Rows.Add("", "Seleccione una casa");

                            foreach (DataRow row in dt.Rows)
                            {
                                string IDCasa = row["IDCasa"].ToString();
                                string Direccion = row["Direccion"].ToString();
                                string IDyNombre = $"ID: {IDCasa} - Direccion: {Direccion}";
                                dtModified.Rows.Add(IDCasa, IDyNombre);
                            }

                            dropIDCasa.DataSource = dtModified;
                            dropIDCasa.DataTextField = "TipoYDescripcion";
                            dropIDCasa.DataValueField = "IDCasa";
                            dropIDCasa.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        #endregion GRID
    }
}