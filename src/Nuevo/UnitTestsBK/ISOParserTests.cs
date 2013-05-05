namespace UnitTests
{
    using System;

    using BusinessEntity;

    using Moq;

    using NUnit.Framework;

    using Switch;

    using UnitTests.Builders;

    [TestFixture]
    public class SwitchTests
    {
        private static ISOParser CreateParser(IByteConverter byteConverter = null)
        {
            ISOParser isoParser = new ISOParser(byteConverter ?? new Mock<IByteConverter>().Object);
            return isoParser;
        }

        [Test]
        public void RetornaMensajeSinCamposCuandoNoHayDatos()
        {
            var isoParser = CreateParser();
            byte[] data = new byte[] { };

            ISOMensaje dataIso = isoParser.Parse(null, data);

            Assert.AreEqual(0, dataIso.Campos.Count);
        }

        [Test]
        public void RetornaMensajeConUnCampo()
        {
            ISOParser isoParser = CreateParser();
            byte[] data = new byte[] { 1 };
            var grupoMensaje = new GrupoMensajeBuilder()
                                .WithCampos(new[] { new Campo { LongitudCuerpo = 1 } }).Build();

            ISOMensaje dataIso = isoParser.Parse(grupoMensaje, data);

            Assert.AreEqual(dataIso.Campos[0], new byte[] { 1 });
        }

        [Test]
        public void RetornaMensajeConDosCampos()
        {
            ISOParser isoParser = CreateParser();
            byte[] data = new byte[] { 1, 2, 3 };
            var grupoMensaje = new GrupoMensajeBuilder()
                    .WithCampos(new[] { new Campo { LongitudCuerpo = 1 }, new Campo { LongitudCuerpo = 2, } }).Build();

            ISOMensaje dataIso = isoParser.Parse(grupoMensaje, data);

            Assert.AreEqual(dataIso.Campos[0], new byte[] { 1 });
            Assert.AreEqual(dataIso.Campos[1], new byte[] { 2, 3 });
        }

        [Test]
        public void RetornaMensajeConCamposVariables()
        {
            ISOParser isoParser = CreateParser();
            byte[] data = new byte[] { 1, 2, 3, 4 };
            var grupoMensaje = new GrupoMensajeBuilder()
                .WithCampos(new[] { new Campo { LongitudCuerpo = 1 }, new Campo { LongitudCabecera = 1, LongitudCuerpo = 2 } }).Build();

            ISOMensaje dataIso = isoParser.Parse(grupoMensaje, data);

            Assert.AreEqual(dataIso.Campos[0], new byte[] { 1 });
            Assert.AreEqual(dataIso.Campos[1], new byte[] { 2, 3, 4 });
        }

        [Test]
        public void RetornaMensajeConCamposCabecera()
        {
            var byteConverter = new Mock<IByteConverter>();
            byteConverter.Setup(x => x.Convert(new byte[] { 1 })).Returns("abc");
            ISOParser isoParser = CreateParser(byteConverter.Object);
            byte[] data = new byte[] { 1, 2, 3, 4, 5 };
            var grupoMensaje = new GrupoMensajeBuilder()
                .WithCamposMaestro(new[] { new CampoMaestro { Selector = true, LongitudCuerpo = 1 }, new CampoMaestro { LongitudCuerpo = 1 } })
                .WithCampos(new[] { new Campo { LongitudCuerpo = 1 }, new Campo { LongitudCuerpo = 2 } })
                .ReturnMensajePorDefectoConSelectores(new[] { "abc" })
                .Build();

            ISOMensaje dataIso = isoParser.Parse(grupoMensaje, data);

            Assert.AreEqual(dataIso.Campos[0], new byte[] { 1 });
            Assert.AreEqual(dataIso.Campos[1], new byte[] { 2 });
            Assert.AreEqual(dataIso.Campos[2], new byte[] { 3 });
            Assert.AreEqual(dataIso.Campos[3], new byte[] { 4, 5 });
        }

        [Test]
        public void LanzaExceptionSiNoEncuentraMensajePorSelectores() {
            var byteConverter = new Mock<IByteConverter>();
            byteConverter.Setup(x => x.Convert(It.IsAny<byte[]>())).Returns("cba");
            ISOParser isoParser = CreateParser(byteConverter.Object);
            byte[] data = new byte[] { 1, 2, 3, 4, 5 };
            var grupoMensaje = new GrupoMensajeBuilder()
                .WithCamposMaestro(new[] { new CampoMaestro { Selector = true, LongitudCuerpo = 1 }})
                .ReturnMensajePorDefectoConSelectores(new[] { "abc" })
                .Build();

            Assert.Throws<Exception>(() => isoParser.Parse(grupoMensaje, data));
        }

    }
}
