using System.Collections.Generic;
using BusinessEntity;
using Excepciones;
using Mensajeria.Mensajes;
using Switch.Comunicacion;

namespace Switch.Dinamica
{
    public enum EnumResultadoDinamica
    {
        DescartarMensaje = 1,
        EsperarRespuesta = 2,
        EnviarRespuesta = 3
    }

    public class DinamicaDeMensaje
    {
        private Dinamica dinamica;
        public Mensaje Mensaje { get; set; }

        public ResultadoDinamica ResultadoDinamica { get; private set; }


        private bool continuar;

        public Nodo NodoActual { get; private set; }

        public DinamicaDeMensaje(Dinamica dinamica, Mensaje mensaje, IEnumerable<PASO_DINAMICA> listaPasos)
        {
            this.continuar = true;
            this.dinamica = dinamica;
            this.Mensaje = mensaje;
            CrearNodos(listaPasos);
        }

        public bool Continuar
        {
            get
            {
                if (continuar)
                {
                    return this.NodoActual == null ? false : true;
                }
                return continuar;
            }
        }

        public void ProcesarResponse()
        {
            continuar = false;
            ProcesarMensaje();

        }

        public void ProcesarRequest()
        {
            ProcesarMensaje();
        }

        private void ProcesarMensaje()
        {
            while (this.Continuar)
            {
                this.EjecutarPaso();
            }
        }

        private void EjecutarPaso()
        {
            switch (NodoActual.Paso.EjecutarPaso(Mensaje))
            {
                case EnumResultadoPaso.Correcto:
                    NodoActual = NodoActual.Izquierda;
                    break;
                case EnumResultadoPaso.Incorrecto:
                    NodoActual = NodoActual.Derecha;
                    break;
                case EnumResultadoPaso.Descartar:
                    ResultadoDinamica = new ResultadoDinamica(EnumResultadoDinamica.DescartarMensaje);
                    continuar = false;
                    break;
                case EnumResultadoPaso.Recibir:
                    NodoActual = NodoActual.Izquierda;
                    break;
                case EnumResultadoPaso.Enviar:
                    if (EsNodoHoja())
                    {
                        ResultadoDinamica = new ResultadoDinamica(EnumResultadoDinamica.EnviarRespuesta);
                    }
                    else
                    {
                        ResultadoDinamica = new ResultadoDinamica(EnumResultadoDinamica.EsperarRespuesta)
                                                {
                                                    EntidadComunicacion = ((EnviarMensaje)NodoActual.Paso).EntidadComunicacion
                                                };

                        NodoActual = NodoActual.Izquierda;

                    }
                    continuar = false;
                    break;
                default:
                    throw new SwitchException("Error: DinamicaDeMensaje - ProcesarPaso: No debe llegar a esta parte del código");
            }
        }

        private bool EsNodoHoja()
        {
            return NodoActual.Izquierda == null && NodoActual.Derecha == null;
        }

        private void CrearNodos(IEnumerable<PASO_DINAMICA> listaPasos)
        {
            Nodo nodo = null;
            foreach (PASO_DINAMICA pasoDinamica in listaPasos)
            {
                Paso pasoComponente = this.dinamica.ObtenerPaso(this, pasoDinamica);

                Nodo nuevoNodo = new Nodo(nodo, pasoComponente);
                if (nodo == null)
                {
                    this.NodoActual = nuevoNodo;
                    nodo = nuevoNodo;
                }
                else
                {
                    while (true)
                    {
                        if (nodo.Izquierda == null)
                        {
                            nodo.Izquierda = nuevoNodo;
                            //if (pasoDinamica.PDT_FIN)
                            //{
                            //    nodo = nodo.Padre;
                            //}
                            //else
                            if (!pasoDinamica.PDT_FIN)
                            {
                                nodo = nodo.Izquierda;
                            }
                            break;
                        }
                        if (nodo.Derecha == null)
                        {
                            nodo.Derecha = nuevoNodo;
                            nodo = nodo.Padre;
                            break;
                        }
                    }
                }
            }
        }

        public class Nodo
        {
            public Nodo Padre { get; set; }
            public Nodo Izquierda { get; set; }
            public Nodo Derecha { get; set; }

            internal Paso Paso;

            public int NumeroHijos()
            {
                if (Izquierda == null && Derecha == null)
                    return 0;
                if (Izquierda != null && Derecha != null)
                    return 0;
                return 1;
            }

            public Nodo(Nodo padre, Paso paso)
            {
                this.Padre = padre;
                this.Paso = paso;
            }
        }
    }
}


