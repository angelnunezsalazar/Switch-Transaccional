namespace Swich.Main.Contracts
{
    using Swich.Main.Mensajeria;

    public interface IDinamica
    {
        MessageData Ejecutar(MessageData messageData);
    }
}