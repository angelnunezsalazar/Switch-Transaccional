using System;
using System.Web.UI;
using BusinessEntity;
using BusinessLayer.Operacion;

namespace UserInterface.Operacion.MantenimientoDinamica
{
    public partial class AgregarDinamica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.lblNombreGrupo.Text = Session["nombreGrupo"].ToString();
                this.lblNombreMensaje.Text = Session["nombreMensaje"].ToString();
                this.lblMensajeTransaccional.Text = Session["nombreMensajeTransaccional"].ToString();
                this.lblNumero.Text = PasoDinamicaBL.obtenerSiguienteNumero(int.Parse(Session["codigoMensajeTransaccional"].ToString()));

                if (this.lblNumero.Text==string.Empty)
                {
                    this.btnAceptar.Enabled = false;
                }
            }

            this.lblMensaje.Text = "";
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Operacion/MantenimientoDinamica/ConsultarDinamica.aspx");
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            PASO_DINAMICA pasoDinamica = new PASO_DINAMICA()
            {
                PDT_FUNCIONALIDAD = int.Parse(this.drlTipoFuncionalidad.SelectedValue),
                PDT_NUMERO = this.lblNumero.Text,
                MENSAJE_TRANSACCIONAL = new MENSAJE_TRANSACCIONAL() 
                { 
                 MTR_CODIGO=int.Parse(Session["codigoMensajeTransaccional"].ToString())
                }
            };

            if (pasoDinamica.PDT_FUNCIONALIDAD==TipoFuncionalidadBL.obtenerCodigoCriptografia())
            {
                pasoDinamica.PDT_PASO=int.Parse(this.drlCriptografia.SelectedValue);
                pasoDinamica.PDT_INFORMACION_ADICIONAL = this.drlCriptografia.SelectedItem.Text;
                
            }
            
            if (pasoDinamica.PDT_FUNCIONALIDAD == TipoFuncionalidadBL.obtenerCodigoEnviar() || 
                pasoDinamica.PDT_FUNCIONALIDAD == TipoFuncionalidadBL.obtenerCodigoRecibir())
            {
                pasoDinamica.ENTIDAD_COMUNICACION = new ENTIDAD_COMUNICACION()
                {
                    EDC_CODIGO = int.Parse(this.drlEntidad.SelectedValue)
                };
                pasoDinamica.PDT_REINTENTOS = int.Parse(this.txtReintentos.Text);
                pasoDinamica.PDT_INFORMACION_ADICIONAL = this.drlEntidad.SelectedItem.Text;
            }

            if (pasoDinamica.PDT_FUNCIONALIDAD == TipoFuncionalidadBL.obtenerCodigoTransformacion())
            {
                pasoDinamica.PDT_PASO = int.Parse(this.drlTransformacion.SelectedValue);
                pasoDinamica.PDT_INFORMACION_ADICIONAL = this.drlTransformacion.SelectedItem.Text;
            }

            if (pasoDinamica.PDT_FUNCIONALIDAD == TipoFuncionalidadBL.obtenerCodigoValidacion())
            {
                pasoDinamica.PDT_PASO = int.Parse(this.drlValidacion.SelectedValue);
                pasoDinamica.PDT_INFORMACION_ADICIONAL = this.drlValidacion.SelectedItem.Text;
            }

            BusinessEntity.EstadoOperacion Estado = PasoDinamicaBL.insertarPasoDinamica(pasoDinamica, int.Parse(Session["codigoGrupo"].ToString()));

            if (Estado.Estado)
            {
                Response.Redirect("~/Operacion/MantenimientoDinamica/ConsultarDinamica.aspx");
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }
    }
}
