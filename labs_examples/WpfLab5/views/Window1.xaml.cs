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
using System.Windows.Shapes;
using static WpfLab5.SQLOperations;
using static WpfLab5.Helper;

namespace WpfLab5
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            comboBox.ItemsSource = (QueryAllGroupNames());
            SaveButtonAppearingFindOption();
        }

        private void UndoButton_Copy_Click(object sender, RoutedEventArgs e)
        {


        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            //Window mainW = new MainWindow();           
            //mainW.Activate();
            App.Current.MainWindow.Activate();
            App.Current.MainWindow.BeginInit();

            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Budget b;
            double gradek;
            double.TryParse(GradeTextBox.Text, out gradek);
            if (checkBudget.IsChecked==false)
                b = Budget.no;
            else b = Budget.yes;
            AddStudent(Name_TextBox.Text, Surname_TextBox.Text, comboBox.Text, MaxStudentNumber() + 1, gradek, b);
            InfoText.Text = InfoSQL;
            
            App.Current.MainWindow.Activate();

            this.Close();
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            this.InitializeComponent();
            Name_TextBox.Clear();
            Surname_TextBox.Clear();
            comboBox.Text = null;
            GradeTextBox.Clear();
            checkBudget.IsChecked=false;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveButtonAppearingFindOption();

        }


        private void Surname_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButtonAppearingFindOption();
            Surname_TextBox.Text.StringWithoutNumbers();
        }

        private void Name_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButtonAppearingFindOption();
            Name_TextBox.Text.StringWithoutNumbers();
        }

        private void GradeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButtonAppearingFindOption();
            GradeTextBox.Text=NumbersWithoutString(GradeTextBox.Text);
        }

        private void checkBudget_Checked(object sender, RoutedEventArgs e)
        {
  
        }

        private void SaveButtonAppearingFindOption() //Save button
        {
            if (!Name_TextBox.Text.Replace(" ", string.Empty).Equals("") && !Surname_TextBox.Text.Replace(" ", string.Empty).Equals("") && !GradeTextBox.Text.Replace(" ", string.Empty).Equals("") && !comboBox.Text.Replace(" ", string.Empty).Equals(""))
                SaveButton.IsEnabled = true;
            else
                //if (textBox3.Text.Replace(" ", string.Empty).Equals("") && textBox4.Text.Replace(" ", string.Empty).Equals("") && textBox5.Text.Replace(" ", string.Empty).Equals(""))
                SaveButton.IsEnabled = false;
        }

        private void comboBox_TextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
