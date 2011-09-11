using System;
using System.Web.UI.WebControls;

namespace UserInterface.Terminales.MantenimientoPuntoServicio
{
    public partial class ModificarPuntoServicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void dsPuntoServicio_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                Response.Redirect("~/Terminales/MantenimientoPuntoServicio/ConsultarPuntoServicio.aspx");
            }
        }
    }
}
