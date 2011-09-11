using System;
using System.Web.UI.WebControls;

namespace UserInterface.Terminales.MantenimientoTerminal
{
    public partial class AgregarTerminal : System.Web.UI.Page
    {
        protected void oTerminal_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                Response.Redirect("~/Terminales/MantenimientoTerminal/ConsultarTerminal.aspx");
            }
        }
    }
}
