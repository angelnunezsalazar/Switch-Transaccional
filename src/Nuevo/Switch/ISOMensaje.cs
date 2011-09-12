namespace Switch
{
    using System.Collections.Generic;

    public class ISOMensaje
    {
        public ISOMensaje()
        {
            Campos = new List<object>();
        }
        public IList<object> Campos { get; set; }
    }
}