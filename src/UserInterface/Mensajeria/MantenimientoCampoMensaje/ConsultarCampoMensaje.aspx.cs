using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoCampoMensaje
{
    public partial class ConsultarCampoMensaje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string url = "~/Mensajeria/MantenimientoCampoMensaje/AgregarCampoMensaje.aspx?Codigo=" +
             Request.QueryString["Codigo"];
            Response.Redirect(url);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mensajeria/MantenimientoMensaje/ConsultarMensaje.aspx?Codigo=" + Request.QueryString["Codigo"]);
        }

        protected void dsCampo_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            List<CAMPO> lista = (List<CAMPO>)e.ReturnValue;
            if (lista.Count == 0)
            {
                CAMPO campo = new CAMPO() 
                { 
                    CAM_NOMBRE="",
                };
                lista.Add(campo);
            }
        }

        protected void grvCampo_DataBound(object sender, EventArgs e)
        {
            if (this.grvCampo.Rows.Count > 0)
            {
                if (this.grvCampo.Rows[0].RowState == DataControlRowState.Normal)
                {
                    if (((Label)this.grvCampo.Rows[0].FindControl("lblNombreItem")).Text == "")
                    {
                        ((Label)this.grvCampo.Rows[0].FindControl("lblPosicionRelativa")).Text = "";
                        ((Label)this.grvCampo.Rows[0].FindControl("lblRequeridoItem")).Text = "";
                        this.grvCampo.Columns[3].Visible = false;
                        this.grvCampo.Columns[4].Visible = false;
                        this.grvCampo.Columns[5].Visible = false;
                        this.grvCampo.Columns[6].Visible = false;
                        this.grvCampo.Columns[7].Visible = false;
                        this.grvCampo.Columns[8].Visible = false;
                    }
                    else
                    {
                        this.grvCampo.Columns[3].Visible = true;
                        this.grvCampo.Columns[4].Visible = true;
                        this.grvCampo.Columns[5].Visible = true;
                        this.grvCampo.Columns[6].Visible = true;
                        this.grvCampo.Columns[7].Visible = true;
                        this.grvCampo.Columns[8].Visible = true;
                    }
                }
            }
        }

        public string Requerido(string requerido)
        {
            if (requerido=="True")
            {
                return "Requerido";
            }
            else
            {
                return "Opcional";
            }
        }

        protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
        {
            CAMPO campo = new CAMPO()
            {
                CAM_REQUERIDO = bool.Parse(((RadioButtonList)this.grvCampo.FooterRow.FindControl("rblRequeridoFooter")).SelectedValue),
                MEN_CODIGO=int.Parse(((HiddenField)this.frmMensaje.FindControl("hdnCodigoMensaje")).Value)
            };

            CAMPO_PLANTILLA campoPlantilla=new CAMPO_PLANTILLA()
            {
                CMP_CODIGO = int.Parse(((DropDownList)this.grvCampo.FooterRow.FindControl("drlCampoPlantillaFooter")).SelectedValue)
            };
            campo.CAMPO_PLANTILLA = campoPlantilla;

            MENSAJE mensaje = new MENSAJE() 
            { 
                MEN_CODIGO=int.Parse(((HiddenField)this.frmMensaje.FindControl("hdnCodigoMensaje")).Value)
            };

            campo.MENSAJE = mensaje;

            EstadoOperacion estado = BusinessLayer.Mensajeria.CampoMensajeBL.insertarCampoPorCampoPlantilla(campo);
            if (estado.Estado)
            {
                this.grvCampo.DataBind();
            }
            else
            {
                this.lblMensaje.Text = estado.Mensaje;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mensajeria/MantenimientoMensaje/ConsultarMensaje.aspx?Codigo=" + Request.QueryString["GrupoMensaje"]);
        }

        protected void dsCampo_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            EstadoOperacion Estado = ((EstadoOperacion)e.ReturnValue);

            if (!Estado.Estado)
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }
    }
}