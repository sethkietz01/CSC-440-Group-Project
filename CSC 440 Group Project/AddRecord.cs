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
            var (isValid, errorMessage) = Helper.validateModifyRecordForms(StudentIDIn.Text, CoursePrefixIn.Text, CourseNumIn.Text, GradeIn.Text, YearIn.Text, SemesterIn.Text);

            if (!isValid) // If the form is not valid
                MessageBox.Show(errorMessage, "Add Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                /*
                 *      Need code here to attempt adding the grade to the database
                 *      1. Query the database to make sure the requested record to add does not already exist
                 *          If the record already exists 
                 *          1.1 Display an error message
                 *          1.2 Return to the main menu (DO NOT PROCEED)
                 *      2. Add the record 
                 *      3. Update the student's GPA
                 *      4. Display a confirmation message
                 *      5. Return to the main menu
                 */

                // This should only be displayed if there is already a record with matching data in the database
                MessageBox.Show("The requested record already exists in the database.", "Add Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // This should only be displayed if the add is actually successful, not just if the form is valid
                MessageBox.Show("Record successfully added.", "Add Record Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                

                this.Close();
            }
        }
    }
}
