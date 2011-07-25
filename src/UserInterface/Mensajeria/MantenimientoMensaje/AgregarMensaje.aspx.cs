using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoMensaje
{
    public partial class AgregarMensaje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)this.frmMensaje.FindControl("lblNombreGrupoMensaje")).Text = Request.QueryString["Nombre"];
            ((HtmlInputHidden)this.frmMensaje.FindControl("lblCodigoGrupoMensaje")).Value = Request.QueryString["Codigo"];
        }

        protected void dsMensaje_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BusinessEntity.EstadoOperacion Estado = ((BusinessEntity.EstadoOperacion)e.ReturnValue);

            if (Estado.Estado)
            {
                Response.Redirect("~/Mensajeria/MantenimientoMensaje/ConsultarMensaje.aspx?Codigo=" + Request.QueryString["Codigo"]);
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void dsMensaje_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            MENSAJE mensaje = (MENSAJE)e.InputParameters[0];

            DropDownList drlCondicionMensaje = (DropDownList)this.frmMensaje.FindControl("drlCondicionMensaje");

            GRUPO_MENSAJE grupoMensaje = new GRUPO_MENSAJE() { GMJ_CODIGO = int.Parse(((HtmlInputHidden)this.frmMensaje.FindControl("lblCodigoGrupoMensaje")).Value) };
            mensaje.GRUPO_MENSAJE = grupoMensaje;

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

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mensajeria/MantenimientoMensaje/ConsultarMensaje.aspx?Codigo=" + Request.QueryString["Codigo"]);
        }
    }
}
