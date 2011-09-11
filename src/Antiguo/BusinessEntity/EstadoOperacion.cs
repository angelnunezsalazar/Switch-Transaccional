using System;

namespace BusinessEntity
{
    public class EstadoOperacion
    {
        public EstadoOperacion(bool estado, string mensaje, Exception excepcion)
        {
            this.estado = estado;
            this.mensaje = mensaje;
            this.excepcion = excepcion;
        }

        public EstadoOperacion(bool estado, string mensaje, Exception excepcion,Boolean mensajePersonalizado)
        {
            this.estado = estado;
            this.mensaje = mensaje;
            this.excepcion = excepcion;
            this.mensajePersonalizado = mensajePersonalizado;
        }

        private string mensaje;

        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }
        private bool estado;

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        private Exception excepcion;

        public Exception Excepcion
        {
            get { return excepcion; }
            set { excepcion = value; }
        }

        private Boolean mensajePersonalizado;

        public Boolean MensajePersonalizado
        {
            get { return mensajePersonalizado; }
            set { mensajePersonalizado = value; }
        }
    }
}
