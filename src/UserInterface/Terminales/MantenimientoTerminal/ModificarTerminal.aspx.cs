using System;
using System.Web.UI.WebControls;

namespace UserInterface.Terminales.MantenimientoTerminal
{
    public partial class ModificarTerminal : System.Web.UI.Page
    {
        protected void oTerminal_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                Response.Redirect("~/Terminales/MantenimientoTerminal/ConsultarTerminal.aspx");
            }
        }
    }
}
