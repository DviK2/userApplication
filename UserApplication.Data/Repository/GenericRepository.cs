using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Data.Data;


namespace UserApplication.Data.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext Context;

        protected readonly DbSet<T> Set;

        public GenericRepository(DbContext context)
        {
            Context = context;
            Set = Context.Set<T>();
        }

        public virtual T Get(int id, bool asNoTracking = false)
        {
            return asNoTracking
                ? Set.AsNoTracking().FirstOrDefault(c => c.Id == id)
                : Set.FirstOrDefault(c => c.Id == id);
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return Set.FirstOrDefault(where);
        }

        public virtual T[] GetAll()
        {
            return Set.Where(c => c.Id > 0).ToArray();
        }

        public virtual T[] GetAll(Expression<Func<T, bool>> where)
        {
            return Set.Where(c => c.Id > 0).Where(where).ToArray();
        }

        public virtual void Insert(T item)
        {
            Set.Add(item);
        }

        public virtual void Remove(T item)
        {
            Set.Remove(item);
        }

        public virtual void Remove(int id)
        {
            var entity = Set.First(x => x.Id == id);
            Set.Remove(entity);
        }

        public DbSet<T> GetSet()
        {
            return Set;
        }

        public virtual void Edit(T entity)
        {
            var entry = Context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Modified;
                return;
            }

            var attachedEntity = Set.Local.SingleOrDefault(c => c.Id == entity.Id);

            if (attachedEntity != null)
            {
                var attachedEntry = Context.Entry(attachedEntity);
                attachedEntry.CurrentValues.SetValues(entity);
            }
            else
            {
                // If current entity has attached relationship - attaching an entity will be failed
                entry.State = EntityState.Modified;
            }
        }
    }
}
