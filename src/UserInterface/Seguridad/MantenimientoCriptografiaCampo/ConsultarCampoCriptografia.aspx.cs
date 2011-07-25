using System;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLayer.Seguridad;

namespace UserInterface.Seguridad.MantenimientoCriptografiaCampo
{
    public partial class ConsultarCampoCriptografia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string Dinamica = Request.QueryString["Dinamica"];
            string Mensaje = ((HiddenField)this.frmDinamicaCriptografia.FindControl("hdnCodigoMensaje")).Value;
            string Ruta = string.Format("~/Seguridad/MantenimientoCriptografiaCampo/AgregarCampoCriptografia.aspx?Dinamica={0}&Mensaje={1}", Dinamica, Mensaje);
            Response.Redirect(Ruta);
        }

        protected string ObtenerDescripcionTipoLlave(string tipoLlave)
        {
            if (tipoLlave == string.Empty)
                return string.Empty;

            int codigoTipoLlave = int.Parse(tipoLlave);
            return TipoLlaveBL.obtenerTipoLlave(codigoTipoLlave);
        }

        protected string ObtenerDescripcionAlgoritmo(string algoritmo)
        {
            return AlgoritmoBL.obtenerAlgoritmo(int.Parse(algoritmo));
        }

        protected string ObtenerCodigoMensaje()
        {
            return ((HiddenField)this.frmDinamicaCriptografia.FindControl("hdnCodigoMensaje")).Value;
        }

        protected string ObtenerDescripcionOperacionLlave(string operacionLlave)
        {
            if (operacionLlave == string.Empty)
                return string.Empty;

            int codigoOperacionLlave = int.Parse(operacionLlave);
            return OperacionLlaveBL.obtenerOperacionLlave(codigoOperacionLlave);
        }

        protected void dsCampoCriptografia_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion Estado = (EstadoOperacion)e.ReturnValue;

            if (!Estado.Estado)
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }
    }
}
