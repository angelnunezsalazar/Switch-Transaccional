namespace UnitTests.Builders
{
    using System.Collections.Generic;

    using BusinessEntity;

    using Moq;

    public class GrupoMensajeBuilder
    {
        private IList<Campo> campos=new List<Campo>();
        private IList<CampoMaestro> camposMaestro = new List<CampoMaestro>();
        private IList<string> valoresSelector;

        public GrupoMensajeBuilder WithCampos(IList<Campo> campos)
        {
            this.campos = campos;
            return this;
        }

        public GrupoMensajeBuilder WithCamposMaestro(IList<CampoMaestro> camposMaestro)
        {
            this.camposMaestro = camposMaestro;
            return this;
        }

        public GrupoMensajeBuilder ReturnMensajePorDefectoConSelectores(IList<string> valoresSelector)
        {
            this.valoresSelector = valoresSelector;
            return this;
        }

        public GrupoMensaje Build()
        {
            var grupoMensaje = new Mock<GrupoMensaje>();
            grupoMensaje.SetupProperty(x => x.CamposMaestro);
            grupoMensaje.Object.CamposMaestro = camposMaestro;

            var mensaje = new Mensaje { Campos = campos };
            if (valoresSelector == null)
                grupoMensaje.Setup(x => x.MensajePorSelector(It.IsAny<List<string>>())).Returns(mensaje);
            else
                grupoMensaje.Setup(x => x.MensajePorSelector(valoresSelector)).Returns(mensaje);

            return grupoMensaje.Object;
        }
    }
}