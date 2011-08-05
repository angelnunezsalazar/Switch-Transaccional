using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;

namespace Mensajeria.Mensajes
{
    public abstract class Mensaje
    {
        protected Mensaje(EnumTipoMensaje Tipo)
        {
            this.Tipo = Tipo;
            Longitud = 0;
            Campos = new List<Campo>();
        }

        public EnumTipoMensaje Tipo { get; private set; }

        public int Longitud { get; private set; }

        public int NroCampos
        {
            get { return this.Campos.Count; }
        }
        protected List<Campo> Campos { get; set; }

        public Campo this[int posicion]
        {
            get
            {
                return this.Campos[posicion];
            }
        }

        protected abstract void AgregarCuerpoMensaje(CAMPO campoMensaje, Valor valorCabecera, Valor valorCuerpo);
        protected abstract void AgregarCabeceraMensaje(CAMPO_PLANTILLA campoPlantilla, Valor valorCabecera, Valor valorCuerpo);

        public void AgregarCuerpo(CAMPO campoMensaje, Valor valorCabecera, Valor valorCuerpo)
        {
            Longitud += campoMensaje.CAM_LONGITUD + (campoMensaje.CAM_LONGITUD_CABECERA.HasValue ? campoMensaje.CAM_LONGITUD_CABECERA.Value : 0);
            if (ValidarTipoDato(campoMensaje.TIPO_DATO, valorCuerpo))
            {
                AgregarCuerpoMensaje(campoMensaje, valorCabecera, valorCuerpo);
            }
        }

        public void AgregarCabecera(CAMPO_PLANTILLA campoPlantilla, Valor valorCabecera, Valor valorCuerpo)
        {
            Longitud += campoPlantilla.CMP_LONGITUD + (campoPlantilla.CMP_LONGITUD_CABECERA.HasValue ? campoPlantilla.CMP_LONGITUD_CABECERA.Value : 0);
            if (ValidarTipoDato(campoPlantilla.TIPO_DATO, valorCuerpo))
            {
                AgregarCabeceraMensaje(campoPlantilla, valorCabecera, valorCuerpo);
            }
        }

        private bool ValidarTipoDato(TIPO_DATO tipoDato, object trama)
        {
            return true;
        }

        public  Mensaje CrearMensaje(EnumTipoMensaje tipo)
        {
            switch (tipo)
            {
                case EnumTipoMensaje.Bitmap:
                    return new MensajeISO8583(tipo);
                case EnumTipoMensaje.XML:
                    return new MensajeXML(tipo);
            }
            return null;
        }

        public Campo Campo(CAMPO campo)
        {
            int codigo = campo.CAM_CABECERA ? campo.CAMPO_PLANTILLA.CMP_CODIGO : campo.CAM_CODIGO;

            return (from c in Campos
                    where c.Codigo == codigo
                    select c).SingleOrDefault();
        }

        public bool EsMensajeTanqueado(Mensaje mensaje)
        {
            IEnumerable<Campo> camposTanqueo = mensaje.Campos.Where(c => c.EsTanqueo);
            return Campos.Where(campo => campo.EsTanqueo)
                        .All(campo => camposTanqueo.Contains(campo, new CampoPorId()));
        }

        internal class CampoPorId : IEqualityComparer<Campo>
        {
            public bool Equals(Campo x, Campo y)
            {
                return x.Codigo == y.Codigo;
            }

            public int GetHashCode(Campo obj)
            {
                return base.GetHashCode();
            }
        }
    }
}
