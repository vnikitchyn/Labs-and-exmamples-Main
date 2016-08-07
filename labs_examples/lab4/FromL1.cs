using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static lab4.SQLOperations;
using static lab4.Helper;


namespace lab4
{
    public partial class FromL1 : Form
    {        
        public FromL1()
        {
            InitializeComponent();
        }

        private void FromL1_Load(object sender, EventArgs e)
        {
            Loading();
        }

        private void Loading ()
        {
            List<dynamic> studs = QueryAllSt();
            if (!studs.Any())
            {
                AddInintial();
                textBox1.Text = InfoSQL;
            }
            listBox1.Visible = true;
            DeleteAppearing();
            button2.Enabled = true;
            button3.Enabled = true;
            textBox3.ReadOnly = true;
            textBox4.Clear();
            textBox4.ReadOnly = true;
            textBox5.Clear();
            textBox5.ReadOnly = true;
            button6.Enabled = false;
            comboBox1.ResetText();
            comboBox1.Enabled = false;
            checkBox1.Enabled = false;
            checkBox1.CheckState = CheckState.Unchecked;
     //     button4.Enabled = false;

            listBox1.DataSource = studs;
            listBox1.ValueMember = "Name";
            listBox1.SelectedItem = null;
            listBox1.DisplayMember = "FullName";
            listBox1.ClearSelected();
            textBox1.Clear();

            listBox1.SelectedValueChanged += new EventHandler(listBox1_SelectedValueChanged);
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var st = (dynamic) listBox1.SelectedItem;

                DeleteAppearing();

                textBox3.Text = listBox1.SelectedValue.ToString();
                textBox1.Text = listBox1.SelectedItem.ToString();
                textBox4.Text = st.Surname;
                textBox5.Text = Convert.ToString(st.AvgGrade);
                comboBox1.Text = st.Group;
                if ((int) st.Bud == 0)
                    checkBox1.CheckState = CheckState.Checked;
                else
                    checkBox1.CheckState = CheckState.Unchecked;
            }
        }


        private void Button1AppearingFindOption() //Save button
        {
            if (!textBox3.Text.Replace(" ", string.Empty).Equals("") && !textBox4.Text.Replace(" ", string.Empty).Equals("") && !textBox5.Text.Replace(" ", string.Empty).Equals("") && !comboBox1.Text.Replace(" ", string.Empty).Equals("")&&listBox1.SelectedItem==null)
                button1.Enabled = true;
            else 
            //if (textBox3.Text.Replace(" ", string.Empty).Equals("") && textBox4.Text.Replace(" ", string.Empty).Equals("") && textBox5.Text.Replace(" ", string.Empty).Equals(""))
                button1.Enabled = false;
        }

        private void DeleteAppearing ()
        {
            if (listBox1.SelectedItem != null)
            button4.Enabled = true;
            else
            button4.Enabled = false;
        }


        private void textBox3_TextChanged(object sender, EventArgs e) //name
        {
            Button1AppearingFindOption();
            textBox3.Text=StringWithoutNumbers(textBox3.Text);                 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //AddInintial();
            //textBox1.Text = InfoSQL;
            Loading();
            //listBox1.SelectedItem = null;
            //listBox1.ClearSelected();
            textBox3.Text = null;
            textBox1.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.Text = null;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Button1AppearingFindOption();
            textBox4.Text = StringWithoutNumbers(textBox4.Text);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            Button1AppearingFindOption();
            textBox5.Text = NumbersWithoutString(textBox5.Text);
        }

        private void button1_Click(object sender, EventArgs e) //save
        {
            Budget b;
            double gradek;
            double.TryParse(textBox5.Text, out gradek);
            if (checkBox1.CheckState == CheckState.Unchecked)
                b = Budget.no;
            else b = Budget.yes;
            AddStudent(textBox3.Text,textBox4.Text,comboBox1.Text,MaxStudentNumber()+1,gradek,b);
            textBox1.Text = InfoSQL;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button1AppearingFindOption();
        }

        private void button2_Click(object sender, EventArgs e) //create
        {        
            listBox1.ClearSelected();
            listBox1.DataSource = null;
            listBox1.Visible = false;
            textBox3.Clear();
            textBox3.ReadOnly = false;
            textBox1.Clear();
            textBox4.Clear();
            textBox4.ReadOnly = false;
            textBox5.Clear();
            textBox5.ReadOnly = false;
            comboBox1.ResetText();
            comboBox1.Items.Clear();
            comboBox1.Enabled = true;
            comboBox1.Items.AddRange(QueryAllGroupNames().ToArray());                
            checkBox1.Enabled = true;
            checkBox1.CheckState = CheckState.Unchecked;
            button4.Enabled = false;
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e) //delete
        {
            var st = (dynamic)listBox1.SelectedItem;
            DialogResult deleteIt = MessageBox.Show("You are going to delete the record, please confirm this action","Lab4 app",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (deleteIt == DialogResult.Yes)
            {
                RemoveStudent(st.Number);
                textBox1.Text = InfoSQL;
            }
            else
            {
                Loading();
                textBox1.Text = InfoSQL;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.ResetText();
            comboBox1.Items.Clear();
            comboBox1.Enabled = true;
            comboBox1.Items.AddRange(QueryAllGroupNames().ToArray());
            button6.Enabled = true;
            button4.Enabled = false;
            button3.Enabled = false;
            button2.Enabled = false;
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            Button1AppearingFindOption();
            comboBox1.Text = StringWithoutNumbers(comboBox1.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var st = (dynamic)listBox1.SelectedItem;
            UpdateStudent(st.Number,comboBox1.Text);
            Loading();
            textBox1.Text=InfoSQL;
        }
    }
}