using System;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoGrupoValidacion
{
    public partial class ConsultarDetalleReglasValidacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GRUPO_VALIDACION grupo = BusinessLayer.Mensajeria.GrupoValidacionBL.obtenerGrupoValidacionPorCodigo(Convert.ToInt32(Request.QueryString["codigo"]));
            lblNombre.Text = grupo.GRV_NOMBRE;

            if (drlCampo.SelectedValue != "0")
            {
                btnAgregar.Visible = true;
                grvReglasCampo.DataBind();
            }
            else
            {
                btnAgregar.Visible = false;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["campo"] != "")
                {
                    drlCampo.SelectedValue = Request.QueryString["campo"];
                    btnAgregar.Visible = true;
                }
            }


        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarValidacionCampo.aspx?codigo=" + Request.QueryString["codigo"] + "&grupo=" + Request.QueryString["grupo"] + "&mensaje=" + Request.QueryString["mensaje"] + "&campo=" + drlCampo.SelectedValue);
        }

        protected void dsValidacionCampo_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            VerificarResultado(e);
        }

        private void VerificarResultado(ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion Estado = (EstadoOperacion)e.ReturnValue;

            if (!Estado.Estado)
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void grvReglasCampo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            VALIDACION_CAMPO vcampo = (VALIDACION_CAMPO)e.Row.DataItem;
            if (vcampo != null)
            {
                Label lblTipoRegla = (Label)e.Row.Cells[0].FindControl("lblTipoRegla");
                Label lblTipoValidacion = (Label)e.Row.Cells[1].FindControl("lblTipoValidacion");
                Label lblValorReferencia = (Label)e.Row.Cells[2].FindControl("lblValorReferencia");
                Label lblCampoTabla = (Label)e.Row.Cells[3].FindControl("lblCampoTabla");

                if (vcampo.VLC_INCLUSION_EXCLUSION == BusinessLayer.Mensajeria.ValidacionCampoBL.obtenerCriterioAplicacionInclusion())
                {
                    lblTipoRegla.Text = "Inclusi&oacute;n";
                    if (vcampo.TABLA == null)
                    {
                        if (vcampo.VLC_CONDICION == BusinessLayer.Mensajeria.ValidacionCampoBL.obtenerCondicionIgual())
                        {
                            lblTipoValidacion.Text = "=";
                        }
                        else if (vcampo.VLC_CONDICION == BusinessLayer.Mensajeria.ValidacionCampoBL.obtenerCondicionDiferente())
                        {
                            lblTipoValidacion.Text = "<>";
                        }
                        else if (vcampo.VLC_CONDICION == BusinessLayer.Mensajeria.ValidacionCampoBL.obtenerCondicionMayor())
                        {
                            lblTipoValidacion.Text = ">";
                        }
                        else if (vcampo.VLC_CONDICION == BusinessLayer.Mensajeria.ValidacionCampoBL.obtenerCondicionMenor())
                        {
                            lblTipoValidacion.Text = "<";
                        }
                        else if (vcampo.VLC_CONDICION == BusinessLayer.Mensajeria.ValidacionCampoBL.obtenerCondicionMayorIgual())
                        {
                            lblTipoValidacion.Text = ">=";
                        }
                        else if (vcampo.VLC_CONDICION == BusinessLayer.Mensajeria.ValidacionCampoBL.obtenerCondicionMenorIgual())
                        {
                            lblTipoValidacion.Text = "<=";
                        }
                        lblValorReferencia.Text = vcampo.VLC_VALOR.ToString();
                    }
                    else
                    {
                        lblTipoValidacion.Text = "Tabla";
                        lblValorReferencia.Text = vcampo.TABLA.TBL_NOMBRE;
                        lblCampoTabla.Text = vcampo.COLUMNA.COL_NOMBRE;
                    }
                }
                else if (vcampo.VLC_INCLUSION_EXCLUSION == BusinessLayer.Mensajeria.ValidacionCampoBL.obtenerCriterioAplicacionProcedimiento())
                {
                    lblTipoRegla.Text = "Especial";
                    lblValorReferencia.Text = vcampo.VLC_PROCEDIMIENTO;
                }
            }
        }
    }
}
