using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLayer.Operacion;

namespace UserInterface.Operacion.MantenimientoTanqueo
{
    public partial class ConsultarTanqueo : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void grvCampo_DataBound(object sender, EventArgs e)
        {
            if (grvCampo.Rows.Count > 0)
                this.btnAceptar.Visible = true;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            IList<int> idCampoTanqueo = new List<int>();
            IList<int> idCampoDestanqueo = new List<int>();

            foreach (GridViewRow fila in this.grvCampo.Rows)
            {
                CheckBox tanqueo = (CheckBox)fila.FindControl("chkTanqueo");

                if (tanqueo.Checked)
                    idCampoTanqueo.Add(Convert.ToInt32(grvCampo.DataKeys[fila.RowIndex]["CAM_CODIGO"]));

                CheckBox destanqueo = (CheckBox)fila.FindControl("chkDestanqueo");
                
                if (destanqueo.Checked)
                    idCampoDestanqueo.Add(Convert.ToInt32(grvCampo.DataKeys[fila.RowIndex]["CAM_CODIGO"]));
            }

            if (idCampoTanqueo.Count>0 || idCampoDestanqueo.Count>0)
            {
                EstadoOperacion estado = TanqueoBL.modificarCamposTranqueo(int.Parse(drlMensaje.SelectedValue), idCampoTanqueo, idCampoDestanqueo);
                if (estado.Estado)
                    lblMensaje.Text = "Los datos han sido guardados correctamente";
                else
                    lblMensaje.Text = estado.Mensaje;
            }
        }

        protected void grvCampo_DataBinding(object sender, EventArgs e)
        {
            this.btnAceptar.Visible = false;
            lblMensaje.Text = "";
        }
    }
}
