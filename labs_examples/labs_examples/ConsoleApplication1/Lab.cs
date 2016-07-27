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

    public static class Lab
    {

       public static void SQLOperations()
        {
            int res = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["Nordwind"].ConnectionString;
            string query = "SELECT * FROM Students";
            string queryInsert = "INSERT INTO Students VALUES (2,'Nemo','Group1','21A','3') ";
            SqlConnection connection = new SqlConnection(connectionString);
            using (SqlCommand command1 = new SqlCommand(queryInsert, connection))
            {
                connection.Open();
                res = command1.ExecuteNonQuery();
                Console.WriteLine("Rows affected: "+res);
            }
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader[0];
                        string sName = reader[1].ToString();
                        string sGroup = reader[2].ToString();
                        string sNumber = reader[3].ToString();
                 Console.WriteLine("{0}, {1}, {2}", sName, sGroup, sNumber);
                    }
                }
            }
            Console.ReadLine();
        }
    public static void Disonnected()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Nordwind"].ConnectionString;
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Students; SELECT * FROM Products", connectionString);
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