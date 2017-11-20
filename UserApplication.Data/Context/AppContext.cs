using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Data.Data;

namespace UserApplication.Data.Context
{
    public class AppContext : DbContext
    {
        static AppContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AppContext>());
        }

        private const string ConnectionName = "UserApplication";

        public AppContext() : base(ConnectionName)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
