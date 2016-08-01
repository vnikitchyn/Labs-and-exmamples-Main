using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static lab4.SQLOperations;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Wellcome();
        }

        static void Switch(string input)
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

        static void Wellcome()
        {
            string wellcome = "";
            string caption = "lab info";
            var choice1 = MessageBox.Show(wellcome + "\nPlease choose application format: window-style(YES) or console(NO)", caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (choice1 == DialogResult.No)
            {
                Console.Title = "lab 4 + form";
                Console.WindowWidth = 100;
                string input = Console.ReadLine();
                Switch(input);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);               
                Application.Run(new FromL1());
            }
        }

    }
}