﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// [id],[Name],[Group],[Number],[AvgGrade]
namespace labweb
{
    public enum Budget { yes, no }

    [Table("Students")]
  public  class Students
    {
        [Key]
        public int Id { get; set;}
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Number { get; set; }
        public double AvgGrade { get; set; }

        [Column("budgeted")]
        public Budget budgetStatus { get; set; }

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


        public Students(Group group, string name, string surname, int number, double avgGrade, Budget budg, int gropId)
        {
            Group = group;
            Name = name;
            Surname = surname;
            Number = number;
            AvgGrade = avgGrade;
            budgetStatus = budg;
            GroupID = gropId;
        }

        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Group!=null)
            sb.Append(Name +" ").Append(Surname+"\t").Append('№'+ Number).Append("\tGrade: "+AvgGrade).Append("\ton budget: "+ budgetStatus).Append("\tGroup: "+ Group.Name);
            else
            sb.Append(Name + " ").Append(Surname+"\t").Append('№'+ Number).Append("\tGrade: " + AvgGrade).Append("\ton budget: " + budgetStatus);
            return sb.ToString();
        }


         public string ToStringNames()
        {
            StringBuilder sb = new StringBuilder();
                sb.Append(Name + " ").Append(Surname + "\t");
            return sb.ToString();
        }




    }
}
