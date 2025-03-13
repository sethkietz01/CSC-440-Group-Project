using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC_440_Group_Project
{
    public partial class Print_Report : Form
    {
        public Print_Report()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (! string.IsNullOrEmpty(textBox1.Text))
            {
                DisplayGrades displayGrades = new DisplayGrades();
                displayGrades.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
