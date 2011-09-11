using System;
using System.Web.UI;
using BusinessEntity;
using BusinessLayer.Operacion;

namespace UserInterface.Operacion.MantenimientoTransformacionCampo
{
    public partial class ModificarTransformacionCampo : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ObtenerTransformacionCampo();
            }
        }

        private void ObtenerTransformacionCampo()
        {
            int Transformacion = int.Parse(Request.QueryString["Transformacion"]);
            int Mensaje = int.Parse(Request.QueryString["Mensaje"]);
            int Campo = int.Parse(Request.QueryString["Campo"]);

            TRANSFORMACION_CAMPO transformacionCampo = TransformacionCampoBL.obtenerTransformacionCampo(Transformacion, Mensaje, Campo);

            this.drlTipoTransformacion.DataBind();
            this.drlTipoTransformacion.Items.FindByValue(transformacionCampo.TCM_TIPO.ToString()).Selected = true;
            ViewState["tipoTransformacion"] = transformacionCampo.TCM_TIPO.ToString();

            if (transformacionCampo.TCM_TIPO == TipoTransformacionBL.obtenerCodigoValorConstante())
            {
                this.txtValorConstante.Text = transformacionCampo.TCM_VALOR_CONSTANTE;
            }

            if (transformacionCampo.TCM_TIPO == TipoTransformacionBL.obtenerCodigoComponente())
            {
                this.drlComponente.DataBind();
                this.drlComponente.Items.FindByValue(transformacionCampo.TCM_COMPONENTE).Selected = true;
                this.txtClase.Text = transformacionCampo.TCM_CLASE;
                this.txtMetodo.Text = transformacionCampo.TCM_METODO;
            }

            if (transformacionCampo.TCM_TIPO == TipoTransformacionBL.obtenerCodigoProcedimientoAlmacenado())
            {
                this.txtProcedimientoBD.Text = transformacionCampo.TCM_PROCEDIMIENTO;
            }

            if (transformacionCampo.TCM_TIPO == TipoTransformacionBL.obtenerCodigoFuncionalidadEstandar())
            {
                this.drlFuncionalidadEstandar.DataBind();
                this.drlFuncionalidadEstandar.Items.FindByValue(transformacionCampo.TCM_FUNCIONALIDAD_ESTANDAR.ToString()).Selected = true;
            }
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
                transformacionCampo.TCM_COMPONENTE = this.drlComponente.SelectedValue;
                transformacionCampo.TCM_CLASE = this.txtClase.Text;
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

            EstadoOperacion resultado = TransformacionCampoBL.modificarTransformacionCampo(transformacionCampo);

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

        protected void btnParametros_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Operacion/MantenimientoParametroTransformacionCampo/ConsultarParametroTransformacionCampo.aspx?Transformacion=" +
                Request.QueryString["Transformacion"] + "&Mensaje=" + Request.QueryString["Mensaje"] + "&Campo=" + Request.QueryString["Campo"]);
        }
    }
}
