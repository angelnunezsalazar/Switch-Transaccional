using System;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLayer.Operacion;

namespace UserInterface.Operacion.MantenimientoParametroTransformacionCampo
{
    public partial class ConsultarParametroTransformacionCampo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Operacion/MantenimientoParametroTransformacionCampo/AgregarParametroTransformacionCampo.aspx?Transformacion="+
                Request.QueryString["Transformacion"] + "&Mensaje=" + Request.QueryString["Mensaje"] + "&Campo=" + Request.QueryString["Campo"]);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName=="Modificar")
            {
                Response.Redirect("~/Operacion/MantenimientoParametroTransformacionCampo/ModificarParametroTransformacionCampo.aspx?Transformacion=" +
                Request.QueryString["Transformacion"] + "&Mensaje=" + Request.QueryString["Mensaje"] + "&Campo=" + Request.QueryString["Campo"] + "&Parametro=" + e.CommandArgument.ToString());
            }
        }

        protected void dsParametroTransformacionCampo_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion resultado = (EstadoOperacion)e.ReturnValue;
            if (!resultado.Estado)
            {
                lblMensaje.Text = resultado.Mensaje;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            string url = string.Format("~/Operacion/MantenimientoTransformacionCampo/ModificarTransformacionCampo.aspx?"+
                "Transformacion={0}&Mensaje={1}&Campo={2}", 
                Request.QueryString["Transformacion"], 
                Request.QueryString["Mensaje"], 
                Request.QueryString["Campo"]);

            Response.Redirect(url);
        }

        protected string ObtenerNombreTipoTransformacion(string codigoTipoTransformacion)
        {
            return TipoTransformacionBL.obtenerNombrePorCodigo(int.Parse(codigoTipoTransformacion));
        }
    }
}
