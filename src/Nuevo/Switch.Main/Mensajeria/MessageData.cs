namespace Swich.Main.Mensajeria
{
    using System.Collections.Generic;

    using Swich.Main.Core;

    public class MessageData
    {
        public TipoMensaje Tipo { get; set; }

        public int MensajeTransaccionalId { get; set; }

        public string RawData { get; set; }

        public List<FieldData> Fields { get; set; }
    }
}