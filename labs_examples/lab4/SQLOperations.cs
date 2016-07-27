using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    internal static class SQLOperations
    {
        internal static void Query()
        {
            using (var db = new DbcontextSt())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Students));
                Stream fileStream1 = new FileStream(@"D:\Vick\CSharp\tempis\student1ver.json", FileMode.Create);

                Console.WriteLine("Native SQL query");
                var allStudents = db.Students.SqlQuery("SELECT * FROM Students");
                foreach (var student in allStudents)
                {
                    Console.WriteLine("id:{0}\tname:{1}\tnumber:{2}\tgroup:{3}",student.id,student.Name,student.Number,student.Group);
                    serializer.WriteObject(fileStream1, student);
                }

                Console.WriteLine("Linq query");
                List <Students> allLinqStudents = db.Students.ToList<Students>();
                    var allLinqStudents2 = from lSt in allLinqStudents
                                           select 1St;

                foreach (var student in allLinqStudents)
                {
                    Console.WriteLine("id:{0}\tname:{1}\tnumber:{2}\tgroup:{3}", student.id, student.Name, student.Number, student.Group);
                }
                fileStream1.Close();
            }
        }

        internal static void Add()
        {
            using (var db = new DbcontextSt())
            {
                var student1 = new Students() { Name = "Bill Gates", Group = "MS", Number = "1",AvgGrade="absolute"};
                db.Students.Add(student1);
                db.SaveChanges();
            }
        }

        internal static void Transact()
        {
            using (var db = new DbcontextSt())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var Students = db.Students.ToList<Students>();
                        Console.WriteLine("Add new subject");
                        db.Students.Add(new Subject { Name = "History", SubjectId = 3 });
                        db.SaveChanges();

                        Console.WriteLine("Update existing subject");
                        var subjectToUpdate = db.Students.Where(s => s.SubjectId == 2).FirstOrDefault<Subject>();
                        db.SaveChanges();
                        dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();
                    }
                }
            }
        }
    }
}