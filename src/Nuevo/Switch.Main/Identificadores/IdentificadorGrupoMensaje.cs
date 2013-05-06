namespace Swich.Main.Identificadores
{
    using Swich.Main.Contracts;
    using Swich.Main.Core;
    using Swich.Main.Queue;

    public interface IIdentificadorGrupoMensaje
    {
        GrupoMensaje Identificar(MessageQueued messageQueued);
    }

    public class IdentificadorGrupoMensaje : IIdentificadorGrupoMensaje
    {
        private readonly IDataAccess dataAccess;

        public IdentificadorGrupoMensaje(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public GrupoMensaje Identificar(MessageQueued messageQueued)
        {
            return this.dataAccess.GrupoMensajePorEntidad(messageQueued.EntidadId);
        }
    }
}