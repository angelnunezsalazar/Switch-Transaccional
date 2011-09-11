using System;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoValorSelector
{
    public partial class ConsultarValorSelector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void dsCampo_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion estado = (EstadoOperacion)e.ReturnValue;

            if (!estado.Estado)
            {
                lblMensaje.Text = estado.Mensaje;
            }
        }
    }
}
