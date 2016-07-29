using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    internal static class SQLOperations
    {
        internal static void QueryAll(string path, string firstLetter)
        {
            using (var db = new DbcontextSt())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Students));
                Stream fileStream1 = new FileStream(path, FileMode.Create);

                // "Native SQL query" - left as example for me
                //var allStudents = db.Students.SqlQuery("SELECT * FROM Students");
                //foreach (var student in allStudents)
                //{
                //    Console.WriteLine("id:{0}\tname:{1}\tnumber:{2}\tgroup:{3}",student.id,student.Name,student.Number,student.Group);
                //    serializer.WriteObject(fileStream1, student);
                //}


                Console.WriteLine("You are reading via Linq query Students");
                List <Students> allLinqStudents = db.Students.ToList<Students>();

                var allLinqStudents2 = from lstud in allLinqStudents
                                     where lstud.Name.StartsWith(firstLetter)
                                    group lstud by lstud.Group into lstud2
                                       orderby lstud2.Key
                                           select lstud2;

                foreach (Students student in allLinqStudents2)
                {
                    Console.WriteLine("id:{0}\tname:{1}\tnumber:{2}\tgroup:{3}", student.id, student.Name, student.Number, student.Group);
                    serializer.WriteObject(fileStream1, student);
                }
                fileStream1.Close();
            }
        }

        internal static void AddInintial()
        {
            using (var db = new DbcontextSt())
            {
                var student1 = new Students() { Name = "Bill Gates", Group = "MS", Number = 14456,AvgGrade="absolute"};
                var student2 = new Students() { Name = "Alicia Nogates", Group = "MS", Number = 55454, AvgGrade = "average" };
                var student3 = new Students() { Name = "Alex Wozniak", Group = "Apple", Number = 4444, AvgGrade = "absolute" };
                var student4 = new Students() { Name = "Anton Nebeda", Group = "MS", Number = 44, AvgGrade = "advance" };
                var student5 = new Students() { Name = "Ashot Obeda", Group = "Apple", Number = 144556, AvgGrade = "intermediate" };
                var student6 = new Students() { Name = "Nemo Beda", Group = "MS", Number = 545745, AvgGrade = "beginner" };
//              db.Students.AddRange(new Students [] { student1, student2, student3, student4, student5, student6 });
                db.Students.Add(student1 );
                db.SaveChanges();
            }
        }

        internal static void Alt()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["NORTHWIND"].ConnectionString;
            var connectionString2 = "Data source=USER-PC\\SQLEXP2014; Initial Catalog = NORTHWIND; Integrated Security = SSPI";
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Categories; SELECT * FROM Products", connectionString2);
            System.Data.DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                int categoryId = (int)row[0];
                string categoryName = row[1].ToString();
                string description = row[2].ToString();
                Console.WriteLine("{0}, {1}, {2}", categoryId, categoryName, description);
            }
        }


        internal static void Remove(int num)
        {
            using (var db = new DbcontextSt())
            {
                List<Students> allDbStudents = db.Students.ToList<Students>();
                var filteredStuds = from st in allDbStudents
                                    where st.Number == num
                                    select st;
                foreach (Students st in filteredStuds)
                {
                    db.Students.Remove(st);
                }
                db.SaveChanges();
            }
        }



        internal static void Add(string name, string group, int num, string grade)
        {
            using (var db = new DbcontextSt())
            {
                var student = new Students() { Name = name, Group = group, Number = num, AvgGrade = grade };
                db.Students.Add(student);
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
                        Console.WriteLine("Adding new studik Nick from Google");
                        Add("Nick","Google",123423,"pre-absolute");
                        db.SaveChanges();

                        Console.WriteLine("Update existing stud");
                        var subjectToUpdate = db.Students.Where(s => s.Name.Contains("Beda")).FirstOrDefault<Students>();
                        db.SaveChanges();
                        dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();
                        Console.WriteLine("transact roolled out. Except: "+ex);
                    }
                }
            }
        }
    }
}