using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Operacion.MantenimientoTransformacionMensaje
{
    public partial class ConsultarTransformacionMensaje : System.Web.UI.Page
    {
        bool refrescarGrilla;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer.ToString().Contains("AgregarTransformacion") ||
                Request.UrlReferrer.ToString().Contains("ModificarTransformacion"))
            {

                this.txtNombre.Text=Session["NombreTransformacion"] == null?"":Session["NombreTransformacion"].ToString();

                this.drlGrupoMensajeInicio.DataBind();
                this.drlGrupoMensajeInicio.Items.FindByValue(Session["GrupoInicio"].ToString()).Selected = true;

                this.drlMensajeInicio.DataBind();
                this.drlMensajeInicio.Items.FindByValue(Session["MensajeInicio"].ToString()).Selected = true;

                this.drlGrupoMensajeFin.DataBind();
                this.drlGrupoMensajeFin.Items.FindByValue(Session["GrupoDestino"].ToString()).Selected = true;

                this.drlMensajeFin.DataBind();
                this.drlMensajeFin.Items.FindByValue(Session["MensajeDestion"].ToString()).Selected = true;

            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            refrescarGrilla = true;
            this.grvTransformacion.DataBind();
        }

        protected void dsTransformacion_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (refrescarGrilla == false)
                {
                    e.Cancel = true;
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session["NombreTransformacion"] = this.txtNombre.Text;
            Session["GrupoInicio"] = this.drlGrupoMensajeInicio.SelectedValue;
            Session["MensajeInicio"] = this.drlMensajeInicio.SelectedValue;
            Session["GrupoDestino"] = this.drlGrupoMensajeFin.SelectedValue;
            Session["MensajeDestion"] = this.drlMensajeFin.SelectedValue;

            Response.Redirect("~/Operacion/MantenimientoTransformacionMensaje/AgregarTransformacionMensaje.aspx");
        }

        protected void grvTransformacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                string[] argument = Convert.ToString(e.CommandArgument).Split('|');

                Session["NombreTransformacion"] = this.txtNombre.Text;
                Session["GrupoInicio"] = this.drlGrupoMensajeInicio.SelectedValue;
                Session["MensajeInicio"] = this.drlMensajeInicio.SelectedValue;
                Session["GrupoDestino"] = this.drlGrupoMensajeFin.SelectedValue;
                Session["MensajeDestion"] = this.drlMensajeFin.SelectedValue;

                Response.Redirect("~/Operacion/MantenimientoTransformacionCampo/ConsultarTransformacionCampo.aspx?Transformacion=" + argument[0] + "&Mensaje=" + argument[1]);
            }
        }

        protected void dsTransformacion_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion resultado = (EstadoOperacion)e.ReturnValue;
            if (!resultado.Estado)
            {
                lblMensaje.Text = resultado.Mensaje;
            }
        }
    }
}
