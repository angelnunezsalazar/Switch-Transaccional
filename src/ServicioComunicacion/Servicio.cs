using System.ServiceProcess;

namespace ServicioComunicacion
{
    public partial class Servicio : ServiceBase
    {
        public Servicio()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {
        }
    }
}
