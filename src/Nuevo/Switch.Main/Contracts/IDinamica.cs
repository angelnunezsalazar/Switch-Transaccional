namespace Swich.Main.Contracts
{
    using Swich.Main.Mensajeria;

    public interface IDinamica
    {
        void Ejecutar(MessageData messageData);
    }
}