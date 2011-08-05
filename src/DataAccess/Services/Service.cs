namespace DataAccess.Services
{
    using System.Collections.Generic;

    using DataAccess;
    using DataAccess.Aspects;
    using DataAccess.Persistence;
    using StructureMap;
    using BusinessEntity;

    public class Service<T> where T : Entity
    {
        protected readonly DatabaseContext context;

        protected DataAccess<T> dataAccess;

        public Service()
        {
            context = ObjectFactory.GetInstance<DatabaseContext>();
            dataAccess = new DataAccess<T>(context);
        }


        public List<T> ObtenerTodos()
        {
            var obtenerTodos = dataAccess.All();
            return obtenerTodos;
        }

        public virtual T Obtener(int id)
        {
            return dataAccess.Get(id);
        }

        [Transaction]
        public virtual void Insertar(T entity)
        {
            dataAccess.Add(entity);
        }

        [Transaction]
        public virtual void Modificar(T entity)
        {
            dataAccess.Update(entity);
        }

        [Transaction]
        public virtual void Eliminar(T oldEntity)
        {
            T entity = dataAccess.Get(oldEntity.Id);
            dataAccess.Remove(entity);
        }
    }
}