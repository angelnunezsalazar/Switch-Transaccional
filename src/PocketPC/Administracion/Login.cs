using System;
using System.Windows.Forms;
using PocketPC.Mensajeria;
using PocketPC.Servicios;

namespace PocketPC.Administracion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario.IngresarUsuario(this.txtPIN.Text);
                Apertura mensaje = new Apertura();
                AperturaService.RealizarApertura(mensaje);
                MessageBox.Show("Éxitos totales!");
            }
            
            
            catch
            {
                MessageBox.Show("No se puede conectar con el servidor");
            }

        }
    }
}