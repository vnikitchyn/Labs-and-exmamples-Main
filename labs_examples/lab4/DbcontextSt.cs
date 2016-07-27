using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class DbcontextSt:DbContext
    {
            public DbSet <Students> Students { get; set; }  
    }
}
