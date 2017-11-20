using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApplication.Data.Data
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public bool IsNew => Id == 0;
    }
}
