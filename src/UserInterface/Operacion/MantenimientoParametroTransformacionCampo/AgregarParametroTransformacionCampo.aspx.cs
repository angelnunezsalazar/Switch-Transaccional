using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLayer.Operacion;

namespace UserInterface.Operacion.MantenimientoParametroTransformacionCampo
{
    public partial class AgregarParametroTransformacionCampo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            regresarListar();
        }

        private void regresarListar()
        {
            Response.Redirect("~/Operacion/MantenimientoParametroTransformacionCampo/ConsultarParametroTransformacionCampo.aspx?Transformacion=" +
           Request.QueryString["Transformacion"] + "&Mensaje=" + Request.QueryString["Mensaje"] + "&Campo=" + Request.QueryString["Campo"]);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            PARAMETRO_TRANSFORMACION_CAMPO parametroTransformacionCampo = new PARAMETRO_TRANSFORMACION_CAMPO();

            parametroTransformacionCampo.TRANSFORMACION_CAMPO = new TRANSFORMACION_CAMPO()
            {
                TRM_CODIGO = int.Parse(Request.QueryString["Transformacion"]),
                CAM_CODIGO_CAMPO_DESTINO = int.Parse(Request.QueryString["Campo"]),
                MEN_CODIGO_MENSAJE_DESTINO = int.Parse(Request.QueryString["Mensaje"])
            };
            parametroTransformacionCampo.PTC_TIPO = int.Parse(this.drlTipoParametro.SelectedValue);

            if (parametroTransformacionCampo.PTC_TIPO == TipoParametroTransformacionCampoBL.obtenerCodigoTabla())
            {
                parametroTransformacionCampo.CAMPO = new CAMPO()
                {
                    CAM_CODIGO = int.Parse(this.drlCampoOrigen.SelectedValue),
                    MEN_CODIGO = (from c in (List<CAMPO>)ViewState["CampoOrigen"]
                                  where c.CAM_CODIGO == int.Parse(this.drlCampoOrigen.SelectedValue)
                                  select c.MEN_CODIGO).First()
                };

                parametroTransformacionCampo.PTC_POSICION_INICIAL =
                    int.Parse(this.txtPosicionInicial.Text);

                parametroTransformacionCampo.PTC_LONGITUD =
                    int.Parse(this.txtLongitud.Text);

                parametroTransformacionCampo.TABLA = new TABLA()
                {
                    TBL_CODIGO = int.Parse(this.drlTabla.SelectedValue)
                };

                parametroTransformacionCampo.COLUMNA_ORIGEN = new COLUMNA()
                {
                    COL_CODIGO = int.Parse(this.drlColumnaOrigen.SelectedValue)
                };

                parametroTransformacionCampo.COLUMNA_DESTINO = new COLUMNA()
                {
                    COL_CODIGO = int.Parse(this.drlColumnaDestino.SelectedValue)
                };
            }

            if (parametroTransformacionCampo.PTC_TIPO == TipoParametroTransformacionCampoBL.obtenerCodigoCampoOrigen())
            {
                parametroTransformacionCampo.CAMPO = new CAMPO()
                {
                    CAM_CODIGO = int.Parse(this.drlCampoOrigen.SelectedValue),
                    MEN_CODIGO = (from c in (List<CAMPO>)ViewState["CampoOrigen"]
                                  where c.CAM_CODIGO == int.Parse(this.drlCampoOrigen.SelectedValue)
                                  select c.MEN_CODIGO).First()
                };

                parametroTransformacionCampo.PTC_POSICION_INICIAL =
                    int.Parse(this.txtPosicionInicial.Text);

                parametroTransformacionCampo.PTC_LONGITUD =
                    int.Parse(this.txtLongitud.Text);
            }

            EstadoOperacion resultado = ParametroTransformacionCampoBL.insertarParametroTransformacionCampo(parametroTransformacionCampo);
            if (resultado.Estado)
            {
                regresarListar();
            }
            else
            {
                this.lblMensaje.Text = resultado.Mensaje;
            }
        }

        protected void dsCampoOrigen_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            List<CAMPO> listaCampo = (List<CAMPO>)e.ReturnValue;

            if (listaCampo.Count != 0)
            {
                ViewState["CampoOrigen"] = listaCampo;
            }
        }

        protected string ObtenerNombreTipoTransformacion(string codigoTipoTransformacion)
        {
            return TipoTransformacionBL.obtenerNombrePorCodigo(int.Parse(codigoTipoTransformacion));
        }

    }
}
