using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLab5
{
    class DbcontextSt:DbContext
    {
            public DbSet <Students> Students { get; set; }
            public DbSet <Group> Groups { get; set; }
    }
}
