using System;
using System.Configuration;
using System.Reflection;

namespace Utilidades
{
    public interface IDllDinamica
    {
        object Ejecutar(string ruta, string clase, string metodo, object[] parametros);
        
    }

    public class DllDinamica : IDllDinamica
    {
        public static Func<string> DirectorioComponentes = () => ConfigurationManager.AppSettings["DirectorioComponentes"];

        public object Ejecutar(string componente, string clase, string metodo, object[] parametros)
        {
            Assembly ensamblado = Assembly.LoadFrom(DirectorioComponentes()+componente);
            Object objeto = ensamblado.CreateInstance(clase, true);
            Type tipo = objeto.GetType();
            MethodInfo informacionMetodo = tipo.GetMethod(metodo);
            return informacionMetodo.Invoke(objeto, parametros);
        }
    }
}
