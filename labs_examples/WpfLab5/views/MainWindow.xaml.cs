using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static WpfLab5.SQLOperations;
using static WpfLab5.Helper;

namespace WpfLab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Loading();
        }

        private void Loading()
        {
            List<dynamic> studs = QueryAllSt();
            if (!studs.Any())
            {
                AddInintial();
                InfoBox.Text = InfoSQL;
            }
            listBox.ItemsSource = studs;
            listBox.DisplayMemberPath = "FullName";
            listBox.SelectionChanged += new SelectionChangedEventHandler(listBox_SelectionChanged);
            comboBox.ItemsSource=(QueryAllGroupNames());
            Name_TextBox.IsReadOnly = true;
            Surname_TextBox.IsReadOnly = true;
            GradeTextBox.IsReadOnly = true;
            checkBudget.IsEnabled = false;
            comboBox.IsEnabled = false;
            comboBox.IsReadOnly = true;
            SaveButton.IsEnabled = false;
            createButton.IsEnabled = true;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                var st = (dynamic)listBox.SelectedItem;
                InfoBox.Text = string.Format("{0} \ngrade: {1} \ngroup: {2}", st.FullName, st.AvgGrade, st.Group);
                Name_TextBox.Text = st.Name;
                Surname_TextBox.Text = st.Surname;
                GradeTextBox.Text = Convert.ToString(st.AvgGrade);
                comboBox.Text = st.Group;
                DeleteAppearing();
                if ((int)st.Bud == 0)
                    checkBudget.IsChecked = true;
                else
                    checkBudget.IsChecked = false;
            }
        }


        private void InfoBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            Loading();
            InfoBox.Text = null;
            Name_TextBox.Text = string.Empty;
            Surname_TextBox.Text = string.Empty;
            GradeTextBox.Text = string.Empty;
            comboBox.Text = "";
            checkBudget.IsChecked = false;
        }

        private void GradeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButtonAppearingFindOption();
            GradeTextBox.Text = NumbersWithoutString(GradeTextBox.Text);
        }

        private void checkBudget_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }



        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
            win1.Activate();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var st = (dynamic)listBox.SelectedItem;
             MessageBoxResult deleteIt = MessageBox.Show("You are going to delete the record, please confirm this action", "Lab4 app", MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (deleteIt == MessageBoxResult.Yes)
            {
                RemoveStudent(st.Number);
            }
            InfoBox.Text = InfoSQL;
            Loading();
        }

        private void DeleteAppearing()
        {
            if (listBox.SelectedItem != null)
            {
                DeleteButton.IsEnabled = true;
                UpdateButton.IsEnabled = true;
            }
            else
            {
                DeleteButton.IsEnabled = false;
                UpdateButton.IsEnabled = false;
            }
        }

        private void Name_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Surname_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButtonAppearingFindOption();
            Surname_TextBox.Text.StringWithoutNumbers();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = true;
            Name_TextBox.IsReadOnly = false;
            Surname_TextBox.IsReadOnly = false;
            GradeTextBox.IsReadOnly = false;
            checkBudget.IsEnabled = true;
            comboBox.IsEnabled = true;
            comboBox.IsReadOnly = false;
            comboBox.IsEditable = true;
            createButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }


        private void SaveButtonAppearingFindOption() //Save button
        {
            if (!Name_TextBox.Text.Replace(" ", string.Empty).Equals("") && !Surname_TextBox.Text.Replace(" ", string.Empty).Equals("") && !GradeTextBox.Text.Replace(" ", string.Empty).Equals("") && !comboBox.Text.Replace(" ", string.Empty).Equals("")&& Name_TextBox.IsReadOnly == false)
                SaveButton.IsEnabled = true;
            else
                //if (textBox3.Text.Replace(" ", string.Empty).Equals("") && textBox4.Text.Replace(" ", string.Empty).Equals("") && textBox5.Text.Replace(" ", string.Empty).Equals(""))
                SaveButton.IsEnabled = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var stA = (dynamic)listBox.SelectedItem;
            Budget b;
            double gradek;
            double.TryParse(GradeTextBox.Text, out gradek);
            if (checkBudget.IsChecked == false)
                b = Budget.no;
            else b = Budget.yes;

            Students stB = new Students (new Group (comboBox.Text),Name_TextBox.Text, Surname_TextBox.Text, stA.Number, gradek, b);
            UpdateStudentFull(stA.Number, stB, comboBox.Text);
            InfoBox.Text = InfoSQL;
            Loading();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Loading();
        }
    }
}
