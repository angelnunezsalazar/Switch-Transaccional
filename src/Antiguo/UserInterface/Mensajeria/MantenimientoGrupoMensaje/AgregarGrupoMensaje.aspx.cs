using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoGrupoMensaje
{
    public partial class AgregarGrupoMensaje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void dsGrupoMensaje_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BusinessEntity.EstadoOperacion Estado = ((BusinessEntity.EstadoOperacion)e.ReturnValue);

            if (Estado.Estado)
            {
                Response.Redirect("~/Mensajeria/MantenimientoGrupoMensaje/ConsultarGrupoMensaje.aspx");
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void dsGrupoMensaje_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            GRUPO_MENSAJE grupoMensaje = (GRUPO_MENSAJE)e.InputParameters[0];
            DropDownList drlTipoMensaje = (DropDownList)this.frmGrupoMensaje.FindControl("drlTipoMensaje");

            TIPO_MENSAJE tipoMensaje = new TIPO_MENSAJE() { TMJ_CODIGO = int.Parse(drlTipoMensaje.SelectedValue) };
            grupoMensaje.TIPO_MENSAJE = tipoMensaje;


            ViewState.Add("GrupoMensaje", grupoMensaje);
        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                GRUPO_MENSAJE grupoMensaje = (GRUPO_MENSAJE)ViewState["GrupoMensaje"];

                ((TextBox)this.frmGrupoMensaje.FindControl("txtNombre")).Text = grupoMensaje.GMJ_NOMBRE;
                ((TextBox)this.frmGrupoMensaje.FindControl("txtDescripcion")).Text = grupoMensaje.GMJ_DESCRIPCION;

                for (int i = 0; i < ((DropDownList)this.frmGrupoMensaje.FindControl("drlTipoMensaje")).Items.Count; i++)
                {
                    if (((DropDownList)this.frmGrupoMensaje.FindControl("drlTipoMensaje")).Items[i].Value == grupoMensaje.TIPO_MENSAJE.TMJ_CODIGO.ToString())
                    {
                        ((DropDownList)this.frmGrupoMensaje.FindControl("drlTipoMensaje")).SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }
}
