using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrExample.Models
{
    class Database : DbContext
    {
        public DbSet<Student> Students { get; set; }
    }
}
