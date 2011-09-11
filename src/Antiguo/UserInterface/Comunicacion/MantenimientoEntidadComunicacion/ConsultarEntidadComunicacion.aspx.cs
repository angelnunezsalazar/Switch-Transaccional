using System;
using System.Web.UI.WebControls;

namespace UserInterface.Comunicacion.MantenimientoEntidadComunicacion
{
    public partial class ConsultarEntidadComunicacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void dsEntidadComunicacion_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BusinessEntity.EstadoOperacion Estado = ((BusinessEntity.EstadoOperacion)e.ReturnValue);
            if (!Estado.Estado)
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }
    }
}
