using System;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Terminales.MantenimientoPuntoServicio
{
    public partial class ConsultarPuntoServicio : System.Web.UI.Page
    {
        protected void grvPuntoServicio_DataBound(object sender, EventArgs e)
        {
            if (this.grvPuntoServicio.Rows.Count == 0)
            {
                this.grvPuntoServicio.BorderStyle = BorderStyle.None;
            }
            else
            {
                this.grvPuntoServicio.BorderStyle = BorderStyle.Solid;
            }

        }
    }
}
