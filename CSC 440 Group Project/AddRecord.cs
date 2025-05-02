using MySql.Data.MySqlClient;
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
    public partial class Add_Record : Form
    {
        public Add_Record()
        {
            InitializeComponent();
        }

        private void submitAddButton_Click(object sender, EventArgs e)
        {
            var (isValid, errorMessage) = Helper.validateModifyRecordForms(StudentIDIn.Text, CoursePrefixIn.Text, CourseNumIn.Text, GradeIn.Text, YearIn.Text, SemesterIn.Text);

            if (!isValid) // If the form is not valid
                MessageBox.Show(errorMessage, "Add Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DatabaseHandler dbHandler = new DatabaseHandler();

                dbHandler.addRecord(StudentIDIn.Text, CoursePrefixIn.Text, CourseNumIn.Text, GradeIn.Text, YearIn.Text, SemesterIn.Text);

                this.Close();
            }
        }

        private void cancelAddButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
