using System.Threading;

namespace Comunicacion
{
    class Program
    {
         static void Main(string[] args)
        {
            Comunicacion comunicacion = new Comunicacion();
            Thread.CurrentThread.Join();
        }
    }
}
