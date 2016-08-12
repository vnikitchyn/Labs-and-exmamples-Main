using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labweb
{
    [Table("Groups")]
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
       
        public virtual List <Students> Students {get; set;}

        public Group() { }
        public Group (string name)
        {
            Name = name;
        }
    }
}