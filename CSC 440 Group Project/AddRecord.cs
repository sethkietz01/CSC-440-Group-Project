﻿using System;
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
    public partial class Add_Record : Form
    {
        public Add_Record()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(StudentIDIn.Text) || string.IsNullOrEmpty(CoursePrefixIn.Text) || string.IsNullOrEmpty(CourseNumIn.Text)
                || string.IsNullOrEmpty(GradeIn.Text) || string.IsNullOrEmpty(YearIn.Text) || string.IsNullOrEmpty(SemesterIn.Text))
            {
                MessageBox.Show("One or more fields were left empty.", "Add Record Failed", MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Record successfully added.", "Add Record Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
