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
            if (e.Exception == null)
            {
                Response.Redirect("~/Comunicacion/MantenimientoProtocolo/ConsultarProtocolo.aspx");
            }
        }

        protected void dsProtocolo_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            Protocolo protocolo = (Protocolo)e.ReturnValue;
            this.tipoProtocolo = protocolo.TipoComunicacion.Id;

            if (!Page.IsPostBack)
            {
                if (protocolo.TipoComunicacion.Id == TipoComunicacionBL.obtenerCodigoComponente())
                {
                    this.componente = protocolo.Componente;
                }

                if (protocolo.TipoComunicacion.Id == TipoComunicacionBL.obtenerCodigoTCP())
                {
                    this.frame = protocolo.Frame.Value;
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
