﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button6AppearingFindOption() //
        {
            if (!textBox3.Text.Replace(" ", string.Empty).Equals("") || !textBox3.Text.Replace(" ", string.Empty).Equals("") || !textBox4.Text.Replace(" ", string.Empty).Equals(""))
                button2.Enabled = true;
            if (textBox3.Text.Replace(" ", string.Empty).Equals("") && textBox3.Text.Replace(" ", string.Empty).Equals("") && textBox4.Text.Replace(" ", string.Empty).Equals(""))
                button2.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SQLOperations.AddInintial();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}