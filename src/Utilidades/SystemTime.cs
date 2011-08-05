using System;

namespace Utilidades
{
    public  class SystemTime
    {
        public  Func<DateTime> Now = () => DateTime.Now;
    }
}