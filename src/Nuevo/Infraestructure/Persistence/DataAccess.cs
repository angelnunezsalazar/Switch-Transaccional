namespace Infraestructure.Persistence
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;

    public interface IDataAccess<T>
        where T : class
    {
        T Get(int id);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);
    }

    public class DataAccess<T> : IDataAccess<T>
        where T : class
    {
        private IDbSet<T> set;

        private DatabaseContext context;

        public DataAccess(DatabaseContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public List<T> All()
        {
            return set.AsNoTracking().ToList();
        }

        public T Get(int id)
        {
            return set.Find(id);
        }

        public void Add(T entity)
        {
            set.Add(entity);
        }

        public void Update(T entity)
        {
            set.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            set.Remove(entity);
        }
    }
}