using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLayer.Comunicacion;

namespace UserInterface.Comunicacion.MantenimientoProtocolo
{
    public partial class ModificarProtocolo : System.Web.UI.Page
    {
        private string componente;
        private int tipoProtocolo;
        private int frame;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void frmProtocolo_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            if (e.NewValues["PTR_PUERTO"].ToString() == string.Empty)
            {
                e.NewValues["PTR_PUERTO"] = null;
            }
            if (e.NewValues["PTR_TIMEOUT_REQUEST"].ToString() == string.Empty)
            {
                e.NewValues["PTR_TIMEOUT_REQUEST"] = null;
            }

            if (e.NewValues["PTR_TIMEOUT_RESPONSE"].ToString() == string.Empty)
            {
                e.NewValues["PTR_TIMEOUT_RESPONSE"] = null;
            }
        }

        protected void dsProtocolo_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BusinessEntity.EstadoOperacion Estado = ((BusinessEntity.EstadoOperacion)e.ReturnValue);

            if (Estado.Estado)
            {
                Response.Redirect("~/Comunicacion/MantenimientoProtocolo/ConsultarProtocolo.aspx");
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }

        protected void dsProtocolo_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            PROTOCOLO protocolo = (PROTOCOLO)e.InputParameters[0];
            DropDownList drlProtocolo = (DropDownList)this.frmProtocolo.FindControl("drlTipoComunicacion");
            

            TIPO_COMUNICACION tipoComunicacion = new TIPO_COMUNICACION() { TPO_CODIGO = int.Parse(drlProtocolo.SelectedValue) };
            protocolo.TIPO_COMUNICACION = tipoComunicacion;

            if (protocolo.TIPO_COMUNICACION.TPO_CODIGO==TipoComunicacionBL.obtenerCodigoComponente())
            {
                DropDownList drlComponente = (DropDownList)this.frmProtocolo.FindControl("drlComponente");
                protocolo.PTR_COMPONENTE = drlComponente.SelectedValue;
            }

            if (protocolo.TIPO_COMUNICACION.TPO_CODIGO == TipoComunicacionBL.obtenerCodigoTCP())
            {
                DropDownList drlFrame = (DropDownList)this.frmProtocolo.FindControl("drlFrame");
                protocolo.PTR_FRAME = int.Parse(drlFrame.SelectedValue);
            }
        }

        protected void dsProtocolo_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            PROTOCOLO protocolo = (PROTOCOLO)e.ReturnValue;
            this.tipoProtocolo = protocolo.TIPO_COMUNICACION.TPO_CODIGO;

            if (!Page.IsPostBack)
            {
                if (protocolo.TIPO_COMUNICACION.TPO_CODIGO == TipoComunicacionBL.obtenerCodigoComponente())
                {
                    this.componente = protocolo.PTR_COMPONENTE;
                }

                if (protocolo.TIPO_COMUNICACION.TPO_CODIGO == TipoComunicacionBL.obtenerCodigoTCP())
                {
                    this.frame = protocolo.PTR_FRAME.Value;
                }
            }
            
        }

        protected void drlComponente_DataBound(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (this.tipoProtocolo == TipoComunicacionBL.obtenerCodigoComponente())
                {
                    ((DropDownList)this.frmProtocolo.FindControl("drlComponente")).
                        Items.FindByValue(this.componente).Selected = true;
                }
            }
        }

        protected void dsFrame_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            SortedList<int, string> returnValue = (SortedList<int, string>)e.ReturnValue;
        }

        protected void drlFrame_DataBound(object sender, EventArgs e)
        {
            if (this.tipoProtocolo == TipoComunicacionBL.obtenerCodigoTCP())
            {
                DropDownList drlFrame = ((DropDownList)this.frmProtocolo.FindControl("drlFrame"));
                ((DropDownList)this.frmProtocolo.FindControl("drlFrame")).
                   Items.FindByValue(this.frame.ToString()).Selected = true;
            }
        }
    }
}
