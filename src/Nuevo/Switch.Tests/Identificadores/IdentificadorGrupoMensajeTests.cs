namespace Switch.Tests.Identificadores
{
    using System.Collections.Generic;

    using FakeItEasy;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Swich.Main.Contracts;
    using Swich.Main.Core;
    using Swich.Main.Identificadores;
    using Swich.Main.Queue;
    //TODO: refactorizar para que solo ingrese cuál es la entidad y no todo el messagequeued

    [TestClass]
    public class IdentificadorGrupoMensajeTests
    {
        [TestMethod]
        public void IdentificaElGrupoDeMensajeSegunLaEntidad()
        {
            var dataAccess = A.Fake<IDataAccess>();
            A.CallTo(() => dataAccess.GrupoMensajePorEntidad(1))
                .Returns(new GrupoMensaje { TipoMensaje = TipoMensaje.ISO8583 });
            var messageQueued = new MessageQueued { EntidadId = 1 };
            var identificador = new IdentificadorGrupoMensaje(dataAccess);

            var grupoMensaje = identificador.Identificar(messageQueued);

            Assert.AreEqual(TipoMensaje.ISO8583, grupoMensaje.TipoMensaje);
        }
    }
}