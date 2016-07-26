using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetExample
{
   public static class Disconnected
    {
      public  static void Disonnected1()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Nordwind"].ConnectionString;

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Categories; SELECT * FROM Products", connectionString);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                int categoryId = (int)row[0];
                string categoryName = row[1].ToString();
                string description = row[2].ToString();
                Console.WriteLine("{0}, {1}, {2}", categoryId, categoryName, description);
            }
            foreach (DataRow row2 in dataSet.Tables[1].Rows)
            {
                int productID = (int)row2[0];
                string productName = row2[1].ToString();
                string something = row2[2].ToString();
                Console.WriteLine("{0}, {1}, {2}", productID, productName, something);
            }
            Console.ReadLine();
        }
    }
}