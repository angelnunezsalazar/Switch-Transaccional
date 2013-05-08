namespace Switch.Tests.Identificadores
{
    using System.Collections.Generic;

    using FakeItEasy;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Swich.Main.Contracts;
    using Swich.Main.Core;
    using Swich.Main.Identificadores;
    using Swich.Main.Queue;

    [TestClass]
    public class IdentificadorGrupoMensajeTests
    {
        [TestMethod]
        public void IdentificaElGrupoDeMensajeSegunLaEntidad()
        {
            var dataAccess = A.Fake<IDataAccess>();
            A.CallTo(() => dataAccess.GrupoMensajePorEntidad(1))
                .Returns(new GrupoMensaje { TipoMensaje = TipoMensaje.ISO8583 });
            var identificador = new IdentificadorGrupoMensaje(dataAccess);

            var entidadId = 1;
            var grupoMensaje = identificador.Identificar(entidadId);

            Assert.AreEqual(TipoMensaje.ISO8583, grupoMensaje.TipoMensaje);
        }
    }
}