namespace Swich.Main.Contracts
{
    using System.Collections.Generic;

    using Swich.Main.Core;
    using Swich.Main.Mensajeria;

    public interface IParser
    {
        List<FieldData> Parse(string rawData, Mensaje mensaje);
    }
}