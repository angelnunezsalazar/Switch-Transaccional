namespace Swich.Main.Mensajeria
{
    using System.Collections.Generic;

    using Swich.Main.Core;

    //TODO: Probar que el clientkey u el entidadID se asignen en el factory
    public class MessageData
    {
        public string ClientKey { get; set; }

        public int EntidadId { get; set; }

        public string RawData { get; set; }

        public List<FieldData> Fields { get; set; }

        public TipoMensaje Tipo { get; set; }

        public int MensajeTransaccionalId { get; set; }
    }
}