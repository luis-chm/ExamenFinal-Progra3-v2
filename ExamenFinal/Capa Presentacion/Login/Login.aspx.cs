using ExamenFinal.CapaDatos;
using ExamenFinal.CapaLogica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenFinal.Capa_Presentacion.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ClsAgente agente = new ClsAgente()
            {
                Email = temail.Text,
                Contrasena = tcontrasena.Text
            };
            int resultadoValidacion = Login_OP.ValidarLogin(agente);

            //Redirigir si el inicio de sesión es exitoso, mostrar una alerta de error si no lo es
            if (resultadoValidacion > 0)
            {
                Response.Redirect("~/Capa Presentacion/Inicio/Inicio.aspx");
            }
            else
            {
                alertas("Correo o contraseña incorrectos");
            }
        }//fin metodo validar login
    }
}