namespace Swich.Main.Contracts.Fakes
{
    using System.Collections.Generic;

    using Swich.Main.Core;

    public class DataAccessFake : IDataAccess
    {
        public GrupoMensaje GrupoMensajePorEntidad(int entidadId)
        {
            var grupo1 = new GrupoMensaje
                {
                    TipoMensaje = TipoMensaje.ISO8583,
                    ValoresSelectores = new List<ValorSelector>
                        {
                            new ValorSelector
                                {
                                    Posicion = 0, Longitud = 8, Valor = "SELECTOR", Mensaje = new Mensaje
                                        {
                                            Transaccionales = new List<MensajeTransaccional>
                                                {
                                                    new MensajeTransaccional
                                                        {
                                                            Id = 1, CampoId = 1, Valor = "TRANSACCIONAL"
                                                        }
                                                }
                                        }
                                }
                        }
                };
            var grupo2 = new GrupoMensaje
            {
                TipoMensaje = TipoMensaje.XML,
                ValoresSelectores = new List<ValorSelector>
                        {
                            new ValorSelector
                                {
                                    Posicion = 0, Longitud = 8, Valor = "SELECTOR", Mensaje = new Mensaje
                                        {
                                            Transaccionales = new List<MensajeTransaccional>
                                                {
                                                    new MensajeTransaccional
                                                        {
                                                            Id = 1, CampoId = 1, Valor = "TRANSACCIONAL"
                                                        }
                                                }
                                        }
                                }
                        }
            };
            if (entidadId == 1) return grupo1;
            return grupo2;
        }
    }
}