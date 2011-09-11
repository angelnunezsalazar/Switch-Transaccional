using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoMensaje
{
    public partial class ModificarMensaje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mensajeria/MantenimientoMensaje/ConsultarMensaje.aspx?Codigo=" + ((HtmlInputHidden)this.frmMensaje.FindControl("lblCodigoGrupoMensaje")).Value);
        }

        protected void dsMensaje_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BusinessEntity.EstadoOperacion Estado = ((BusinessEntity.EstadoOperacion)e.ReturnValue);

            if (Estado.Estado)
            {
                Response.Redirect("~/Mensajeria/MantenimientoMensaje/ConsultarMensaje.aspx?Codigo=" + ((HtmlInputHidden)this.frmMensaje.FindControl("lblCodigoGrupoMensaje")).Value);
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void dsMensaje_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            MENSAJE mensaje = (MENSAJE)e.InputParameters[0];

            DropDownList drlCondicionMensaje = (DropDownList)this.frmMensaje.FindControl("drlCondicionMensaje");
            ViewState.Add("Mensaje", mensaje);
        }

        protected void frmMensaje_DataBound(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                MENSAJE mensaje = (MENSAJE)ViewState["Mensaje"];

                ((TextBox)this.frmMensaje.FindControl("txtNombre")).Text = mensaje.MEN_NOMBRE;
                ((TextBox)this.frmMensaje.FindControl("txtDescripcion")).Text = mensaje.MEN_DESCRIPCION;

            }
        }
    }
}
