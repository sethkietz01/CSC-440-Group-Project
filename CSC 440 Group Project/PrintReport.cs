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
            /*
             *  Need code here to first validate that the form is complete and all input is formatted as expected
             */

            /*
             *      Need code here to print a transcript for the student
             *      1. Check if the student exists
             *          If the student does not exists
             *          1.1 Display an error message
             *          1.2 Display the input form again
             *      2. Retrieve all records for the student
             *      3. Retrieve the GPA for the student
             *      4. Display all retrieved information 
             *      5. Return to the main menu
             */

            //This should only be displayed if there is no matching student ID in the database
            MessageBox.Show("Student 901888777 could not be found in the database.", "Print Transcript Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);


            // This should only be displayed upon successful record retrieval
            //      Note that the data is currently hard-coded in for demonstration
            //      purposes, but will be parsed from the records in practice
            string message = "Transcript for Student 901888777" +
                "\n\nCourse\tSemester\t\tGrade" +
                "\nCSC 340\tSpring, 2024\tB" +
                "\nCSC 440\tSpring, 2025\tA" +
                "\n\nCumulative GPA: 3.50";

            MessageBox.Show(message, "Print Transcript Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
