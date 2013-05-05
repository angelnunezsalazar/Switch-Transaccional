namespace Swich.Main.Contracts
{
    using Swich.Main.Core;

    public interface IDataAccess
    {
        GrupoMensaje GrupoMensajePorEntidad(int entidadId);
    }
}