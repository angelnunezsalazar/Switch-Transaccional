using System.ServiceProcess;

namespace ServicioComunicacion
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new Servicio() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
