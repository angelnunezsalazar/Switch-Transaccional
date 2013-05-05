using NUnit.Framework;

namespace UnitTests
{
    using BusinessEntity;

    [TestFixture]
    public class GrupoTests
    {
        [Test]
        public void RetornaNullSiNoEncuentraUnMensajePorSelector()
        {
            GrupoMensaje grupoMensaje = new GrupoMensaje();
            grupoMensaje.Mensajes = new[] { new Mensaje { Campos = new[] { new Campo { Selector = false } } } };

            var mensaje = grupoMensaje.MensajePorSelector(new[] { "123" });

            Assert.AreEqual(mensaje, null);
        }

        [Test]
        public void RetornaMensajePorSelector()
        {
            GrupoMensaje grupoMensaje = new GrupoMensaje();
            grupoMensaje.Mensajes = new[] { new Mensaje { Campos = new[] { new Campo { Selector = true, SelectorRequest = "123" } } } };

            var mensaje = grupoMensaje.MensajePorSelector(new[] { "123" });

            Assert.AreEqual(mensaje, grupoMensaje.Mensajes[0]);
        }

        [Test]
        public void RetornaMensajePorSelectorCuandoExistenCamposSelectoresYNoSelectores()
        {
            GrupoMensaje grupoMensaje = new GrupoMensaje();
            grupoMensaje.Mensajes = new[] { new Mensaje { Campos = new[]
                {
                    new Campo { Selector = false },
                    new Campo { Selector = true, SelectorRequest = "123" }
                } } };

            var mensaje = grupoMensaje.MensajePorSelector(new[] { "123" });

            Assert.AreEqual(mensaje, grupoMensaje.Mensajes[0]);
        }

        [Test]
        public void RetornaMensajePorSelectorDadosVariosValores()
        {
            GrupoMensaje grupoMensaje = new GrupoMensaje();
            grupoMensaje.Mensajes = new[] { new Mensaje { Campos = new[]
                {
                    new Campo { Selector = true,SelectorRequest = "456"},
                    new Campo { Selector = true, SelectorRequest = "123" }
                } } };

            var mensaje = grupoMensaje.MensajePorSelector(new[] { "123", "456" });

            Assert.AreEqual(mensaje, grupoMensaje.Mensajes[0]);
        }

        [Test]
        public void RetornaNullSinoEncuentraSelectorDadoVariosValores()
        {
            GrupoMensaje grupoMensaje = new GrupoMensaje();
            grupoMensaje.Mensajes = new[] { new Mensaje { Campos = new[]
                {
                    new Campo { Selector = true, SelectorRequest = "123" }
                } } };

            var mensaje = grupoMensaje.MensajePorSelector(new[] { "123", "456" });

            Assert.IsNull(mensaje);
        }

        [Test]
        public void RetornaMensajePorSelectorCuandoElGrupoTieneVariosMensajes()
        {
            GrupoMensaje grupoMensaje = new GrupoMensaje();
            grupoMensaje.Mensajes = new[] { 
                new Mensaje { Campos = new[]
                {
                    new Campo { Selector = true,SelectorRequest = "426"},
                    new Campo { Selector = true, SelectorRequest = "123" }
                } },
                new Mensaje { Campos = new[]
                {
                    new Campo { Selector = true,SelectorRequest = "456"},
                    new Campo { Selector = true, SelectorRequest = "123" }
                } }
            };

            var mensaje = grupoMensaje.MensajePorSelector(new[] { "123", "456" });

            Assert.AreEqual(mensaje, grupoMensaje.Mensajes[1]);
        }
    }
}