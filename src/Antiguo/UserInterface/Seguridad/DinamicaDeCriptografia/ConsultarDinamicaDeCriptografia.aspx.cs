using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Seguridad.DinamicaDeCriptografia
{
    public partial class ConsultarDinamicaDeCriptografia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void imgAgregar_Click(object sender, ImageClickEventArgs e)
        {

            DINAMICA_CRIPTOGRAFIA dinamicaCriptografia = new DINAMICA_CRIPTOGRAFIA()
            {
                DNC_NOMBRE = ((TextBox)this.grvDinamicaCriptografia.FooterRow.FindControl("txtNombreFooter")).Text,
                DNC_TIPO = int.Parse(((DropDownList)this.grvDinamicaCriptografia.FooterRow.FindControl("drlTipoFooter")).SelectedValue)
            };

            MENSAJE mensaje = new MENSAJE()
            {
                MEN_CODIGO = int.Parse(this.drlMensaje.SelectedValue)
            };

            dinamicaCriptografia.MENSAJE = mensaje;

            EstadoOperacion Estado = BusinessLayer.Seguridad.DinamicaCriptografiaBL.insertarDinamicaCriptografia(dinamicaCriptografia);

            if (Estado.Estado)
            {
                this.grvDinamicaCriptografia.DataBind();
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void grvDinamicaCriptografia_DataBound(object sender, EventArgs e)
        {
            if (this.grvDinamicaCriptografia.Rows.Count > 0)
            {
                if (this.grvDinamicaCriptografia.Rows[0].RowState == DataControlRowState.Normal)
                {
                    if (((Label)this.grvDinamicaCriptografia.Rows[0].FindControl("lblNombreItem")).Text == "")
                    {
                        ((Label)this.grvDinamicaCriptografia.Rows[0].FindControl("lblTipoItem")).Text = "";

                        this.grvDinamicaCriptografia.Columns[3].Visible = false;
                        this.grvDinamicaCriptografia.Columns[4].Visible = false;
                        this.grvDinamicaCriptografia.Columns[5].Visible = false;
                    }
                    else
                    {
                        this.grvDinamicaCriptografia.Columns[3].Visible = true;
                        this.grvDinamicaCriptografia.Columns[4].Visible = true;
                        this.grvDinamicaCriptografia.Columns[5].Visible = true;
                    }
                }
            }
        }

        protected void dsDinamicaCriptografia_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (drlMensaje.SelectedValue != "-1")
            {
                List<DINAMICA_CRIPTOGRAFIA> lista = (List<DINAMICA_CRIPTOGRAFIA>)e.ReturnValue;
                if (lista.Count == 0)
                {
                    DINAMICA_CRIPTOGRAFIA dinamicaCriptografia = new DINAMICA_CRIPTOGRAFIA();
                    dinamicaCriptografia.DNC_NOMBRE = "";
                    lista.Add(dinamicaCriptografia);
                }
            }
        }

        protected void dsDinamicaCriptografia_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (drlMensaje.SelectedValue == "-1" || drlGrupoMensaje.SelectedValue == "-1")
            {
                e.Cancel = true;
            }
        }

        protected void dsDinamicaCriptografia_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion Estado = (EstadoOperacion)e.ReturnValue;

            if (!Estado.Estado)
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void dsDinamicaCriptografia_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion Estado = (EstadoOperacion)e.ReturnValue;

            if (!Estado.Estado)
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }
    }
}
