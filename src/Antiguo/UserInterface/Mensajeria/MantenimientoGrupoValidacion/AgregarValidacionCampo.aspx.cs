using System;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoGrupoValidacion
{
    public partial class AgregarValidacionCampo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GRUPO_VALIDACION gvalidacion = BusinessLayer.Mensajeria.GrupoValidacionBL.obtenerGrupoValidacionPorCodigo(Convert.ToInt32(Request.QueryString["codigo"]));
            CAMPO campo = BusinessLayer.Mensajeria.CampoMensajeBL.obtenerCampo(Convert.ToInt32(Request.QueryString["mensaje"]), Convert.ToInt32(Request.QueryString["campo"]));
            lblNombreGrupo.Text = gvalidacion.GRV_NOMBRE;
            lblNombreCampo.Text = campo.CAM_NOMBRE;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            VALIDACION_CAMPO vcampo = new VALIDACION_CAMPO();
            vcampo.GRV_CODIGO = Convert.ToInt32(Request.QueryString["codigo"]);
            vcampo.MEN_CODIGO = Convert.ToInt32(Request.QueryString["mensaje"]);
            vcampo.CAM_CODIGO = Convert.ToInt32(Request.QueryString["campo"]);
            int criterio = Convert.ToInt32(ddlCriterio.SelectedValue);
            vcampo.VLC_INCLUSION_EXCLUSION = criterio;
            if (criterio == BusinessLayer.Mensajeria.ValidacionCampoBL.obtenerCriterioAplicacionInclusion())
            {
                //Inclusión o Exclusión
                int tipoRegla = Convert.ToInt32(ddlTipoRegla.SelectedValue);
                if (tipoRegla == BusinessLayer.Mensajeria.ValidacionCampoBL.obtenerTipoReglaCondicion())
                {
                    //Condición
                    vcampo.VLC_CONDICION = Convert.ToInt32(ddlCondicion.SelectedValue);
                    vcampo.VLC_VALOR = txtValorComparacion.Text;
                }
                else
                {
                    //Tabla de valores
                    //TABLA tabla = BusinessLayer.Mensajeria.TablaBL.obtenerTablaPorCodigo();
                    vcampo.TABLA = new TABLA() { TBL_CODIGO = Convert.ToInt32(ddlNombreTabla.SelectedValue) };
                    //COLUMNA columna = BusinessLayer.Mensajeria.ColumnaBL.obtenerColumnaPorCodigo(Convert.ToInt32(ddlNombreTabla.SelectedValue));
                    vcampo.COLUMNA = new COLUMNA() { COL_CODIGO = Convert.ToInt32(ddlNombreTabla.SelectedValue) };
                }
            }
            else
            {
                //Especial
                vcampo.VLC_PROCEDIMIENTO = txtProcedure.Text;
            }

            BusinessLayer.Mensajeria.ValidacionCampoBL.insertarValidacionCampo(vcampo);

            Response.Redirect("ConsultarDetalleReglasValidacion.aspx?codigo=" + Request.QueryString["codigo"] + "&grupo=" + Request.QueryString["grupo"] + "&mensaje=" + Request.QueryString["mensaje"] + "&campo=" + Request.QueryString["campo"]);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
