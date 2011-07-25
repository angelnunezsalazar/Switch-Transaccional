using System;
using System.Web.UI.WebControls;

namespace UserInterface.Terminales.MantenimientoTerminal
{
    public partial class ModificarTerminal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void oTerminal_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            BusinessEntity.TERMINAL miTerminal = (BusinessEntity.TERMINAL)e.InputParameters[0];

            DropDownList puntoServicioCombo = (DropDownList)this.fvTerminal.FindControl("ddlPtoServicio");
            DropDownList entidadCombo = (DropDownList)this.fvTerminal.FindControl("ddlEntidad");
            DropDownList estadoTerminalCombo = (DropDownList)this.fvTerminal.FindControl("ddlEstadoTerminal");

            BusinessEntity.PUNTO_SERVICIO puntoServicio = new BusinessEntity.PUNTO_SERVICIO() { PSR_CODIGO = Int32.Parse(puntoServicioCombo.SelectedValue) };
            BusinessEntity.ENTIDAD_COMUNICACION entidadComunicacion = new BusinessEntity.ENTIDAD_COMUNICACION() { EDC_CODIGO = Int32.Parse(entidadCombo.SelectedValue) };
            BusinessEntity.ESTADO_TERMINAL estadoTerminal = new BusinessEntity.ESTADO_TERMINAL() { EST_CODIGO = Int32.Parse(estadoTerminalCombo.SelectedValue) };

            miTerminal.PUNTO_SERVICIO = puntoServicio;
            miTerminal.ESTADO_TERMINAL = estadoTerminal;
            miTerminal.ENTIDAD_COMUNICACION = entidadComunicacion;

            miTerminal.TRM_CODIGO = int.Parse(Request.QueryString["Codigo"]);
        }

        protected void oTerminal_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BusinessEntity.EstadoOperacion Estado = ((BusinessEntity.EstadoOperacion)e.ReturnValue);
            if (Estado.Estado)
            {
                Response.Redirect("~/Terminales/MantenimientoTerminal/ConsultarTerminal.aspx");
            }
            else
            {
                this.lblMensaje.Text = Estado.Mensaje;
            }
        }
    }
}
