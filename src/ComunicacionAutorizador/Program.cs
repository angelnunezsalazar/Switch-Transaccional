using System.Threading;

namespace ComunicacionAutorizador
{
    class Program
    {
        static void Main(string[] args)
        {
            ComunicacionAutorizador comunicacion = new ComunicacionAutorizador();
            Thread.CurrentThread.Join();
        }
    }
}
