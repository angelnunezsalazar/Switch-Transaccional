namespace Switch
{
    using System;
    using System.Collections.Generic;

    using BusinessEntity;

    public class ISOParser
    {
        private readonly IByteConverter byteConverter;

        public ISOParser(IByteConverter byteConverter)
        {
            this.byteConverter = byteConverter;
        }

        public ISOMensaje Parse(GrupoMensaje grupoMensaje, byte[] data)
        {
            ISOMensaje isoData = new ISOMensaje();
            if (data.Length > 0)
            {
                int index = 0;
                var valoresSelector = new List<string>();
                foreach (var campoMaestro in grupoMensaje.CamposMaestro)
                {
                    byte[] campoData = new byte[campoMaestro.LongitudTotal];
                    Array.Copy(data, index, campoData, 0, campoMaestro.LongitudTotal);
                    isoData.Campos.Add(campoData);
                    index += campoMaestro.LongitudTotal;

                    if (campoMaestro.Selector) valoresSelector.Add(byteConverter.Convert(campoData));
                }

                var mensaje = grupoMensaje.MensajePorSelector(valoresSelector);
                if (mensaje == null) 
                    throw new Exception("No se encontró ningun mensaje que concuerde con los selectores enviados");

                foreach (var campo in mensaje.Campos)
                {
                    byte[] campoData = new byte[campo.LongitudTotal];
                    Array.Copy(data, index, campoData, 0, campo.LongitudTotal);
                    isoData.Campos.Add(campoData);
                    index += campo.LongitudTotal;
                }
            }
            return isoData;
        }
    }
}