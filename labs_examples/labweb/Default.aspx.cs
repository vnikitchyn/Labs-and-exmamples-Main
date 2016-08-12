using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace labweb
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<dynamic> GetStudentsAll()
        {
            using (var db = new DbcontextSt())
            {
                List<Students> allS = db.Students.ToList<Students>();
                List<Group> allG = db.Groups.ToList<Group>();

                IEnumerable<dynamic> allSG = from gr in allG
                                             join st in allS
                                             on gr.Id equals st.GroupID
                                             select new { Name = st.Name, Surname = st.Surname, st.Number, AvgGrade = st.AvgGrade, Bud = st.budgetStatus, Group = gr.Name, FullName = string.Format("{0} {1}", st.Name, st.Surname) };
                return allSG;
            }
        }
      public IEnumerable<Students> GetStudents()
        {
            using (var db = new DbcontextSt())
            {
                List<Students> allS = db.Students.ToList<Students>();

                var allSG = from s in allS
                            select s;
                return allS;
            }


        }  
    }
}