using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;

namespace UserInterface.Mensajeria.MantenimientoCampoPlantilla
{
    public partial class ConsultarCampoPlantilla : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.drlGrupoMensaje.SelectedValue == "-1")
                this.btnAgregar.Visible = false;
            else
                this.btnAgregar.Visible = true;

            this.lblMensaje.Text = "";
        }

        protected void dsCampoPlantilla_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (this.drlGrupoMensaje.SelectedValue == "-1")
            {
                e.Cancel = true;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mensajeria/MantenimientoCampoPlantilla/AgregarCampoPlantilla.aspx?CodigoGrupoMensaje=" + this.drlGrupoMensaje.SelectedValue);
        }

        protected void dsCampoPlantilla_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BusinessEntity.EstadoOperacion Estado = ((BusinessEntity.EstadoOperacion)e.ReturnValue);

            if (!Estado.Estado)
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void drlGrupoMensaje_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["GrupoMensaje"] = this.drlGrupoMensaje.SelectedIndex;
        }

        protected void dsGrupoMensaje_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            List<GRUPO_MENSAJE> ListaGrupoMensaje = (List<GRUPO_MENSAJE>)e.ReturnValue;

            Dictionary<int, int> ListaGrupoTipoMensaje = new Dictionary<int, int>();

            foreach (GRUPO_MENSAJE GrupoMensaje in ListaGrupoMensaje)
            {
                ListaGrupoTipoMensaje.Add(GrupoMensaje.GMJ_CODIGO, GrupoMensaje.TIPO_MENSAJE.TMJ_CODIGO);
            }

            ViewState["GrupoConTipoMensaje"] = ListaGrupoTipoMensaje;
        }

        protected void grdCampoPlantilla_DataBound(object sender, EventArgs e)
        {
            Dictionary<int, int> ListaGrupoTipoMensaje = (Dictionary<int, int>)ViewState["GrupoConTipoMensaje"];

            if (this.drlGrupoMensaje.SelectedValue != "-1")
            {
                if (ListaGrupoTipoMensaje != null)
                {
                    if (ListaGrupoTipoMensaje.Count > 0)
                    {
                        int CodigoGrupoMensaje = int.Parse(this.drlGrupoMensaje.SelectedValue);
                        int CodigoTipoMenaje = ListaGrupoTipoMensaje.Where(o => o.Key == CodigoGrupoMensaje).First().Value;
                        if (CodigoTipoMenaje == BusinessLayer.Mensajeria.TipoMensajeBL.obtenerCodigoXML())
                        {
                            this.grdCampoPlantilla.Columns[2].Visible = false;
                            this.grdCampoPlantilla.Columns[3].Visible = false;
                        }
                        else
                        {
                            this.grdCampoPlantilla.Columns[2].Visible = true;
                            this.grdCampoPlantilla.Columns[3].Visible = true;
                        }
                    }
                }
            }

        }

        protected void drlGrupoMensaje_DataBound(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String UrlPrevia = Request.ServerVariables["HTTP_REFERER"];
                if (Session["GrupoMensaje"] != null)
                {
                    if (UrlPrevia.Contains("AgregarCampoPlantilla") || UrlPrevia.Contains("ModificarCampoPlantilla"))
                    {
                        this.drlGrupoMensaje.SelectedIndex = -1;
                        int Seleccionado = int.Parse(Session["GrupoMensaje"].ToString());
                        this.drlGrupoMensaje.SelectedIndex = Seleccionado;
                    }
                    else
                    {
                        Session.Remove("GrupoMensaje");
                    }
                }
            }

            if (this.drlGrupoMensaje.SelectedValue == "-1")
                this.btnAgregar.Visible = false;
            else
                this.btnAgregar.Visible = true;
        }
    }
}
