using ExamenFinal.CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenFinal.Capa_Presentacion.Menu_Master
{
    public partial class Menu : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime fechaHoraActual = DateTime.Now;
            lblFechaHora.Text = fechaHoraActual.ToString();
        }
    }
}