using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoTabla
{
    public partial class ConsultarTabla : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void imgAgregar_Click(object sender, ImageClickEventArgs e)
        {
            if (Page.IsValid)
            {
                string nombre = ((TextBox)this.grvTabla.FooterRow.FindControl("txtNombre")).Text;
                string descripcion = ((TextBox)this.grvTabla.FooterRow.FindControl("txtDescripcion")).Text;

                TABLA nuevaTabla = new TABLA();
                nuevaTabla.TBL_NOMBRE = nombre;
                nuevaTabla.TBL_DESCRIPCION = descripcion;

                EstadoOperacion Estado = BusinessLayer.Mensajeria.TablaBL.insertarTabla(nuevaTabla);

                if (Estado.Estado)
                {
                    this.grvTabla.DataBind();
                }
                else
                {
                    this.lblMensaje.Text = Estado.Mensaje;
                }
            }
        }

        protected void grvTabla_DataBound(object sender, EventArgs e)
        {
            if (this.grvTabla.Rows.Count > 0)
            {
                if (this.grvTabla.Rows[0].RowState == DataControlRowState.Normal)
                {
                    if (((Label)this.grvTabla.Rows[0].FindControl("lblNombreTabla")).Text == "")
                    {
                        this.grvTabla.Columns[3].Visible = false;
                        this.grvTabla.Columns[4].Visible = false;
                        this.grvTabla.Columns[5].Visible = true;
                    }
                    else
                    {
                        this.grvTabla.Columns[3].Visible = true;
                        this.grvTabla.Columns[4].Visible = true;
                        this.grvTabla.Columns[5].Visible = true;
                    }
                }
            }
        }

        protected void dsTabla_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            List<TABLA> lista = (List<TABLA>)e.ReturnValue;
            if (lista.Count == 0)
            {
                TABLA tabla = new TABLA();
                tabla.TBL_NOMBRE = "";
                tabla.TBL_DESCRIPCION = "";
                lista.Add(tabla);
            }
        }

        protected void dsTabla_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            VerificarResultado(e);
        }

        protected void dsTabla_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
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
    }
}
