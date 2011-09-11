using System;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using Mensajeria.Mensajes;
using Switch.Dinamica;

namespace Switch.Criptografia
{
    public class CriptografiaMensaje : Paso
    {
        DINAMICA_CRIPTOGRAFIA entidad;

        EnumTipoCriptografia tipoCriptografia;

        public CriptografiaMensaje(DINAMICA_CRIPTOGRAFIA entidad, PASO_DINAMICA pasoDinamica)
            : base(pasoDinamica)
        {
            this.entidad = entidad;

            this.tipoCriptografia = (EnumTipoCriptografia)
            Enum.ToObject(typeof(EnumTipoCriptografia), entidad.DNC_TIPO);
        }


        public override EnumResultadoPaso EjecutarPaso(Mensaje mensajeOrigen)
        {
            foreach (CRIPTOGRAFIA_CAMPO entidadCriptografiaCampo in this.entidad.CRIPTOGRAFIA_CAMPO)
            {
                CriptografiaCampo criptografiaCampo = new CriptografiaCampo(mensajeOrigen, entidadCriptografiaCampo);
                Campo campo = mensajeOrigen.Campo(entidadCriptografiaCampo.CAMPO_RESULTADO);
                campo.ValorCuerpo = new Caracter(ValorPorTipoCriptografia(criptografiaCampo, campo));
            }

            return EnumResultadoPaso.Correcto;
        }

        public string ValorPorTipoCriptografia(CriptografiaCampo criptografiaCampo, Campo campo)
        {
            switch (tipoCriptografia)
            {
                case EnumTipoCriptografia.Encriptacion:
                    return criptografiaCampo.Encriptar(campo.ValorCuerpo);
                case EnumTipoCriptografia.Desencriptacion:
                    return criptografiaCampo.Desencriptar(campo.ValorCuerpo);
                default:
                    throw new Exception("Error: CriptografiaMensaje - ValorPorTipoCriptografia");
            }
        }
    }
}
