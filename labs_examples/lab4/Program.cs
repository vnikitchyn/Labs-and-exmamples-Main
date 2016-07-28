using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lab4.SQLOperations;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("possible options:n init\nqury\nadd\ndel\ntransact");
            string keyword = Console.ReadLine();
            switch (keyword)
            {
                case ("init"):
                    AddInintial();
                    keyword = Console.ReadLine();
                    break;

                case ("query"):
                    Console.Write("Enetr fist letter of ame");
                    keyword = Console.ReadLine();
                    QueryAll(@"G:\Works\C#\trash\student1ver.json", keyword);
                    keyword = Console.ReadLine();
                    break;

                case ("add"):
                    Add("Name", "goup", 98923, "grade");
                    keyword = Console.ReadLine();
                    break;

                case ("del"):
                    Console.Write("Enter number (not id) of stud to delete");
                    keyword = Console.ReadLine();
                    int num;
                    int.TryParse(keyword, out num);
                    Remove(num);
                    keyword = Console.ReadLine();
                    break;

                case ("transact"):
                    Transact();
                    keyword = Console.ReadLine();
                    break;

                default:
                    Console.Write("u exited from app");
                    break;
            }

        }
    }
}