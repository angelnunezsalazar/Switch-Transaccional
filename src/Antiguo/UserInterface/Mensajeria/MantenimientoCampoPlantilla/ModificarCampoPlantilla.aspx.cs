using System;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoCampoPlantilla
{
    public partial class ModificarCampoPlantilla : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void dsCampoPlantilla_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BusinessEntity.EstadoOperacion Estado = ((BusinessEntity.EstadoOperacion)e.ReturnValue);

            if (Estado.Estado)
            {
                Response.Redirect("~/Mensajeria/MantenimientoCampoPlantilla/ConsultarCampoPlantilla.aspx");
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void dsCampoPlantilla_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            CAMPO_PLANTILLA campoPlantilla = (CAMPO_PLANTILLA)e.InputParameters[0];

            campoPlantilla.TIPO_DATO = new TIPO_DATO()
            {
                TDT_CODIGO = int.Parse(((DropDownList)this.frmCampoPlantilla.FindControl("drlTipoDato")).SelectedValue)
            };

            campoPlantilla.GRUPO_MENSAJE = new GRUPO_MENSAJE()
            {
                GMJ_CODIGO = int.Parse(((HiddenField)this.frmGrupoMensaje.FindControl("hdnCodigoGrupoMensaje")).Value)
            };
        }

        protected void frmCampoPlantilla_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            if (e.NewValues["CMP_LONGITUD_CABECERA"].ToString() == string.Empty)
            {
                e.NewValues["CMP_LONGITUD_CABECERA"] = null;
            }
            if (e.NewValues["CMP_POSICION_RELATIVA"].ToString() == string.Empty)
            {
                e.NewValues["CMP_POSICION_RELATIVA"] = null;
            }
        }
    }
}
