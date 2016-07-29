using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrExample.Models;

namespace EntityFrExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var database = new Database()) {
                
                var sstud = new Student { Id = 1, Name = "Garry" };
                database.Students.Add(sstud);
                database.SaveChanges();
                var students = database.Students.ToList();
                foreach (var student in students) {
                    if (student != null) {
                        Console.WriteLine(student.Name);
                    }
                }
            }
        }
    }
}
