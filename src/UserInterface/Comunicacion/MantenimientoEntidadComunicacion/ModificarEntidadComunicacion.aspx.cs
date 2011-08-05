using System;
using System.IO;
using BusinessEntity;
using BusinessLayer.Comunicacion;

namespace UserInterface.Comunicacion.MantenimientoEntidadComunicacion
{
    public partial class ModificarEntidadComunicacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
            if (!IsPostBack)
            {
                EntidadComunicacion entidadComunicacion = EntidadComunicacionBL.obtenerEntidadComunicacion(int.Parse(Request.QueryString["Codigo"]));
                if (entidadComunicacion != null)
                {

                    this.txtNombre.Text = entidadComunicacion.EDC_NOMBRE;
                    this.txtDescripcion.Text = entidadComunicacion.EDC_DESCRIPCION;
                    this.txtCola.Text = entidadComunicacion.EDC_COLA;
                    this.txtNombreLog.Text = entidadComunicacion.EDC_NOMBRE_LOG;
                    this.txtDirectorioLog.Text = entidadComunicacion.EDC_RUTA_LOG;
                    this.txtTimeOutCola.Text = entidadComunicacion.EDC_TIMEOUT_EN_COLA.ToString();

                    this.drlProtocolo.DataBind();
                    this.drlProtocolo.Items.FindByValue(entidadComunicacion.PROTOCOLO.PTR_CODIGO.ToString()).Selected = true;
                    this.drlTipoEntidad.DataBind();
                    this.drlTipoEntidad.Items.FindByValue(entidadComunicacion.TIPO_ENTIDAD.TEM_CODIGO.ToString()).Selected = true;
                }
                else
                {
                    this.lblMensaje.Text = "No se pueden recuperar los datos";
                }
            }
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

            EntidadComunicacion entidadComunicacion = new EntidadComunicacion()
            {
                EDC_CODIGO = int.Parse(Request.QueryString["Codigo"])
            };
            entidadComunicacion.EDC_NOMBRE = this.txtNombre.Text;
            entidadComunicacion.EDC_COLA = this.txtCola.Text;
            entidadComunicacion.EDC_DESCRIPCION = this.txtDescripcion.Text;
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
            BusinessEntity.EstadoOperacion Resultado = EntidadComunicacionBL.modificarEntidadComunicacion(entidadComunicacion);

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
