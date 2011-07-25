using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Terminales.MantenimientoEstadoTerminal
{
    public partial class ConsultarEstadoTerminal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string descripcion = ((TextBox)this.grvEstadoTerminal.FooterRow.FindControl("txtDescripcion")).Text;

                ESTADO_TERMINAL nuevoEstadoTerminal = new ESTADO_TERMINAL();
                nuevoEstadoTerminal.EST_NOMBRE = descripcion;

                EstadoOperacion Estado = BusinessLayer.Terminales.EstadoTerminalBL.insertarEstadoTerminal(nuevoEstadoTerminal);

                if (Estado.Estado)
                {
                    this.grvEstadoTerminal.DataBind();
                }
                else
                {
                    this.lblMensaje.Text = Estado.Mensaje;
                }
            }
        }

        protected void dsEstadoTerminal_Updated(object sender, ObjectDataSourceStatusEventArgs e)
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

        protected void dsEstadoTerminal_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            VerificarResultado(e);
        }

        protected void dsEstadoTerminal_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            List<ESTADO_TERMINAL> lista = (List<ESTADO_TERMINAL>)e.ReturnValue;
            if (lista.Count == 0)
            {
                ESTADO_TERMINAL estadoTerminal = new ESTADO_TERMINAL();
                estadoTerminal.EST_NOMBRE = "";
                lista.Add(estadoTerminal);
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {

        }

        protected void grvEstadoTerminal_DataBound(object sender, EventArgs e)
        {
            if (this.grvEstadoTerminal.Rows.Count > 0)
            {
                if (this.grvEstadoTerminal.Rows[0].RowState == DataControlRowState.Normal)
                {
                    if (((Label)this.grvEstadoTerminal.Rows[0].FindControl("lblNombreItem")).Text == "")
                    {
                        this.grvEstadoTerminal.Columns[2].Visible = false;
                        this.grvEstadoTerminal.Columns[3].Visible = false;
                    }
                    else
                    {
                        this.grvEstadoTerminal.Columns[2].Visible = true;
                        this.grvEstadoTerminal.Columns[3].Visible = true;
                    }
                }
            }
        }

    }
}
