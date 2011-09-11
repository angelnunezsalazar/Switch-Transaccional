using System;
using BusinessEntity;

namespace UserInterface.Operacion.MantenimientoTransformacionMensaje
{
    public partial class ModificarTransformacionMensaje : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TRANSFORMACION transformacionMensaje = BusinessLayer.Operacion.TransformacionMensajeBL.obtenerTransformacion(int.Parse(Request.QueryString["Transformacion"]));
                this.txtNombre.Text = transformacionMensaje.TRM_NOMBRE;

                this.drlGrupoMensajeInicio.DataBind();
                this.drlGrupoMensajeInicio.Items.FindByValue(transformacionMensaje.MENSAJE_ORIGEN.GRUPO_MENSAJE.GMJ_CODIGO.ToString()).Selected = true;

                this.drlMensajeOrigen.DataBind();
                this.drlMensajeOrigen.Items.FindByValue(transformacionMensaje.MENSAJE_ORIGEN.MEN_CODIGO.ToString()).Selected = true;

                this.drlGrupoMensajeFin.DataBind();
                this.drlGrupoMensajeFin.Items.FindByValue(transformacionMensaje.MENSAJE_DESTINO.GRUPO_MENSAJE.GMJ_CODIGO.ToString()).Selected = true; ;

                this.drlMensajeDestino.DataBind();
                this.drlMensajeDestino.Items.FindByValue(transformacionMensaje.MENSAJE_DESTINO.MEN_CODIGO.ToString()).Selected = true;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            TRANSFORMACION transformacion = new TRANSFORMACION();
            transformacion.TRM_NOMBRE = this.txtNombre.Text;
            transformacion.TRM_CODIGO = int.Parse(Request.QueryString["Transformacion"]);

            transformacion.MENSAJE_ORIGEN = new MENSAJE()
            {
                MEN_CODIGO = int.Parse(this.drlMensajeOrigen.SelectedValue)
            };

            transformacion.MENSAJE_DESTINO = new MENSAJE()
            {
                MEN_CODIGO = int.Parse(this.drlMensajeDestino.SelectedValue)
            };

            EstadoOperacion estado = BusinessLayer.Operacion.TransformacionMensajeBL.modificarTransformacion(transformacion);
            if (estado.Estado)
            {
                Response.Redirect("~/Operacion/MantenimientoTransformacionMensaje/ConsultarTransformacionMensaje.aspx");
            }
            else
            {
                this.lblMensaje.Text = estado.Mensaje;
            }
        }
    }
}
