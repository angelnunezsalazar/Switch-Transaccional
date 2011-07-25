using System.Threading;
using Switch.DA;
using Utilidades;

namespace Switch
{
    class Program
    {
        static void Main()
        {
            new Switch(new FactoryDA(), new DllDinamica());
            Thread.CurrentThread.Join();
        }
    }
}
