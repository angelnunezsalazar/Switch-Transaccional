using System;
using System.IO;
using BusinessEntity;
using BusinessLayer.Comunicacion;

namespace UserInterface.Comunicacion.MantenimientoEntidadComunicacion
{
    public partial class AgregarEntidadComunicacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DirectoryInfo Directorio = new DirectoryInfo(this.txtDirectorioLog.Text);
            if (Directorio == null)
            {
                this.lblMensaje.Text = "El Directorio Log no existe";
                return;
            }

            if (!Directorio.Exists)
            {
                this.lblMensaje.Text = "El Directorio Log no existe";
                return;
            }

            ENTIDAD_COMUNICACION entidadComunicacion = new ENTIDAD_COMUNICACION();
            entidadComunicacion.EDC_NOMBRE = this.txtNombre.Text;
            entidadComunicacion.EDC_DESCRIPCION = this.txtDescripcion.Text;
            entidadComunicacion.EDC_COLA = this.txtCola.Text;
            entidadComunicacion.EDC_RUTA_LOG = this.txtDirectorioLog.Text;
            entidadComunicacion.EDC_NOMBRE_LOG = this.txtNombreLog.Text;
            entidadComunicacion.EDC_TIMEOUT_EN_COLA = int.Parse(this.txtTimeOutCola.Text);

            PROTOCOLO protocolo = new PROTOCOLO()
            {
                PTR_CODIGO = int.Parse(this.drlProtocolo.SelectedValue)
            };

            TIPO_ENTIDAD TipoEntidad = new TIPO_ENTIDAD()
            {
                TEM_CODIGO = int.Parse(this.drlTipoEntidad.SelectedValue)
            };

            entidadComunicacion.PROTOCOLO = protocolo;
            entidadComunicacion.TIPO_ENTIDAD = TipoEntidad;
            BusinessEntity.EstadoOperacion Resultado = EntidadComunicacionBL.insertarEntidadComunicacion(entidadComunicacion);

            if (Resultado.Estado)
            {
                Response.Redirect("~/Comunicacion/MantenimientoEntidadComunicacion/ConsultarEntidadComunicacion.aspx");
            }
            else
            {
                this.lblMensaje.Text = Resultado.Mensaje;
            }
        }
    }
}
