using System;
using System.Web.UI.WebControls;

namespace UserInterface.Terminales.MantenimientoTerminal
{
    public partial class ModificarTerminal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void oTerminal_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            BusinessEntity.Terminal terminal = (BusinessEntity.Terminal)e.InputParameters[0];
            terminal.Id = int.Parse(Request.QueryString["Codigo"]);
        }

        protected void oTerminal_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BusinessEntity.EstadoOperacion Estado = ((BusinessEntity.EstadoOperacion)e.ReturnValue);
            if (Estado.Estado)
            {
                Response.Redirect("~/Terminales/MantenimientoTerminal/ConsultarTerminal.aspx");
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }
    }
}
