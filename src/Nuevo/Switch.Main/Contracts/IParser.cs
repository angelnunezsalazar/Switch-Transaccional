namespace Swich.Main.Contracts
{
    using Swich.Main.Core;

    public interface IParser
    {
        MessageData Parse(string rawData, Mensaje mensaje);
    }
}