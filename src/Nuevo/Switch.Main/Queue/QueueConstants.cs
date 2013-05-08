namespace Swich.Main.Queue
{
    using System;

    public class QueueConstants
    {
        public static readonly string MAQUINA = @".\";

        public static readonly string PRIVATE_QUEQUE = MAQUINA + @"Private$\";

        public static readonly string REQUEST_QUEQUE = "Request";

        public static readonly string RESPONSE_QUEQUE = "Response";

        public static readonly string CELLPHONE_QUEQUE = "CellPhone";

        public static readonly string BANKAUTHORIZER_QUEQUE = "BankAuthorizer";

        public static readonly Type TIPO_SERIALIZACION = typeof(MessageQueued);
    }
}