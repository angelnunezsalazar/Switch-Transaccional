using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserInterface.Mensajeria.MantenimientoGrupoValidacion
{
    public partial class ConsultarGrupoValidacion : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (drlMensaje.SelectedValue != "-1")
            {
                btnNuevo.Visible = true;
            }
            else
            {
                btnNuevo.Visible = false;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarGrupoValidacion.aspx?grupo=" + drlGrupoMensaje.SelectedValue + "&mensaje=" + drlMensaje.SelectedValue);
        }

        protected void dsGrupoValidacion_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BusinessEntity.EstadoOperacion Estado = ((BusinessEntity.EstadoOperacion)e.ReturnValue);

            if (!Estado.Estado)
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }
    }
}
