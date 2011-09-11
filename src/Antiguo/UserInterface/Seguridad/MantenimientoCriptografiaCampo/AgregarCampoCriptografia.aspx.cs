using System;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLayer.Seguridad;

namespace UserInterface.Seguridad.MantenimientoCriptografiaCampo
{
    public partial class AgregarCampoCriptografia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            int codigoMensaje=int.Parse(((HiddenField)this.frmDinamicaCriptografia.FindControl("hdnCodigoMensaje")).Value);

            CRIPTOGRAFIA_CAMPO criptografiaCampo = new CRIPTOGRAFIA_CAMPO();

            DINAMICA_CRIPTOGRAFIA dinamicaCriptografia = new DINAMICA_CRIPTOGRAFIA()
            {
                DNC_CODIGO=int.Parse(Request.QueryString["Dinamica"])
            };
            criptografiaCampo.DINAMICA_CRIPTOGRAFIA = dinamicaCriptografia;

            CAMPO campoOrigen = new CAMPO()
            {
                CAM_CODIGO = int.Parse(this.drlCampoOrigen.SelectedValue),
                MEN_CODIGO = codigoMensaje
            };
            criptografiaCampo.CAMPO_RESULTADO = campoOrigen;

            criptografiaCampo.CRC_ALGORITMO = int.Parse(this.drlAlgoritmo.SelectedValue);
            criptografiaCampo.CRC_TIPO_LLAVE_1 = int.Parse(this.drlTipoLlave1.SelectedValue);

            if (criptografiaCampo.CRC_TIPO_LLAVE_1 == TipoLlaveBL.obtenerCodigoCampo())
            {
                CAMPO campoLlave1 = new CAMPO()
                {
                    CAM_CODIGO = int.Parse(this.drlCampoLlave1.SelectedValue),
                    MEN_CODIGO = codigoMensaje
                };
                criptografiaCampo.CAMPO_LLAVE_1 = campoLlave1;
            }

            if (criptografiaCampo.CRC_TIPO_LLAVE_1 == TipoLlaveBL.obtenerCodigoLlaveFija())
            {
                criptografiaCampo.CRC_LLAVE_1 = this.txtLlaveFija1.Text;
            }

            criptografiaCampo.CRC_SEGUNDA_LLAVE = this.chkSegundaLlave.Checked;
            if (criptografiaCampo.CRC_SEGUNDA_LLAVE)
            {
                criptografiaCampo.CRC_TIPO_LLAVE_2 = int.Parse(this.drlTipoLlave2.SelectedValue);

                if (criptografiaCampo.CRC_TIPO_LLAVE_2 == TipoLlaveBL.obtenerCodigoCampo())
                {
                    CAMPO campoLlave2 = new CAMPO()
                    {
                        CAM_CODIGO = int.Parse(this.drlCampoLlave2.SelectedValue),
                        MEN_CODIGO = codigoMensaje
                    };
                    criptografiaCampo.CAMPO_LLAVE_2 = campoLlave2;
                }

                if (criptografiaCampo.CRC_TIPO_LLAVE_2 == TipoLlaveBL.obtenerCodigoLlaveFija())
                {
                    criptografiaCampo.CRC_LLAVE_2 = this.txtLlaveFija2.Text;
                }

                criptografiaCampo.CRC_OPERACION_LLAVE = int.Parse(this.drlOperacionLlave.SelectedValue);
            }

            EstadoOperacion estadoOperacion = CriptografiaCampoBL.insertarCriptografiaCampo(criptografiaCampo);
            if (estadoOperacion.Estado)
            {
                Regresar();
            }
            else
            {
                this.lblMensaje.Text = estadoOperacion.Mensaje;
            }


        }

        private void Regresar()
        {
            Response.Redirect("~/Seguridad/MantenimientoCriptografiaCampo/ConsultarCampoCriptografia.aspx?Dinamica=" + Request.QueryString["Dinamica"]);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Regresar();
        }
    }
}
