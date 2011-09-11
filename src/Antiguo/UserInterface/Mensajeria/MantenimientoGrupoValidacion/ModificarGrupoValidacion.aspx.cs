using System;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoGrupoValidacion
{
    public partial class ModificarGrupoValidacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int codigo = Convert.ToInt32(Request.QueryString["codigo"]);
                GRUPO_VALIDACION grupo = BusinessLayer.Mensajeria.GrupoValidacionBL.obtenerGrupoValidacionPorCodigo(codigo);

                lblCodigo.Text = grupo.GRV_CODIGO.ToString();
                txtNombre.Text = grupo.GRV_NOMBRE;
                lblGrupoMensaje.Text = grupo.MENSAJE.GRUPO_MENSAJE.GMJ_NOMBRE;
                hdnGrupoMensaje.Value = grupo.MENSAJE.GRUPO_MENSAJE.GMJ_CODIGO.ToString();
                lblMensaje.Text = grupo.MENSAJE.MEN_NOMBRE;
                hdnMensaje.Value = grupo.MENSAJE.MEN_CODIGO.ToString();
            }
        }

        protected void btnDefinirReglas_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConsultarDetalleReglasValidacion.aspx?codigo=" + Request.QueryString["codigo"] + "&grupo=" + hdnGrupoMensaje.Value + "&mensaje=" + hdnMensaje.Value);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            int codigo = Convert.ToInt32(Request.QueryString["codigo"]);
            GRUPO_VALIDACION grupo = BusinessLayer.Mensajeria.GrupoValidacionBL.obtenerGrupoValidacionPorCodigo(codigo);
            grupo.GRV_NOMBRE = txtNombre.Text;
            BusinessLayer.Mensajeria.GrupoValidacionBL.modificarGrupoValidacion(grupo);
        }
    }
}
