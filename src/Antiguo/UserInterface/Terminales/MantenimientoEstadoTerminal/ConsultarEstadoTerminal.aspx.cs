using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Terminales.MantenimientoEstadoTerminal
{
    using System.Web.UI;

    using BusinessLayer.Terminales;

    public partial class ConsultarEstadoTerminal : Page
    {
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string descripcion = ((TextBox)this.grvEstadoTerminal.FooterRow.FindControl("txtDescripcion")).Text;

                EstadoTerminalBL estadoTerminalBl = new EstadoTerminalBL();
                try
                {
                    estadoTerminalBl.Insertar(new EstadoTerminal { Nombre = descripcion });
                    this.grvEstadoTerminal.DataBind();
                }
                catch { }
            }
        }

        protected void dsEstadoTerminal_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            List<EstadoTerminal> lista = (List<EstadoTerminal>)e.ReturnValue;
            if (lista.Count == 0)
            {
                EstadoTerminal estadoTerminal = new EstadoTerminal { Nombre = "" };
                lista.Add(estadoTerminal);
            }
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
