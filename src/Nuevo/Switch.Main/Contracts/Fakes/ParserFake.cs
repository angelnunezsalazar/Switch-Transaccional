namespace Swich.Main.Contracts.Fakes
{
    using System.Collections.Generic;

    using Swich.Main.Core;
    using Swich.Main.Mensajeria;

    public class ParserFake:IParser
    {
        public List<FieldData> Parse(string rawData, Mensaje mensaje)
        {
            return new List<FieldData>();
        }
    }
}