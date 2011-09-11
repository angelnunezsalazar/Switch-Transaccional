using System;
using System.Web.UI;
using BusinessEntity;
using BusinessLayer.Seguridad;

namespace UserInterface.Seguridad.MantenimientoCriptografiaCampo
{
    public partial class ModificarCampoCriptografia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int dinamica = int.Parse(Request.QueryString["Dinamica"]);
                int campo = int.Parse(Request.QueryString["Campo"]);
                CRIPTOGRAFIA_CAMPO critografiaCampo = CriptografiaCampoBL.obtenerCriptografiaCampo(dinamica, campo);

                drlCampoOrigen.DataBind();
                this.drlCampoOrigen.Items.FindByValue
                    (critografiaCampo.CAMPO_RESULTADO.CAM_CODIGO.ToString()).Selected = true;

                drlAlgoritmo.DataBind();
                this.drlAlgoritmo.Items.FindByValue
                    (critografiaCampo.CRC_ALGORITMO.ToString()).Selected = true;

                drlTipoLlave1.DataBind();
                this.drlTipoLlave1.Items.FindByValue
                    (critografiaCampo.CRC_TIPO_LLAVE_1.ToString()).Selected = true;

                if (critografiaCampo.CRC_TIPO_LLAVE_1 == TipoLlaveBL.obtenerCodigoCampo())
                {
                    drlCampoLlave1.DataBind();
                    this.drlCampoLlave1.Items.FindByValue
                    (critografiaCampo.CAMPO_LLAVE_1.CAM_CODIGO.ToString()).Selected = true;
                }

                if (critografiaCampo.CRC_TIPO_LLAVE_1 == TipoLlaveBL.obtenerCodigoLlaveFija())
                {
                    this.txtLlaveFija1.Text = critografiaCampo.CRC_LLAVE_1;
                }

                this.chkSegundaLlave.Checked = critografiaCampo.CRC_SEGUNDA_LLAVE;

                if (critografiaCampo.CRC_SEGUNDA_LLAVE)
                {
                    this.drlTipoLlave2.DataBind();
                    this.drlTipoLlave2.Items.FindByValue
                        (critografiaCampo.CRC_TIPO_LLAVE_2.ToString()).Selected = true;


                    if (critografiaCampo.CRC_TIPO_LLAVE_2 == TipoLlaveBL.obtenerCodigoCampo())
                    {
                        this.drlCampoLlave2.DataBind();
                        this.drlCampoLlave2.Items.FindByValue
                        (critografiaCampo.CAMPO_LLAVE_2.CAM_CODIGO.ToString()).Selected = true;
                    }

                    if (critografiaCampo.CRC_TIPO_LLAVE_2 == TipoLlaveBL.obtenerCodigoCampo())
                    {
                        this.txtLlaveFija2.Text = critografiaCampo.CRC_LLAVE_2;
                    }

                    this.drlOperacionLlave.DataBind();
                    this.drlOperacionLlave.Items.FindByValue
                        (critografiaCampo.CRC_OPERACION_LLAVE.ToString()).Selected = true;
                }
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

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            int codigoMensaje = int.Parse(Request.QueryString["Mensaje"]);

            CRIPTOGRAFIA_CAMPO criptografiaCampo = new CRIPTOGRAFIA_CAMPO();
            criptografiaCampo.CRC_CODIGO = int.Parse(Request.QueryString["Campo"]);

            DINAMICA_CRIPTOGRAFIA dinamicaCriptografia = new DINAMICA_CRIPTOGRAFIA()
            {
                DNC_CODIGO = int.Parse(Request.QueryString["Dinamica"])
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

            EstadoOperacion estadoOperacion = CriptografiaCampoBL.modificarCriptografiaCampo(criptografiaCampo);
            if (estadoOperacion.Estado)
            {
                Regresar();
            }
            else
            {
                this.lblMensaje.Text = estadoOperacion.Mensaje;
            }
        }
    }
}
