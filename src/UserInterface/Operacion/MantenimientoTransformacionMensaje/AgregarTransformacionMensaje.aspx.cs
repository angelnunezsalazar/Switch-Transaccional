using System;
using BusinessEntity;

namespace UserInterface.Operacion.MantenimientoTransformacionMensaje
{
    public partial class AgregarTransformacionMensaje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            TRANSFORMACION transformacion = new TRANSFORMACION();
            transformacion.TRM_NOMBRE = this.txtNombre.Text;

            transformacion.MENSAJE_ORIGEN = new MENSAJE() 
            { 
                MEN_CODIGO=int.Parse(this.drlMensajeOrigen.SelectedValue)
            };

            transformacion.MENSAJE_DESTINO = new MENSAJE() 
            {
                MEN_CODIGO = int.Parse(this.drlMensajeDestino.SelectedValue)
            };

            EstadoOperacion estado=BusinessLayer.Operacion.TransformacionMensajeBL.insertarTransformacion(transformacion);
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
