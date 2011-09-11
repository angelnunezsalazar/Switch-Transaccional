using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoColumna
{
    public partial class ConsultarColumna : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void imgAgregar_Click(object sender, ImageClickEventArgs e)
        {
            COLUMNA columna = new COLUMNA()
            {
                COL_NOMBRE = ((TextBox)this.grvColumna.FooterRow.FindControl("txtNombreFooter")).Text,
                COL_LONGITUD = int.Parse(((TextBox)this.grvColumna.FooterRow.FindControl("txtLongitudFooter")).Text)
            };

            TABLA tabla = new TABLA()
            {
                TBL_CODIGO = int.Parse(((HiddenField)this.frmTabla.FindControl("hdnCodigo")).Value)
            };
            columna.TABLA = tabla;

            TIPO_DATO_COLUMNA tipoDato = new TIPO_DATO_COLUMNA()
            {
                TDC_CODIGO = int.Parse(((DropDownList)this.grvColumna.FooterRow.FindControl("drlTipoFooter")).SelectedValue)
            };
            columna.TIPO_DATO_COLUMNA = tipoDato;

            EstadoOperacion Estado = BusinessLayer.Mensajeria.ColumnaBL.insertarColumna(columna);

            if (Estado.Estado)
            {
                this.grvColumna.DataBind();
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void grvcolumna_DataBound(object sender, EventArgs e)
        {
            if (this.grvColumna.Rows.Count > 0)
            {
                if (this.grvColumna.Rows[0].RowState == DataControlRowState.Normal)
                {
                    if (((Label)this.grvColumna.Rows[0].FindControl("lblNombreItem")).Text == "")
                    {
                        ((Label)this.grvColumna.Rows[0].FindControl("lblLongitudItem")).Text = "";

                        this.grvColumna.Columns[4].Visible = false;
                        this.grvColumna.Columns[5].Visible = false;
                    }
                    else
                    {
                        this.grvColumna.Columns[4].Visible = true;
                        this.grvColumna.Columns[5].Visible = true;
                    }
                }
            }
        }

        protected void dsColumna_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (Request.QueryString["Tabla"] != "-1")
            {
                List<COLUMNA> lista = (List<COLUMNA>)e.ReturnValue;
                if (lista.Count == 0)
                {
                    COLUMNA columna = new COLUMNA();
                    columna.COL_NOMBRE = "";
                    TIPO_DATO_COLUMNA tipoDato = new TIPO_DATO_COLUMNA()
                    {
                        TDC_NOMBRE=""
                    };
                    lista.Add(columna);
                }
            }
        }

        protected void dsColumna_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion Estado = (EstadoOperacion)e.ReturnValue;

            if (!Estado.Estado)
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void dsColumna_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion Estado = (EstadoOperacion)e.ReturnValue;

            if (!Estado.Estado)
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void dsColumna_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            COLUMNA columna = (COLUMNA)e.InputParameters[0];
            TIPO_DATO_COLUMNA tipoDatoColumna = new TIPO_DATO_COLUMNA()
            {
                TDC_CODIGO = int.Parse(((DropDownList)this.grvColumna.Rows
                [this.grvColumna.EditIndex].FindControl("drlTipoEdit")).SelectedValue)
            };

            columna.TIPO_DATO_COLUMNA = tipoDatoColumna;
        }
    }
}
