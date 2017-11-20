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
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(int id, bool asNoTracking = false);

        T Get(Expression<Func<T, bool>> where);

        T[] GetAll();

        T[] GetAll(Expression<Func<T, bool>> where);

        void Edit(T entity);

        void Insert(T item);

        void Remove(T item);

        void Remove(int id);

        DbSet<T> GetSet();
    }
}
