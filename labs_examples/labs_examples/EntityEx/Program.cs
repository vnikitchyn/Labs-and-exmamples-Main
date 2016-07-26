using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace EntityEx
{
    public class Program
    {
        static void Main(string[] args)
        {
//          using (var db = new System.Data.Entity.DbContext())
            using (var db = new DbContexts())
            {
                #region Add Data
                //var student = new Student() { Name = "Bill Gates"};

                //var math = new Subject() { Name = "Mathematics" };
                //var physics = new Subject() { Name = "Physics" };

                //student.Subjects.Add(math);
                //student.Subjects.Add(physics);

                //db.Students.Add(student);
                //db.SaveChanges();
                #endregion

                #region Querying
                Console.WriteLine("Native SQL query");
                var allSubjects = db.Subjects.SqlQuery("SELECT * FROM Subjects");
                foreach (var subject in allSubjects)
                {
                    Console.WriteLine(subject.Name);
                }

                Console.WriteLine("Linq query");
                var allLinqSubjects = db.Subjects.ToList<Subject>();
                foreach (var subject in allLinqSubjects)
                {
                    Console.WriteLine(subject.Name);
                }
                #endregion

                #region Transaction
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var subjects = db.Subjects.ToList<Subject>();
                        Console.WriteLine("Add new subject");
                        db.Subjects.Add(new Subject { Name = "History", SubjectId = 3 });
                        db.SaveChanges();

                        Console.WriteLine("Update existing subject");
                        var subjectToUpdate = db.Subjects.Where(s => s.SubjectId == 2).FirstOrDefault<Subject>();
                        db.SaveChanges();
                        dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();
                    }
                }
                #endregion
            }
        }
    }
}