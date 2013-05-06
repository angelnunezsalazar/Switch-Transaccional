namespace Swich.Main
{
    using System.Collections.Generic;

    using Swich.Main.Contracts;
    using Swich.Main.Core;
    using Swich.Main.Queue;

    public class MessageData
    {
        public TipoMensaje Tipo { get; set; }

        public int MensajeTransaccionalId { get; set; }

        public string RawData { get; set; }

        public List<FieldData> Fields { get; set; }
    }
}