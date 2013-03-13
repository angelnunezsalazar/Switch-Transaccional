using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Messaging;
using ColaMensajes;

namespace ComunicacionAutorizador
{
    class ComunicacionAutorizador
    {
        private const int PUERTO = 444;
        private const string IP = "192.168.3.128";
        const string DESCARTAR = "DESCARTAR";

        public Socket cliente;
        public AsyncCallback llamAsincrona;
        byte[] dataBuffer = new byte[10];
        IAsyncResult resultado;

        MessageListener listenerCola;

        public class SocketPacket
        {
            public Socket thisSocket;
            public byte[] dataBuffer = new byte[1];
        }

        public ComunicacionAutorizador()
        {
            if (Conectar())
            {
                //Leer de la cola
                this.listenerCola = Cola.StartListener("colaautorizador", MessageListenerCola);

                //Esperando respuesta del servidor
                //Enviar();
                EsperarData();
            }
            Desconectar();
        }

        public bool Conectar()
        {
            //Creando el socket
            cliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Endpoint
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(IP), PUERTO);

            cliente.Connect(ipEnd);

            if (cliente.Connected)
            {
                Console.WriteLine("Conectado al servidor");
                return true;
            }
            Console.WriteLine("Error al conectar con el servidor");
            return false;
        }

        public void Desconectar()
        {
            if (cliente != null)
            {
                cliente.Close();
                cliente = null;

                Console.WriteLine("Desconectado");
            }
        }

        public void Enviar(Message msg)
        {
            try
            {
                string mensaje = Encoding.ASCII.GetString((byte[])msg.Body);

                Object objData = "Hola mundo\n";
                byte[] byData = Encoding.ASCII.GetBytes(objData.ToString());
                if (cliente != null)
                {
                    cliente.Send(byData);
                    Console.WriteLine("Data enviada: [" + objData + "]");
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message);
            }	
        }

        public void EsperarData()
        {
            try
            {
                if (llamAsincrona == null)
                {
                    llamAsincrona = new AsyncCallback(AlRecibirData);
                }
                SocketPacket theSocPkt = new SocketPacket {thisSocket = cliente};

                // Inicializa listening de la data ASINCRONA
                resultado = cliente.BeginReceive(theSocPkt.dataBuffer,
                                                 0,
                                                 theSocPkt.dataBuffer.Length,
                                                 SocketFlags.None,
                                                 llamAsincrona,
                                                 theSocPkt);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message);
            }
        }

        public void AlRecibirData(IAsyncResult asyn)
        {
            try
            {
                SocketPacket theSockId = (SocketPacket)asyn.AsyncState;
                int iRx = theSockId.thisSocket.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                Decoder d = Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
                String szData = new String(chars);
                //this.txtMensajes.Text = this.txtMensajes.Text + szData;
                EsperarData();
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: El Socket ha sido cerrado\n");
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message);
            }
        }

        private void MessageListenerCola(object sender, ReceiveCompletedEventArgs e)
        {
            Message Msg = ((MessageQueue)e.AsyncResult.AsyncState).EndReceive(e.AsyncResult);
            ((MessageQueue)e.AsyncResult.AsyncState).BeginReceive(Cola.TimeSpan, e.AsyncResult.AsyncState);
            Enviar(Msg);
        }

    }
}
