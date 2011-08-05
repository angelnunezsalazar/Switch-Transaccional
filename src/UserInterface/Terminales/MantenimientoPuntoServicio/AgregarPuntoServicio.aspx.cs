using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserInterface.Terminales.MantenimientoPuntoServicio
{
    public partial class AgregarPuntoServicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
            if (!Page.IsPostBack)
            {
                ((CheckBox)this.FormView1.FindControl("chkHabilitado")).Checked = true;
            }

        }

        protected void oPuntoServicio_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BusinessEntity.EstadoOperacion Estado = ((BusinessEntity.EstadoOperacion)e.ReturnValue);
            if (Estado.Estado)
            {
                Response.Redirect("~/Terminales/MantenimientoPuntoServicio/ConsultarPuntoServicio.aspx");
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }

        }
    }
}
