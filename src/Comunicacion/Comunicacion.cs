using System;
using System.Messaging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ColaMensajes;

namespace Comunicacion
{
    public class Comunicacion
    {
        const int MAX_CLIENTS = 10;
        const string DESCARTAR = "DESCARTAR";
        private const int PUERTO = 333;

        public AsyncCallback pfnWorkerCallBack;
        private Socket socketPrincipal;
        private Socket[] workerSocket = new Socket[MAX_CLIENTS];
        private int contadorClientes = 0;

        private string cabecera = "";
        private string mensaje = "";
        private byte[] bmensaje;
        private int contadorBytes = 0;
        private int longitudMensaje;

        private int supercontador = 0;

        MessageListener listenerCola;

        public class SocketPacket
        {
            public System.Net.Sockets.Socket m_currentSocket;
            public byte[] dataBuffer = new byte[1];
        }


        public Comunicacion()
        {
            this.listenerCola = Cola.StartListener("ColaCelular", new ReceiveCompletedEventHandler(MessageListenerCola));
            IniciarListening();
        }

        private void MessageListenerCola(object sender, ReceiveCompletedEventArgs e)
        {
            Message Msg = ((MessageQueue)e.AsyncResult.AsyncState).EndReceive(e.AsyncResult);
            IAsyncResult AsyncResult = ((MessageQueue)e.AsyncResult.AsyncState).BeginReceive(Cola.TimeSpan, ((MessageQueue)e.AsyncResult.AsyncState));
            ProcesarMensajeCola(Msg);
        }

        private void ProcesarMensajeCola(Message msg)
        {
            try
            {
                string mensaje = Encoding.ASCII.GetString((byte[]) msg.Body);

                if (mensaje != DESCARTAR)
                {
                    //Devolver el mensaje al dispositivo
                    String trama = "";
                    trama += mensaje.Length.ToString("0000");
                    trama += mensaje;

                    //Enviar mensaje
                    Enviar(trama);
                    Console.WriteLine("No descartó");
                }
                else
                {
                    //Acción a realizar cuando se descarta el mensaje en la dinámica

                    workerSocket[contadorClientes-1].Close();
                    workerSocket[contadorClientes-1]= null;
                    //DetenerListening();
                    Console.WriteLine("Descartó, cerró comunicación");
                }
            }
            catch (Exception)
            {
                
            }
        }


        void Enviar(string mensaje)
        {
            //Enviar data a todos los clientes
            try
            {
                Object objData = mensaje;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString());
                for (int i = 0; i < contadorClientes; i++)
                {
                    if (workerSocket[i] != null)
                    {
                        if (workerSocket[i].Connected)
                        {
                            workerSocket[i].Send(byData);
                        }
                    }
                }
            }
            catch (SocketException)
            {
                
            }
        }


        void IniciarListening()
        {
            int puerto = PUERTO;

            // Creando LISTENING del socket
            socketPrincipal = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Stream,
                                         ProtocolType.Tcp);
            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, puerto);

            // Asociando la LOCAL IP Address
            socketPrincipal.Bind(ipLocal);

            // Iniciar listening
            socketPrincipal.Listen(MAX_CLIENTS);

            // Comienza la conexión asíncrona, activando el evento AlConectarseCliente
            socketPrincipal.BeginAccept(new AsyncCallback(AlConectarseCliente), null);
        }

        void DetenerListening()
        {
            if (socketPrincipal != null)
            {
                socketPrincipal.Close();
            }
            for (int i = 0; i < contadorClientes; i++)
            {
                if (workerSocket[i] != null)
                {
                    workerSocket[i].Close();
                    workerSocket[i] = null;
                }
            }
        }

        public void AlConectarseCliente(IAsyncResult asyn)
        {
            try
            {
                workerSocket[contadorClientes] = socketPrincipal.EndAccept(asyn);
                EsperarData(workerSocket[contadorClientes]);
                // Incrementa el total de clientes
                contadorClientes++;

                Console.WriteLine("Ciente " + contadorClientes + " conectado!");

                socketPrincipal.BeginAccept(new AsyncCallback(AlConectarseCliente), null);
            }
            catch (ObjectDisposedException)
            {
            }
            catch (SocketException)
            {
            }
        }

        public void EsperarData(System.Net.Sockets.Socket soc)
        {
            try
            {
                if (pfnWorkerCallBack == null)
                {
                    // Specify the call back function which is to be 
                    // invoked when there is any write activity by the 
                    // connected client
                    pfnWorkerCallBack = new AsyncCallback(AlRecibirData);
                }
                SocketPacket theSocPkt = new SocketPacket();
                theSocPkt.m_currentSocket = soc;

                //Comienza a recibir la data escrita por el cliente, usando el metodo Asincrono
                soc.BeginReceive(theSocPkt.dataBuffer, 0,
                                   theSocPkt.dataBuffer.Length,
                                   SocketFlags.None,
                                   pfnWorkerCallBack,
                                   theSocPkt);
            }
            catch (SocketException)
            {
            }
        }

        public void AlRecibirData(IAsyncResult asyn)
        {
            try
            {
                SocketPacket socketData = (SocketPacket)asyn.AsyncState;

                int iRx = 0;
                // Complete the BeginReceive() asynchronous call by EndReceive() method
                // which will return the number of characters written to the stream 
                // by the client
                iRx = socketData.m_currentSocket.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                System.Text.Decoder d = System.Text.Encoding.ASCII.GetDecoder();
                int charLen = d.GetChars(socketData.dataBuffer,
                                         0, iRx, chars, 0);
                //String szData = new String(socketData.dataBuffer);
                String szData = new String(chars);
                



                //
                if (contadorBytes < 4)
                {
                    cabecera += szData[0].ToString();
                    contadorBytes++;
                    if (contadorBytes == 4)
                    {
                        int.TryParse(cabecera, out longitudMensaje);
                        bmensaje = new byte[longitudMensaje];
                    }
                }
                else
                {
                    if ((contadorBytes - 4) < longitudMensaje)
                    {
                        mensaje += szData[0].ToString();
                        bmensaje[contadorBytes-4] = socketData.dataBuffer[0];
                        contadorBytes++;
                    }
                    
                    if ((contadorBytes - 4) == longitudMensaje)
                    {
                        Cola.EnviarMensaje(bmensaje, "Colita", Cola.REQUEST_QUEQUE, "ColaCelular");
                        Console.WriteLine(supercontador + ": Escribió en cola request " + contadorClientes);
                        cabecera = "";
                        mensaje = "";
                        contadorBytes = 0;
                        supercontador++;
                    }
                }

                // Continua la espera de Data en el Socket
                EsperarData(socketData.m_currentSocket);
            }
            catch (ObjectDisposedException)
            {
            }
            catch (SocketException)
            {
            }
        }
    }
}
