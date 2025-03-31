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
    public partial class ModifyRecord : Form
    {
        public ModifyRecord()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void ModifyRecord_Load(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var (isValid, errorMessage) = Helper.validateModifyRecordForms(StudentIDIn.Text, CoursePrefixIn.Text, CourseNumIn.Text, GradeIn.Text, YearIn.Text, SemesterIn.Text);

            if (!isValid) // If the form is not valid
                MessageBox.Show(errorMessage, "Modify Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                /*
                 *      Need code here to attempt modifying the grade in the database
                 *      1. Query the database to make sure the requested record to modify exists
                 *          If the record does not exists
                 *          1.1 Display an error message
                 *          1.2 Display the input form again
                 *      2. Retrieve the record from the database
                 *      3. Update the record's grade 
                 *      4. Update the student's GPA
                 *      5. Display a confirmation message
                 *      6. Return to the main menu
                 *      
                 */

                // This should only be displayed if the requested record does not exists in the database
                MessageBox.Show("The requested record does not exist in the database.", "Modify Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // This should be displayed when the record is retrieved from the database
                //      Note that the data is currently hard-coded in for demonstration
                //      purposes, but will be parsed from the record in practice
                string message = "Confirm the modification of this record:" +
                    "\nStudent ID: 901888777" +
                    "\nCourse: CSC 440" +
                    "\nGrade: A" +
                    "\nSpring, 2025" +
                    "\n\nChange the grade from A to B";
                MessageBox.Show(message, "Confirm Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // This should only be displayed if the add is actually successful, not just if the form is valid
                MessageBox.Show("Record successfully modified.", "Modify Record Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);



                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
