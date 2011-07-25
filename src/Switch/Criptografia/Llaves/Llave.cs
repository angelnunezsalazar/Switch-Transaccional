using System;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using Mensajeria.Mensajes;

namespace Switch.Criptografia.Llaves
{
    public class Llave
    {
        public static Valor ValorLlave(Mensaje mensaje, string llaveFija, CAMPO campo, int tipoLlave)
        {
            EnumTipoLlave tipoTransformacion = (EnumTipoLlave)
                Enum.ToObject(typeof(EnumTipoLlave), tipoLlave);

            switch (tipoTransformacion)
            {
                //TODO: Falta implementar WorkingKey
                case EnumTipoLlave.WorkingKey:
                    throw new NotImplementedException("No funciona WorkingKey");
                case EnumTipoLlave.Campo:
                    return mensaje.Campo(campo).ValorCuerpo;
                case EnumTipoLlave.LlaveFija:
                    return new Caracter(llaveFija);
                default:
                    throw new Exception("Error: Llave - ValorLlave");
            }
        }
    }
}
