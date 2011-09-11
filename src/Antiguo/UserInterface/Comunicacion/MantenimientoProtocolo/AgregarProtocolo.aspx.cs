using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLayer.Comunicacion;

namespace UserInterface.Comunicacion.MantenimientoProtocolo
{
    public partial class AgregarProtocolo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ((HtmlGenericControl)this.frmProtocolo.FindControl("divComponente")).
                    Style.Add("Display", "None");
                ((HtmlGenericControl)this.frmProtocolo.FindControl("divTcp")).
                    Style.Add("Display", "Block");
            }
            this.lblMensaje.Text = "";
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OcultarMostrarDivisionProtocoloSeleccionado();
        }

        private void OcultarMostrarDivisionProtocoloSeleccionado()
        {
            DropDownList drlProtocolo = (DropDownList)this.frmProtocolo.FindControl("drlTipoComunicacion");

            if (drlProtocolo.SelectedValue == TipoComunicacionBL.obtenerCodigoComponente().ToString())
            {
                ((HtmlGenericControl)this.frmProtocolo.FindControl("divComponente")).
                    Style.Add("Display", "Block");
                ((HtmlGenericControl)this.frmProtocolo.FindControl("divTcp")).
                    Style.Add("Display", "None");
            }
            else if (drlProtocolo.SelectedValue == TipoComunicacionBL.obtenerCodigoTCP().ToString())
            {
                ((HtmlGenericControl)this.frmProtocolo.FindControl("divComponente")).
                    Style.Add("Display", "None");
                ((HtmlGenericControl)this.frmProtocolo.FindControl("divTcp")).
                    Style.Add("Display", "Block");
            }
        }

        protected void frmProtocolo_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            if (e.Values["PTR_PUERTO"].ToString() == string.Empty)
            {
                e.Values["PTR_PUERTO"] = null;
            }

            if (e.Values["PTR_TIMEOUT_REQUEST"].ToString() == string.Empty)
            {
                e.Values["PTR_TIMEOUT_REQUEST"] = null;
            }

            if (e.Values["PTR_TIMEOUT_RESPONSE"].ToString() == string.Empty)
            {
                e.Values["PTR_TIMEOUT_RESPONSE"] = null;
            }

            if (e.Values["PTR_FRAME"].ToString() == string.Empty)
            {
                e.Values["PTR_FRAME"] = null;
            }
        }

        protected void dsProtocolo_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                Response.Redirect("~/Comunicacion/MantenimientoProtocolo/ConsultarProtocolo.aspx");
            }
        }
    }
}
