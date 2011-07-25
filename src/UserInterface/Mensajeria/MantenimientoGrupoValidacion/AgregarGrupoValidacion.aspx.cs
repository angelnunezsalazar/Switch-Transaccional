using System;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoGrupoValidacion
{
    public partial class AgregarGrupoValidacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            GRUPO_VALIDACION grupo = new GRUPO_VALIDACION();
            grupo.GRV_NOMBRE = txtNombre.Text;

            BusinessLayer.Mensajeria.GrupoValidacionBL.insertarGrupoValidacion(grupo, Convert.ToInt32(Request.QueryString["mensaje"]));

            Response.Redirect("ModificarGrupoValidacion.aspx?codigo=" + grupo.GRV_CODIGO);
        }
    }
}
