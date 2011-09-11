using System;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoMensaje
{
    public partial class ConsultarMensaje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                this.lblMensaje.Text = "";
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string url = "~/Mensajeria/MantenimientoMensaje/AgregarMensaje.aspx?Codigo=" +
                         drlMensaje.SelectedValue +
                         "&Nombre=" + drlMensaje.SelectedItem.Text;
            Response.Redirect(url);
        }

        protected void DropDownList1_DataBound(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Codigo"] != null)
                {
                    drlMensaje.SelectedIndex = -1;
                    drlMensaje.Items.FindByValue(Request.QueryString["Codigo"]).Selected=true;
                }
            }
        }

        protected void grdMensaje_DataBound(object sender, EventArgs e)
        {
            if (this.drlMensaje.SelectedValue == "-1")
                this.btnAgregar.Visible = false;
            else 
                this.btnAgregar.Visible = true;   
        }

        protected void dsMensaje_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion resultado = (EstadoOperacion)e.ReturnValue;
            if (!resultado.Estado)
            {
                this.lblMensaje.Text = resultado.Mensaje;
            }
        }

    }
}
