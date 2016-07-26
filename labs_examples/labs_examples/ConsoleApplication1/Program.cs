using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Lab.SQLOperations();
            //            Connected();
        }

        static void Connected()
        {
            int d = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["Nordwind"].ConnectionString;
            string query = "SELECT * FROM Students";
            string queryInsert = "INSERT INTO (1,'Nemo','Group1','21','3,4') Students";

            SqlConnection connection = new SqlConnection(connectionString);
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                // d=command.ExecuteNonQuery();
                {
                    while (reader.Read())
                    {
                        int categoryId = (int)reader[0];
                        string categoryName = reader[1].ToString();
                        string description = reader[2].ToString();
                        Console.WriteLine("{0}, {1}, {2}", categoryId, categoryName, description);
                    }
                }
                // int categoriesCount = (int)command.ExecuteScalar();
                // for string query = "SELECT Count(Students) FROM Categories";
            }
            Console.ReadLine();
        }

    }
}
