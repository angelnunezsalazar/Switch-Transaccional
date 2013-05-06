namespace Swich.Main.Contracts
{
    using System.Collections.Generic;

    using Swich.Main.Core;

    public interface IParser
    {
        MessageData Parse(string rawData, Mensaje mensaje);

        List<FieldData> Parse1(string rawData, Mensaje mensaje);
    }

    public class FieldData
    {
        public int CampoId { get; set; }

        public string Data { get; set; }
    }
}