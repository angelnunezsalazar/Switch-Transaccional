using System;
using System.Windows.Forms;
using PocketPC.Mensajeria;
using PocketPC.Servicios;

namespace PocketPC.Transferencias
{
    public partial class CuentaTerceros : Form
    {
        public CuentaTerceros()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Transferencia mensaje = new Transferencia();
            mensaje.AgregarCampo(4, Mensaje.AmountToInt(this.txtImporte.Text),true);
            mensaje.AgregarCampo(49, this.cmbMoneda.SelectedValue.ToString(),false);
            mensaje.AgregarCampo(61, this.cmbCuentaCargo.SelectedValue.ToString(),false);
            mensaje.AgregarCampo(62, this.cmbTipoCuenta.SelectedValue.ToString() + this.txtCuentaAbono.Text,false);

            try
            {
               TransferenciaService.EnviarTransferencia(mensaje);
               ConfirmacionCuentaTerceros confirmacion = new ConfirmacionCuentaTerceros();
               confirmacion.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("No se puede conectar con el servidor");
            }
        }

        public void CargarMonedas()
        {
            this.cmbMoneda.DataSource = Moneda.Monedas();
            this.cmbMoneda.DisplayMember = "Descripcion";
            this.cmbMoneda.ValueMember = "Codigo";
        }

        public void CargarTipoCuenta()
        {
            this.cmbTipoCuenta.DataSource = TipoCuenta.TipoCuentas();
            this.cmbTipoCuenta.DisplayMember = "Descripcion";
            this.cmbTipoCuenta.ValueMember = "Codigo";
        }

        private void CargarCuentas()
        {
            ConsultaCuentaService.SolicitarConsulta();
        }

        private void CuentaTerceros_Load(object sender, EventArgs e)
        {
            CargarMonedas();
            CargarTipoCuenta();
            CargarCuentas();
        }
    }
}