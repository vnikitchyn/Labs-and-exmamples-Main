using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static labweb.Students;

namespace labweb
{
    internal static class SQLOperations
    {
        public static string InfoSQL { get; set; }
        internal static void QueryStbyFisrtLetter(string firstLetter)
        {
            using (var db = new DbcontextSt())
            {
                //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Students));
                //Stream fileStream1 = new FileStream(path, FileMode.Create);

                // "Native SQL query" - left as example for me
                //var allStudents = db.Students.SqlQuery("SELECT * FROM Students");
                //foreach (var student in allStudents)
                //{
                //    Console.WriteLine("id:{0}\tname:{1}\tnumber:{2}\tgroup:{3}",student.id,student.Name,student.Number,student.Group);
                //    serializer.WriteObject(fileStream1, student);
                //}

                Console.WriteLine("You are reading via Linq query Students");
                List<Students> allLinqStudents = db.Students.ToList<Students>();

                var allLinqStudents2 = from lstud in allLinqStudents
                                       where lstud.Name.StartsWith(firstLetter)
                                       group lstud by lstud.Group into lstud2
                                       orderby lstud2.Key
                                       select lstud2;

                foreach (Students student in allLinqStudents2)
                {
                    InfoSQL = string.Format("id:{0}\tname:{1}\tnumber:{2}\tgroup:{3}", student.Group.ToString(), student.Name, student.Number, student.Group);
                    
            //        serializer.WriteObject(fileStream1, student);
                }
                //fileStream1.Close();
            }
        }

        internal static List<dynamic> QueryAllSt()
        {
            using (var db = new DbcontextSt())
            {
                List<Students> allS = db.Students.ToList<Students>();
                List<Group> allG = db.Groups.ToList<Group>();

                IEnumerable<dynamic> allSG = from gr in allG
                            join st in allS
                            on gr.Id equals st.GroupID
                            select new { Name = st.Name, Surname = st.Surname, st.Number, AvgGrade = st.AvgGrade, Bud = st.budgetStatus,Group = gr.Name, FullName = string.Format("{0} {1}",  st.Name,st.Surname)};
                return allSG.ToList();
            }           
        }

        internal static List<string> QueryAllGroupNames()
        {
            using (var db = new DbcontextSt())
            {
                List<Group> allG = db.Groups.ToList<Group>();

                IEnumerable<string> allSG = from  gr in allG
                                             select  gr.Name;          
                return allSG.ToList();
            }
        }


        internal static void AddInintial()
        {
            using (var db = new DbcontextSt())
            {
                var group1 = new Group("MS");
                var group2 = new Group("Apple");
                var group3 = new Group("Lego");

                var s1 = new Students(group1, "Bill", "Gates", 1, 5.0, Budget.yes);
                var s2 = new Students(group2, "Alicia", "Nogates", 2, 3.4, Budget.yes);
                var s3 = new Students(group2, "Andy", "Wozniak", 42, 5.0, Budget.yes);
                var s4 = new Students(group3, "Anton", "Nebeda", 334, 4.4, Budget.yes);
                var s5 = new Students(group3, "Ashot", "Obeda", 12, 4.3, Budget.no);
                var s6 = new Students(group1, "Nemo", "Beda", 984, 3.4, Budget.no);
                Students [] stA = new[] { s1,s2,s3,s4,s5,s6 };
                AddStudents(stA);

                db.SaveChanges();
            }
        }

        internal static void RemoveStudent(int num)
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
                    InfoSQL = string.Format("Student {0} is removed from db (groupID={1})", st.ToString(), st.GroupID);
                }
                db.SaveChanges();
            }
        }

        internal static void RemoveGroup(string groupname)
        {
            using (var db = new DbcontextSt())
            {
                List<Group> allDbGroups = db.Groups.ToList<Group>();
                var filteredGroups = from gr in allDbGroups
                                    where gr.Name == groupname
                                    select gr;
                foreach (Group gr in filteredGroups)
                {
                    db.Groups.Remove(gr);
                }
                db.SaveChanges();
            }
        }

        internal static void AddStudents(Students [] studs)
        {
            StringBuilder sb = new StringBuilder();
            using (var db = new DbcontextSt())
            {
                foreach (Students stud in studs)
                {
                    int groupID = FindGroupId(stud.Group.Name);
                    if (groupID > 0)
                    {
                        stud.Group=null;
                        stud.GroupID = groupID;
                        sb.Append(string.Format("Student {0} added to db (groupID={1})", stud.ToString(), groupID));
                    }
                    db.Students.Add(stud);
                }

                InfoSQL = string.Format("Bulk addition is completed. Below values are added to db:\n{0}", sb.ToString());
                db.SaveChanges();
            }
       }

        internal static void AddStudents(Students studs)
        {
            AddStudents(new Students[] { studs} );
        }


        internal static void AddStudent(string name, string surname, string groupname, int num, double grade, Budget b)
        {
            using (var db = new DbcontextSt())
            {
                Students student = null;
                int groupID = FindGroupId(groupname);
                if (groupID > 0)
                {
                    student = new Students(null, name, surname, num, grade, b, groupID);
                }
                else
                {
                    student = new Students(new Group(groupname), name, surname, num, grade, b);
                }
                db.Students.Add(student);
                InfoSQL = string.Format("Student {0} added to db (groupID={1})", student.ToString(), groupID);
                db.SaveChanges();
            }
        }

        internal static Students FindStudent(int num)
        {
            using (var db = new DbcontextSt())
            {
                List<Students> allDbStudents = db.Students.ToList<Students>();
                var filteredStuds = from st in allDbStudents
                                    where st.Number == num
                                    select st;
              
                return filteredStuds.First();
                    }
        }

        internal static void UpdateStudent(int num, string groupname)
        {
            using (var db = new DbcontextSt())
            {
                Students studentA=FindStudent(num);
                Students studentB = null;
                int groupID = FindGroupId(groupname);

                if (groupID > 0)
                {
                    studentB = new Students(null, studentA.Name, studentA.Surname, studentA.Number, studentA.AvgGrade, studentA.budgetStatus, groupID);
                }

                else
                {
                    studentB = new Students(new Group(groupname), studentA.Name, studentA.Surname, studentA.Number, studentA.AvgGrade, studentA.budgetStatus);
                }
                studentA = studentB;
                studentB = null;
                db.SaveChanges();
                InfoSQL = string.Format("Updated {0}, new group: {1}", studentA, groupname);
            }
        }

        internal static void UpdateStudentFull(int numA, Students stB, string groupname)
        {
            using (var db = new DbcontextSt())
            {
                Students studentA = FindStudent(numA);
                Students studentB = stB;
                int groupID = FindGroupId(groupname);

                if (groupID > 0)
                {
                    studentB = new Students(null, stB.Name, stB.Surname, stB.Number, stB.AvgGrade, stB.budgetStatus, groupID);
                }

                else
                {
                    studentB = new Students(new Group(groupname), stB.Name, stB.Surname, stB.Number, stB.AvgGrade, stB.budgetStatus);
                }
                //studentA = studentB;
                RemoveStudent(numA);
                db.Students.Add(studentB);
                db.SaveChanges();
                InfoSQL = string.Format("Updated {0}, new group: {1}", studentA, groupname);
            }
        }


        internal static void UpdateStudent2(int num, string groupname)
        {

            using (var db = new DbcontextSt())
            {
                Students studentA = FindStudent(num);
                Students studentB = null;

                int groupID = FindGroupId(groupname);

                if (groupID > 0)
                {
                    studentB = new Students(null, studentA.Name, studentA.Surname, studentA.Number, studentA.AvgGrade, studentA.budgetStatus, groupID);
                }

                else
                {
                    studentB = new Students(new Group(groupname), studentA.Name, studentA.Surname, studentA.Number, studentA.AvgGrade, studentA.budgetStatus);
                }
                RemoveStudent(num);
                db.Students.Add(studentB);               
                db.SaveChanges();
                InfoSQL = string.Format("Updated {0}", studentB);
            }
        }
   
        internal static void AddGroup(string groupname)
        {
            bool find=false;
            using (var db = new DbcontextSt())
            {
                var groupsAll = db.Groups.ToList();
                foreach(Group g in groupsAll)
                {
                    if (groupname.Equals(g.Name))
                    {
                        find = true;
                    }              
                }
                if (!find)
                {
                    Group group = new Group(groupname);
                    db.Groups.Add(group);
                    db.SaveChanges();
                    InfoSQL = string.Format("Group name '{0}' (id:{1}) is added to db", group.Name, group.Id);
                }
                else
                InfoSQL = "Such name is already presented in db, operation is refused";
            }
        }


        internal static int FindGroupId(string groupname)
        {
            using (var db = new DbcontextSt())
            {
                int id=0;
                var groupsAll = db.Groups.ToList();
                if (groupsAll.Any())
                {
                    var groupNameL = from gr in groupsAll
                                     where gr.Name == groupname
                                     select gr;
                    //foreach (Group group in groupNameL)
                    //{
                    //    id = group.Id;
                    //}
                    if (groupNameL.FirstOrDefault()!= null)
                    id = groupNameL.FirstOrDefault().Id;
                }
                else InfoSQL = "Such group id is not found, returned '0'";                    
                return id;
            }
        } 


        internal static int MaxStudentNumber ()
        {
            using (var db = new DbcontextSt())
            {
                int id = 0;
                var sts = db.Students.ToList();
                if (sts.Any())
                {
                    var stsl = from st in sts  
                               orderby  st.Number descending                               
                                     select st;

                    id = stsl.FirstOrDefault().Number;
                    InfoSQL = string.Format("student with max number '{0}': {1}",id,stsl.FirstOrDefault().ToStringNames());
                }
                else
                {
                    InfoSQL = "No one students here, Number is set as 1";
                    id = 1;
                }

                return id;
            }
        }






        //internal static void Transact()
        //{
        //    using (var db = new DbcontextSt())
        //    {
        //        using (var dbTransaction = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var Students = db.Students.ToList<Students>();
        //                Console.WriteLine("Adding new studik Nick from Google");
        //                Add("Nick","Eh","Apple", 233454, 4.9, 0);
        //                db.SaveChanges();

        //                Console.WriteLine("Update existing stud");
        //                var subjectToUpdate = db.Students.Where(s => s.Name.Contains("Beda")).FirstOrDefault<Students>();
        //                db.SaveChanges();
        //                dbTransaction.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                dbTransaction.Rollback();
        //                Console.WriteLine("transact roolled out. Except: "+ex);
        //            }
        //        }
        //    }
        //}


        //internal static void Alt()
        //{
        //    //var connectionString = ConfigurationManager.ConnectionStrings["NORTHWIND"].ConnectionString;
        //    var connectionString2 = "Data source=USER-PC\\SQLEXP2014; Initial Catalog = NORTHWIND; Integrated Security = SSPI";
        //    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Categories; SELECT * FROM Products", connectionString2);
        //    System.Data.DataSet dataSet = new DataSet();
        //    adapter.Fill(dataSet);
        //    foreach (DataRow row in dataSet.Tables[0].Rows)
        //    {
        //        int categoryId = (int)row[0];
        //        string categoryName = row[1].ToString();
        //        string description = row[2].ToString();
        //        Console.WriteLine("{0}, {1}, {2}", categoryId, categoryName, description);
        //    }
        //}

    }
}