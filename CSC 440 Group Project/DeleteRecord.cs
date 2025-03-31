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
    public partial class Delete_Record : Form
    {
        public Delete_Record()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var (isValid, errorMessage) = Helper.validateModifyRecordForms(StudentIDIn.Text, CoursePrefixIn.Text, CourseNumIn.Text, GradeIn.Text, YearIn.Text, SemesterIn.Text);

            if (!isValid) // If the form is not valid
                MessageBox.Show(errorMessage, "Delete Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                /*
                 *      Need code here to attempt deleting the grade to the database
                 *      1. Query the database to make sure the requested record to delete exists
                 *          If the record does not exists
                 *          1.1 Display an error message
                 *          1.2 Return to the main menu (DO NOT PROCEED)
                 *      2. Retrieve the record from the database
                 *      3. Display the retrieved record to the user, prompting to confirm the deletion
                 *          If the user presses cancel
                 *          3.1 Cancel current action
                 *          3.2 Display the input form again
                 *      4. Delete the record from the database
                 *      5. Update the student's GPA
                 *      6. Display a confirmation message
                 *      7. Return to the main menu
                 *      
                 */

                // This should only be displayed if the requested record does not exists in the database
                MessageBox.Show("The requested record does not exist in the database.", "Delete Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // This should be displayed when the record is retrieved from the database
                //      Note that the data is currently hard-coded in for demonstration
                //      purposes, but will be parsed from the record in practice
                string message = "Confirm the deletion of this record:" +
                    "\nStudent ID: 901888777" +
                    "\nCourse: CSC 440" +
                    "\nGrade: A" +
                    "\nSpring, 2025";
                MessageBox.Show(message, "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // This should only be displayed if the add is actually successful, not just if the form is valid
                MessageBox.Show("Record successfully deleted.", "Delete Record Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);



                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
