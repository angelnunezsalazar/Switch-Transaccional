namespace Swich.Main.Identificadores
{
    using System.Collections.Generic;
    using System.Linq;

    using Swich.Main.Contracts;
    using Swich.Main.Core;
    using Swich.Main.Exceptions;
    using Swich.Main.Mensajeria;

    public interface IIdentificadorTransaccional
    {
        MensajeTransaccional Identificar(Mensaje mensaje, List<FieldData> fields);
    }

    public class IdentificadorTransaccional : IIdentificadorTransaccional
    {
        public MensajeTransaccional Identificar(Mensaje mensaje, List<FieldData> fields)
        {
            foreach (var transaccional in mensaje.Transaccionales)
            {
                var field = fields.SingleOrDefault(x => x.CampoId == transaccional.CampoId);
                if (field.Data == transaccional.Valor)
                {
                    return transaccional;
                }
            }
            throw new TransaccionalNoIdentificadoException();
        }
    }
}