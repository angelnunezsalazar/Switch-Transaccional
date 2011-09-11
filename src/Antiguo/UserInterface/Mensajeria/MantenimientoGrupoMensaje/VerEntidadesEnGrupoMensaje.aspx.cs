using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoGrupoMensaje
{
    public partial class VerEntidadesEnGrupoMensaje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void dsEntidad_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            List<EntidadComunicacion> lista = (List<EntidadComunicacion>)e.ReturnValue;
            if (lista.Count == 0)
            {
                EntidadComunicacion entidad = new EntidadComunicacion();
                entidad.EDC_NOMBRE = "";
                lista.Add(entidad);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((Label)e.Row.FindControl("lblNombre")).Text == "")
                {
                    ((Label)e.Row.FindControl("lblNumero")).Text = "";
                    ((ImageButton)e.Row.FindControl("imgEliminar")).ImageUrl = "~/Includes/Imagenes/blank.gif";
                    ((ImageButton)e.Row.FindControl("imgEliminar")).Enabled = false;

                }
            }
        }

        protected void imgNuevo_Click(object sender, EventArgs e)
        {
            string codEntidadComunicacion = ((DropDownList)this.GridView1.FooterRow.FindControl("drlEntidadComunicacion")).SelectedValue;
            string codGrupoMensaje = Request.QueryString["Codigo"];

            EstadoOperacion Estado = BusinessLayer.Comunicacion.EntidadComunicacionBL.agregarEntidadAGrupoMensaje(int.Parse(codGrupoMensaje), int.Parse(codEntidadComunicacion));

            if (Estado.Estado)
            {
                this.GridView1.DataBind();
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void dsEntidad_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BusinessEntity.EstadoOperacion Estado = ((BusinessEntity.EstadoOperacion)e.ReturnValue);
            if (!Estado.Estado)
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }
    }
}
