﻿using System;
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
        public int Id { get; set;}
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Number { get; set; }
        public double AvgGrade { get; set; }
        public enum Budget { yes, no }
        public Budget budgetStatus;

        public int GroupID { get; set; }
        [ForeignKey ("GroupID")]
        public Group Group { get; set; }

        public Students() { }

        public Students(Group group,  string name, string surname , int number, double avgGrade, Budget budg) {
            Group = group;
            Name = name;
            Surname = surname;
            Number = number;
            AvgGrade = avgGrade;
            budgetStatus = budg;
            GroupID = Group.Id;
        }

        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name +" ").Append(Surname+"\t").Append("Number "+Number).Append("Grade: "+AvgGrade).Append("\ton budget: "+ budgetStatus).Append("\tGroup: "+ Group.Name);
            return sb.ToString();
        }
    }
}
