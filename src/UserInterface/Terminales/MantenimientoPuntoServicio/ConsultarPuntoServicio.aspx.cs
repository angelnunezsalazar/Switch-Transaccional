using System;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Terminales.MantenimientoPuntoServicio
{
    public partial class ConsultarPuntoServicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblError.Text = "";
        }

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

        protected void dsPuntoServicio_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion Estado = (EstadoOperacion)e.ReturnValue;

            if (!Estado.Estado)
            {
                this.lblError.Text = Estado.Mensaje;
            }
        }
    }
}
