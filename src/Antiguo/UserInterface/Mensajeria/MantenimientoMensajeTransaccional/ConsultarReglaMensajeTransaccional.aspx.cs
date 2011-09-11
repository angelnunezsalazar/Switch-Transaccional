using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoMensajeTransaccional
{
    public partial class ConsultarReglaMensajeTransaccional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void dsReglaMensajeTransaccional_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

        }

        protected void grvReglaMensajeTransaccional_DataBound(object sender, EventArgs e)
        {
            if (this.grvReglaMensajeTransaccional.Rows.Count > 0)
            {
                if (this.grvReglaMensajeTransaccional.Rows[0].RowState == DataControlRowState.Normal)
                {
                    if (((Label)this.grvReglaMensajeTransaccional.Rows[0].FindControl("lblCampoItem")).Text == "")
                    {
                        ((Label)this.grvReglaMensajeTransaccional.Rows[0].FindControl("lblCampoItem")).Text = " ";
                        ((Label)this.grvReglaMensajeTransaccional.Rows[0].FindControl("lblCampoItem")).ForeColor = System.Drawing.Color.White;
                        this.grvReglaMensajeTransaccional.Columns[3].Visible = false;
                        this.grvReglaMensajeTransaccional.Columns[4].Visible = false;
                        this.grvReglaMensajeTransaccional.Columns[5].Visible = false;
                    }
                    else
                    {
                        this.grvReglaMensajeTransaccional.Columns[3].Visible = true;
                        this.grvReglaMensajeTransaccional.Columns[4].Visible = true;
                        this.grvReglaMensajeTransaccional.Columns[5].Visible = true;
                    }
                }

            }
        }

        protected void imgAgregar_Click(object sender, ImageClickEventArgs e)
        {
            REGLA_MENSAJE_TRANSACCIONAL reglaMensajeTransaccional = new REGLA_MENSAJE_TRANSACCIONAL();
            reglaMensajeTransaccional.RMT_VALOR = ((TextBox)this.grvReglaMensajeTransaccional.FooterRow.FindControl("txtValorFooter")).Text;

            CAMPO campo = new CAMPO()
            {
                MEN_CODIGO = int.Parse(((HiddenField)this.frmMensajeTransaccional.FindControl("hdnCodigoMensaje")).Value),
                CAM_CODIGO = int.Parse(((DropDownList)this.grvReglaMensajeTransaccional.FooterRow.FindControl("drlCampoFooter")).SelectedValue)
            };
            reglaMensajeTransaccional.CAMPO = campo;

            MENSAJE_TRANSACCIONAL mensajeTransaccional = new MENSAJE_TRANSACCIONAL()
            {
                MTR_CODIGO = int.Parse(Request.QueryString["CodigoMensajeTransaccional"])
            };
            reglaMensajeTransaccional.MENSAJE_TRANSACCIONAL = mensajeTransaccional;

            EstadoOperacion Estado = BusinessLayer.Mensajeria.ReglaMensajeTransaccionalBL.insertarReglaMensajeTransaccional(reglaMensajeTransaccional);

            if (Estado.Estado)
            {
                this.grvReglaMensajeTransaccional.DataBind();
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void dsReglaMensajeTransaccional_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            List<REGLA_MENSAJE_TRANSACCIONAL> lista = (List<REGLA_MENSAJE_TRANSACCIONAL>)e.ReturnValue;
            if (lista.Count == 0)
            {
                REGLA_MENSAJE_TRANSACCIONAL reglaMensaje = new REGLA_MENSAJE_TRANSACCIONAL();
                reglaMensaje.RMT_VALOR = "";
                lista.Add(reglaMensaje);
            }
        }

        protected void dsReglaMensajeTransaccional_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            devolverResultado(e);
        }

        private void devolverResultado(ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion estado = (EstadoOperacion)e.ReturnValue;
            if (!estado.Estado)
            {
                lblMensaje.Text = estado.Mensaje;
            }
        }

        protected void dsReglaMensajeTransaccional_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            devolverResultado(e);
        }
    }
}
