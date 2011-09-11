using System;
using BusinessEntity;
using BusinessLayer.Operacion;

namespace UserInterface.Operacion.MantenimientoTransformacionCampo
{
    public partial class AgregarTransformacionCampo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            TRANSFORMACION_CAMPO transformacionCampo = new TRANSFORMACION_CAMPO();

            transformacionCampo.TRM_CODIGO = int.Parse(Request.QueryString["Transformacion"]);
            transformacionCampo.MEN_CODIGO_MENSAJE_DESTINO = int.Parse(Request.QueryString["Mensaje"]);
            transformacionCampo.CAM_CODIGO_CAMPO_DESTINO = int.Parse(Request.QueryString["Campo"]);

            transformacionCampo.TCM_TIPO = int.Parse(this.drlTipoTransformacion.SelectedValue);
           
            if (this.drlTipoTransformacion.SelectedValue == TipoTransformacionBL.obtenerCodigoValorConstante().ToString())
            {
                transformacionCampo.TCM_VALOR_CONSTANTE = this.txtValorConstante.Text;
            }

            if (this.drlTipoTransformacion.SelectedValue == TipoTransformacionBL.obtenerCodigoComponente().ToString())
            {
                transformacionCampo.TCM_COMPONENTE=this.drlComponente.SelectedValue;
                transformacionCampo.TCM_CLASE=this.txtClase.Text;
                transformacionCampo.TCM_METODO = this.txtMetodo.Text;
            }


            if (this.drlTipoTransformacion.SelectedValue == TipoTransformacionBL.obtenerCodigoProcedimientoAlmacenado().ToString())
            {
                transformacionCampo.TCM_PROCEDIMIENTO = this.txtProcedimientoBD.Text;
            }


            if (this.drlTipoTransformacion.SelectedValue == TipoTransformacionBL.obtenerCodigoFuncionalidadEstandar().ToString())
            {
                transformacionCampo.TCM_FUNCIONALIDAD_ESTANDAR = int.Parse(this.drlFuncionalidadEstandar.SelectedValue);
                
            }

            EstadoOperacion resultado = TransformacionCampoBL.insertarTransformacionCampo(transformacionCampo);

            if (resultado.Estado)
            {
                string url = string.Format("~/Operacion/MantenimientoTransformacionCampo/ModificarTransformacionCampo.aspx?Transformacion={0}&Mensaje={1}&Campo={2}"
                    , Request.QueryString["Transformacion"], Request.QueryString["Mensaje"], Request.QueryString["Campo"]);

                Response.Redirect(url);
            }
            else
            {
                this.lblMensaje.Text = resultado.Mensaje;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            string url = string.Format("~/Operacion/MantenimientoTransformacionCampo/ConsultarTransformacionCampo.aspx?Transformacion={0}&Mensaje={1}", Request.QueryString["Transformacion"], Request.QueryString["Mensaje"]);
            Response.Redirect(url);
        }
    }
}
