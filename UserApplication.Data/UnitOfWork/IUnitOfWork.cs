using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApplication.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        void SaveChanges();

        Task SaveChangesAsync();
    }

}
