namespace Swich.Main.Identificadores
{
    using System.Collections.Generic;
    using System.Linq;

    using Swich.Main.Contracts;
    using Swich.Main.Core;

    public class IdentificadorTransaccional
    {
        public MensajeTransaccional IdentificarTransaccional(Mensaje mensaje, List<FieldData> fields)
        {
            MensajeTransaccional transaccionalEncontrado = new MensajeTransaccional();
            foreach (var transaccional in mensaje.Transaccionales)
            {
                var field = fields.SingleOrDefault(x => x.CampoId == transaccional.CampoId);
                if (field.Data == transaccional.Valor)
                {
                    transaccionalEncontrado = transaccional;
                    break;
                }
            }
            return transaccionalEncontrado;
        }
    }
}