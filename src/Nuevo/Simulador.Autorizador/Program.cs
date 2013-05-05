namespace Simulador.Autorizador
{
    using Swich.Main.Socket;

    class Program
    {
        static void Main(string[] args)
        {
            var socketHandler = new SocketListener(12000, (handler, content) =>
                {
                    var frame = content.Length.ToString().PadLeft(4, '0');
                    content = frame + content;
                    SocketSender.Send(handler, content);
                });
            socketHandler.StartListening();
        }
    }
}
