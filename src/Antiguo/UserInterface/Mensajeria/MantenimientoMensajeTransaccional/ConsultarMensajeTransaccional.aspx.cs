using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoMensajeTransaccional
{
    public partial class ConsultarMensajeTransaccional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void dsMensajeTransaccional_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (drlMensaje.SelectedValue == "-1")
                e.Cancel = true;
        }

        protected void dsMensajeTransaccional_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (drlMensaje.SelectedValue != "-1")
            {
                List<MENSAJE_TRANSACCIONAL> lista = (List<MENSAJE_TRANSACCIONAL>)e.ReturnValue;
                if (lista.Count == 0)
                {
                    MENSAJE_TRANSACCIONAL mensajeTransaccional = new MENSAJE_TRANSACCIONAL();
                    mensajeTransaccional.MTR_NOMBRE = "";
                    lista.Add(mensajeTransaccional);
                }
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            if (this.grvMensajeTransaccional.Rows.Count > 0)
            {
                if (this.grvMensajeTransaccional.Rows[0].RowState==DataControlRowState.Normal)
                {
                    if (((Label)this.grvMensajeTransaccional.Rows[0].FindControl("lblNombreItem")).Text == "")
                    {
                        ((Label)this.grvMensajeTransaccional.Rows[0].FindControl("lblNombreItem")).Text = "";
                        //((Label)this.grvMensajeTransaccional.Rows[0].FindControl("lblNombreItem")).ForeColor = System.Drawing.Color.White;
                        this.grvMensajeTransaccional.Columns[3].Visible = false;
                        this.grvMensajeTransaccional.Columns[4].Visible = false;
                        this.grvMensajeTransaccional.Columns[5].Visible = false;
                    }
                    else
                    {
                        this.grvMensajeTransaccional.Columns[3].Visible = true;
                        this.grvMensajeTransaccional.Columns[4].Visible = true;
                        this.grvMensajeTransaccional.Columns[5].Visible = true;
                    }
                }
            }
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            MENSAJE_TRANSACCIONAL mensajeTransaccional = new MENSAJE_TRANSACCIONAL();
            mensajeTransaccional.MTR_NOMBRE = ((TextBox)this.grvMensajeTransaccional.FooterRow.FindControl("txtNombreFooter")).Text;

            RadioButtonList rdlCondicionMensaje = (RadioButtonList)this.grvMensajeTransaccional.FooterRow.FindControl("rblCondicionMensajeFooter");
            mensajeTransaccional.CONDICION_MENSAJE = new CONDICION_MENSAJE()
                                                   {
                                                       CNM_CODIGO = int.Parse(rdlCondicionMensaje.SelectedValue)
                                                   };

            mensajeTransaccional.MENSAJE = new MENSAJE()
                                         {
                                             MEN_CODIGO=int.Parse(this.drlMensaje.SelectedValue)
                                         };


            EstadoOperacion Estado = BusinessLayer.Mensajeria.MensajeTransaccionalBL.insertarMensajeTransaccional(mensajeTransaccional);

            if (Estado.Estado)
            {
                this.grvMensajeTransaccional.DataBind();
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void dsMensajeTransaccional_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion Estado = (EstadoOperacion)e.ReturnValue;

            if (!Estado.Estado)
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void dsMensajeTransaccional_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion Estado = (EstadoOperacion)e.ReturnValue;

            if (!Estado.Estado)
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void dsMensajeTransaccional_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            MENSAJE_TRANSACCIONAL mensajeTransaccional = (MENSAJE_TRANSACCIONAL)e.InputParameters[0];

            RadioButtonList rdlCondicionMensaje = (RadioButtonList)this.grvMensajeTransaccional.Rows[this.grvMensajeTransaccional.EditIndex].FindControl("rblCondicionMensajeEdit");
            mensajeTransaccional.CONDICION_MENSAJE = new CONDICION_MENSAJE()
            {
                CNM_CODIGO = int.Parse(rdlCondicionMensaje.SelectedValue)
            };
        }

        protected void rblCondicionMensajeFooter_DataBound(object sender, EventArgs e)
        {
            RadioButtonList radioFooter = sender as RadioButtonList;
            if (radioFooter.Items.Count>0)
            {
                radioFooter.Items[0].Selected=true;
            }
        }
    }
}
