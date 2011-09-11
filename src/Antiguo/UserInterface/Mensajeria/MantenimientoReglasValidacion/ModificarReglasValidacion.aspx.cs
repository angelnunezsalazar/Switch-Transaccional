using System;
using System.Web.UI.WebControls;

namespace UserInterface.Mensajeria.MantenimientoReglasValidacion
{
    public partial class ModificarReglasValidacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            Label lblCodigoGV = (Label)fvGrupoValidacion.FindControl("Codigo");
            DropDownList ddlCampo = (DropDownList)fvGrupoValidacion.FindControl("ddlCampo");
            HiddenField hdnCodigoMensaje = (HiddenField)fvGrupoValidacion.FindControl("hdnCodigoMensaje");
            Response.Redirect("AgregarDetalleReglasValidacion.aspx?Grupo=" + lblCodigoGV.Text + "&Mensaje=" + hdnCodigoMensaje.Value + "&Campo=" + ddlCampo.SelectedValue.ToString());

        }
    }
}
