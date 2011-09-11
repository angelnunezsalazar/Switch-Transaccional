using System;
using System.Web.UI.WebControls;
using BusinessLayer.Operacion;

namespace UserInterface.Operacion.MantenimientoTransformacionCampo
{
    public partial class ConsultarTransformacionCampo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void VerDetalles(string Transformacion, string Mensaje, string Campo)
        {
            int cantidadParametros = TransformacionCampoBL.obtenerCantidadTransformacionCampo(int.Parse(Transformacion), int.Parse(Mensaje), int.Parse(Campo));
            string pagina = string.Empty;
            if (cantidadParametros > 0)
            {
                pagina = "ModificarTransformacionCampo";
            }
            else
            {
                pagina = "AgregarTransformacionCampo";
            }
            string url=string.Format("~/Operacion/MantenimientoTransformacionCampo/{0}.aspx?Transformacion={1}&Mensaje={2}&Campo={3}", pagina, Transformacion, Mensaje, Campo);

            Response.Redirect(url);
        }

        protected void grvCampoDestino_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string Tranformacion = Request.QueryString["Transformacion"];
                string Mensaje = this.grvCampoDestino.DataKeys[index].Values["MEN_CODIGO"].ToString();
                string Campo = this.grvCampoDestino.DataKeys[index].Values["CAM_CODIGO"].ToString();

                VerDetalles(Tranformacion, Mensaje, Campo);
            }
        }
    }
}
