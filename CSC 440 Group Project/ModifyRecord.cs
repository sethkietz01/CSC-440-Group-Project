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
                // Connect to the database
                string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();

                        // Query the database to make sure the requested record to modify exists
                        string selectQuery = "SELECT StudentID, CoursePrefix, CourseNum, Grade, Year, Semester FROM sklc440grades WHERE StudentID = @StudentID AND CoursePrefix = @CoursePrefix AND CourseNum = @CourseNum AND Year = @Year AND Semester = @Semester";
                        MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                        selectCmd.Parameters.AddWithValue("@StudentID", StudentIDIn.Text);
                        selectCmd.Parameters.AddWithValue("@CoursePrefix", CoursePrefixIn.Text);
                        selectCmd.Parameters.AddWithValue("@CourseNum", CourseNumIn.Text);
                        selectCmd.Parameters.AddWithValue("@Year", YearIn.Text);
                        selectCmd.Parameters.AddWithValue("@Semester", SemesterIn.Text);

                        string originalGrade = null;
                        using (MySqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            // If there is no matching record, stop the transaction
                            if (!reader.HasRows)
                            {
                                MessageBox.Show("The requested record does not exist in the database.", "Modify Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Retrieve the record from the database
                            reader.Read();
                            string studentID = reader["StudentID"].ToString();
                            string coursePrefix = reader["CoursePrefix"].ToString();
                            string courseNum = reader["CourseNum"].ToString();
                            originalGrade = reader["Grade"].ToString();
                            string year = reader["Year"].ToString();
                            string semester = reader["Semester"].ToString();

                            // Display the retrieved record to the user, prompting to confirm the modification
                            string message = "Confirm the modification of this record:" +
                                             "\nStudent ID: " + studentID +
                                             "\nCourse: " + coursePrefix + " " + courseNum +
                                             "\nGrade: " + originalGrade +
                                             "\n" + semester + ", " + year +
                                             "\n\nChange the grade from " + originalGrade + " to " + GradeIn.Text;

                            DialogResult result = MessageBox.Show(message, "Confirm Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            // If the user presses cancel, stop the transaction
                            if (result == DialogResult.No)
                                return;
                        } 

                        // Update the record's grade
                        string updateQuery = "UPDATE sklc440grades SET Grade = @NewGrade WHERE StudentID = @StudentID AND CoursePrefix = @CoursePrefix AND CourseNum = @CourseNum AND Year = @Year AND Semester = @Semester";
                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@NewGrade", GradeIn.Text);
                            updateCmd.Parameters.AddWithValue("@StudentID", StudentIDIn.Text);
                            updateCmd.Parameters.AddWithValue("@CoursePrefix", CoursePrefixIn.Text);
                            updateCmd.Parameters.AddWithValue("@CourseNum", CourseNumIn.Text);
                            updateCmd.Parameters.AddWithValue("@Year", YearIn.Text);
                            updateCmd.Parameters.AddWithValue("@Semester", SemesterIn.Text);

                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Helper.updateGPA(StudentIDIn.Text, conn);
                                MessageBox.Show("Record successfully modified.", "Modify Record Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                                MessageBox.Show("Failed to modify the record.", "Modify Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"Error modifying record: {ex.Message}");
                        MessageBox.Show($"Database error: {ex.Message}", "Modify Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
