namespace Infraestructure.Services
{
    using System.Collections.Generic;

    using BusinessEntity;

    using Infraestructure.Persistence;

    using StructureMap;

    public class Service<T> where T : Entity
    {
        protected readonly DatabaseContext context;

        protected DataAccess<T> dataAccess;

        public Service()
        {
            context = ObjectFactory.GetInstance<DatabaseContext>();
            dataAccess = new DataAccess<T>(context);
        }

        public virtual List<T> ObtenerTodos()
        {
            var obtenerTodos = dataAccess.All();
            return obtenerTodos;
        }

        public virtual T Obtener(int id)
        {
            return dataAccess.Get(id);
        }

        public virtual void Insertar(T entity)
        {
            dataAccess.Add(entity);
        }

        public virtual void Modificar(T entity)
        {
            dataAccess.Update(entity);
        }

        public virtual void Eliminar(int id)
        {
            T entity = dataAccess.Get(id);
            dataAccess.Remove(entity);
        }
    }
}