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

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            List<dynamic> studs = QueryAllSt();
            if (!studs.Any())
            {
                AddInintial();
                InfoBox.Text = InfoSQL;
            }
            listBox.ItemsSource = studs;

        }

        private void InfoBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
