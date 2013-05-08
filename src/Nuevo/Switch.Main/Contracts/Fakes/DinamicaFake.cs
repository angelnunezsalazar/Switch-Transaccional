namespace Swich.Main.Contracts.Fakes
{
    using System;
    using Swich.Main.Mensajeria;
    using Swich.Main.Queue;

    public class DinamicaFake:IDinamica
    {
        public void Ejecutar(MessageData messageData)
        {
            var messageQueued = new MessageQueued
            {
                ClientKey = messageData.ClientKey,
                EntidadId = messageData.EntidadId,
                RawData = messageData.RawData
            };

            if (messageData.RawData.Contains("REQUEST"))
            {
                Console.WriteLine("Processing request message. Key: {0}", messageData.ClientKey);
                QueueSender.Send(messageData.ClientKey, messageQueued, QueueConstants.BANKAUTHORIZER_QUEQUE);
            }
            if (messageData.RawData.Contains("RESPONSE"))
            {
                Console.WriteLine("Processing response message. Key: {0}", messageData.ClientKey);
                QueueSender.Send(messageData.ClientKey, messageQueued, QueueConstants.CELLPHONE_QUEQUE);
            }
        }
    }
}