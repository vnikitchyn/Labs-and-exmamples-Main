using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
//
//
//
namespace ConsoleApplication2
{
    [Serializable]
    [XmlRoot]
    public class Student
    {
        //[NonSerialized] NB: to mark as non serialize
        [XmlElement] //NB: it is always by default for each of properties/fields
 //     [XmlAttribute] //NB: it is for attributes

        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Course { get; set; }
        public string [] Themes {get;set;}

      public  Student (int studentId, string name, string group, string course, string [] t){
            StudentId = studentId;
            Name = name;
            Group = group;
            Course = course;
            Themes = t;
            }

        public Student()
        {
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string themes = string.Join("\n", Themes);
            sb.Append("sID: " + StudentId).Append("\tName: " + Name).Append("\tGroup: " + Group).Append("\tCourse: " + Course).
                Append("\nThemes: \n" + themes);
            return sb.ToString();
        }
    }
}
