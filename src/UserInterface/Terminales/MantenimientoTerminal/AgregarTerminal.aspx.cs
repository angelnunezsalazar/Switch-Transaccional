using System;
using System.Web.UI.WebControls;

namespace UserInterface.Terminales.MantenimientoTerminal
{
    public partial class AgregarTerminal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {

        }


        protected void oTerminal_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
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
