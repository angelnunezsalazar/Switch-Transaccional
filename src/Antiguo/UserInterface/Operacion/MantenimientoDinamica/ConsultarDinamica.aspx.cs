using System;
using System.Web.UI;
using BusinessEntity;
using BusinessLayer.Operacion;

namespace UserInterface.Operacion.MantenimientoDinamica
{
    public partial class ConsultarDinamica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.lblMensaje.Text = "";
        }

        protected string ObtenerNombreTipoFuncionalidad(string codigoTipoFuncionalidad)
        {
            return TipoFuncionalidadBL.obtenerNombrePorCodigo(int.Parse(codigoTipoFuncionalidad));
        }

        protected void drlMensajeTransaccional_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["codigoGrupo"] = this.drlGrupoMensajeInicio.SelectedValue;
            Session["nombreGrupo"] = this.drlGrupoMensajeInicio.SelectedItem.Text;
            Session["codigoMensaje"] = this.drlMensaje.SelectedValue;
            Session["nombreMensaje"] = this.drlMensaje.SelectedItem.Text;
            Session["codigoMensajeTransaccional"] = this.drlMensajeTransaccional.SelectedValue;
            Session["nombreMensajeTransaccional"] = this.drlMensajeTransaccional.SelectedItem.Text;

            if (this.drlMensajeTransaccional.SelectedIndex != 0)
                this.btnAgregar.Visible = true;
            else
                this.btnAgregar.Visible = false;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Operacion/MantenimientoDinamica/AgregarDinamica.aspx?codigo=" + this.drlMensajeTransaccional.SelectedValue);
        }

        protected void drlMensajeTransaccional_DataBound(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String UrlPrevia = Request.ServerVariables["HTTP_REFERER"];
                if (Session["codigoMensajeTransaccional"] != null)
                {
                    if (UrlPrevia.Contains("AgregarDinamica"))
                    {

                        this.drlMensajeTransaccional.SelectedIndex = -1;
                        int seleccionado = int.Parse(Session["codigoMensajeTransaccional"].ToString());
                        this.drlMensajeTransaccional.SelectedIndex = seleccionado;
                    }
                }
            }

            if (this.drlMensajeTransaccional.SelectedIndex != 0)
                this.btnAgregar.Visible = true;
            else
                this.btnAgregar.Visible = false;
        }

        protected void drlGrupoMensajeInicio_DataBound(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String UrlPrevia = Request.ServerVariables["HTTP_REFERER"];
                if (Session["codigoMensajeTransaccional"] != null)
                {
                    if (UrlPrevia.Contains("AgregarDinamica"))
                    {
                        this.drlGrupoMensajeInicio.SelectedIndex = -1;
                        int seleccionado = int.Parse(Session["codigoGrupo"].ToString());
                        this.drlGrupoMensajeInicio.SelectedIndex = seleccionado;
                    }
                }
            }
        }

        protected void drlMensaje_DataBound(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String UrlPrevia = Request.ServerVariables["HTTP_REFERER"];
                if (Session["codigoMensajeTransaccional"] != null)
                {
                    if (UrlPrevia.Contains("AgregarDinamica"))
                    {

                        this.drlMensaje.SelectedIndex = -1;
                        int seleccionado = int.Parse(Session["codigoMensaje"].ToString());
                        this.drlMensaje.SelectedIndex = seleccionado;

                    }
                }
            }

        }

        protected void grvPasoDinamica_DataBound(object sender, EventArgs e)
        {

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.grvPasoDinamica.Rows.Count > 0)
            {
                int codidigoUltimoPaso = int.Parse(this.grvPasoDinamica.DataKeys[this.grvPasoDinamica.Rows.Count - 1].Value.ToString());

                BusinessEntity.EstadoOperacion Estado = PasoDinamicaBL.eliminarPasoDinamica(new PASO_DINAMICA() { PDT_CODIGO = codidigoUltimoPaso });

                if (!Estado.Estado)
                {
                    this.lblMensaje.Text = Estado.Mensaje;
                }
                else
                {
                    this.grvPasoDinamica.DataBind();
                }
            }
        }
    }
}
