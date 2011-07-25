using System.ComponentModel;
using System.Configuration.Install;


namespace ServicioComunicacion
{
    [RunInstaller(true)]
    public partial class Instalador : Installer
    {
        public Instalador()
        {
            InitializeComponent();
        }
    }
}
