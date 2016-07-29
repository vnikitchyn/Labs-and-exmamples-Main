using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// [id],[Name],[Group],[Number],[AvgGrade]
namespace lab4
{
   [Table("Students")]
  public  class Students
    {
        [Key]
        public int id { get; set;}
        public string Name { get; set; }
        public string Group { get; set; }
        public int Number { get; set; }
        public string AvgGrade { get; set; }
    }
}
