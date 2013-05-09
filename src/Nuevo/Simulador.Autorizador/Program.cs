namespace Simulador.Autorizador
{
    using Swich.Main.Socket;

    class Program
    {
        static void Main(string[] args)
        {
            var socketHandler = new SocketListener(12000, (handler, content) =>
                {
                    //var frame = content.Length.ToString().PadLeft(4, '0');
                    //content = frame + content;
                    var clientKey = content.ToString().Substring(0, 36);
                    if (content.Contains("x"))
                        SocketSender.Send(handler, "0054" + clientKey + "SELECTORxxRESPONSE");
                    else
                        SocketSender.Send(handler, "0055" + clientKey + "SELECTORyyyRESPONSE");
                });
            socketHandler.StartListening();
        }
    }
}
