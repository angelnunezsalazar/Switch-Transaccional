using System;
using System.Web.UI.WebControls;

namespace UserInterface.Terminales.MantenimientoTerminal
{
    public partial class AgregarTerminal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {

        }

        protected void oTerminal_Inserting1(object sender, ObjectDataSourceMethodEventArgs e)
        {
            BusinessEntity.TERMINAL nuevoTerminal = (BusinessEntity.TERMINAL)e.InputParameters[0];
            DropDownList puntoServicioCombo = (DropDownList)this.fvTerminales.FindControl("ddlPtoServicio");
            DropDownList entidadCombo = (DropDownList)this.fvTerminales.FindControl("ddlEntidad");
            DropDownList tipoTerminalCombo = (DropDownList)this.fvTerminales.FindControl("ddlTipoTerminal");

            BusinessEntity.PUNTO_SERVICIO puntoServicio = new BusinessEntity.PUNTO_SERVICIO() { PSR_CODIGO = Int32.Parse(puntoServicioCombo.SelectedValue) };
            BusinessEntity.ENTIDAD_COMUNICACION entidadComunicacion = new BusinessEntity.ENTIDAD_COMUNICACION() { EDC_CODIGO = Int32.Parse(entidadCombo.SelectedValue) };
            BusinessEntity.ESTADO_TERMINAL estadoTerminal = new BusinessEntity.ESTADO_TERMINAL() { EST_CODIGO = 1 };

            nuevoTerminal.PUNTO_SERVICIO = puntoServicio;
            nuevoTerminal.ESTADO_TERMINAL = estadoTerminal;
            nuevoTerminal.ENTIDAD_COMUNICACION = entidadComunicacion;
        }

        protected void oTerminal_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
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
