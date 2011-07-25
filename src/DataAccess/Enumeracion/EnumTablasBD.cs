
namespace DataAccess.Enumeracion.EnumTablasBD
{
    //TODO: Cambiar la Enumeracion por otra cosa que informe mejor del tipo
    public enum EnumFuncionalidadEstandar
    {
        Fecha_DDMMYYYY=1,
        Fecha_HHMM=2,
        Llave_Maestra
    }

    public enum EnumTipoFuncionalidad
    {
        Enviar = 1,
        Recibir=2,
        Validacion=3,
        Transformacion=4,
        Criptografia=5,
        Descartar=6
    }

    public enum EnumTipoDatoCampo
    {
        Alfanumerico = 1,
        Numerico_sin_Punto = 2,
        Numerico_con_Punto = 3,
        BCD = 4,
        Alfabetico = 5,
        Binario = 6
    }

    public enum EnumTipoCabecera
    {

    }

    public enum EnumTipoComunicacion
    {
        TCP = 1,
        Componente = 2
    }

    public enum EnumTipoTransformacion
    {
        ValorConstante = 1,
        ProcedimientoAlmacenado = 2,
        FuncionalidadEstandar = 3,
        Concatenacion = 4,
        Componente = 5
    }

    public enum EnumTipoParametroTransformacionCampo
    {
        Tabla = 5,
        CampoOrigen = 6
    }

    public enum EnumTipoMensaje
    {
        Bitmap = 1,
        XML = 2
    }

    public enum EnumTipoLlave
    {
        WorkingKey = 1,
        Campo = 2,
        LlaveFija = 3
    }

    public enum EnumOperacionLlave
    {
        Concatenacion = 1,
        XOR = 2
    }

    public enum EnumCriterioAplicacion
    {
        Inclusion = 2,
        Procedimiento = 3,
        Componente = 4
    }

    public enum EnumTipoRegla
    {
        Condicion = 1,
        TablaValores = 2
    }

    public enum EnumCondicion
    {
        Igual = 1,
        Diferente = 2,
        Menor = 3,
        MenorIgual = 4,
        Mayor = 5,
        MayorIgual = 6
    }

    public enum EnumAlgoritmo
    {
        DES = 1,
        TDES = 2,
        Rijndael = 3
    }

    public enum EnumTipoCriptografia
    { 
        Encriptacion=1,
        Desencriptacion=2
    }

    public enum EnumFrame
    {
        Delimitadores = 1,
        Cabecera_4_Bytes = 2
    }
}
