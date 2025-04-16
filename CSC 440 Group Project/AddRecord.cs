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

                // Connect to the database
                string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();

                        // Query the database to check for an existing record
                        string checkIfExistsQuery = "SELECT COUNT(*) FROM sklc440grades WHERE StudentID = @StudentID AND CoursePrefix = @CoursePrefix AND CourseNum = @CourseNum AND Year = @Year AND Semester = @Semester";
                        using (MySqlCommand checkIfExistsCmd = new MySqlCommand(checkIfExistsQuery, conn))
                        {
                            checkIfExistsCmd.Parameters.AddWithValue("@StudentID", StudentIDIn.Text);
                            checkIfExistsCmd.Parameters.AddWithValue("@CoursePrefix", CoursePrefixIn.Text);
                            checkIfExistsCmd.Parameters.AddWithValue("@CourseNum", CourseNumIn.Text);
                            checkIfExistsCmd.Parameters.AddWithValue("@Year", YearIn.Text);
                            checkIfExistsCmd.Parameters.AddWithValue("@Semester", SemesterIn.Text);

                            long existingRecordCount = (long)checkIfExistsCmd.ExecuteScalar();

                            // If the record already exists, show an error message and do not proceed with the add
                            if (existingRecordCount > 0)
                            {
                                MessageBox.Show("The requested record already exists in the database.", "Add Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Add the record
                        string insertQuery = "INSERT INTO sklc440grades (StudentID, CoursePrefix, CourseNum, Grade, Year, Semester) VALUES (@StudentID, @CoursePrefix, @CourseNum, @Grade, @Year, @Semester)";
                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@StudentID", StudentIDIn.Text);
                            insertCmd.Parameters.AddWithValue("@CoursePrefix", CoursePrefixIn.Text);
                            insertCmd.Parameters.AddWithValue("@CourseNum", CourseNumIn.Text);
                            insertCmd.Parameters.AddWithValue("@Grade", GradeIn.Text);
                            insertCmd.Parameters.AddWithValue("@Year", YearIn.Text);
                            insertCmd.Parameters.AddWithValue("@Semester", SemesterIn.Text);

                            int rowsAffected = insertCmd.ExecuteNonQuery();

                            // If the add was successful, update the GPA for the student and display a confirmation message
                            if (rowsAffected > 0)
                            {
                                Helper.updateGPA(StudentIDIn.Text, conn);

                                MessageBox.Show("Record successfully added.", "Add Record Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                this.Close();
                            }
                            else
                                MessageBox.Show("Failed to add the record.", "Add Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"Error adding record: {ex.Message}");
                        MessageBox.Show($"Database error: {ex.Message}", "Add Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                

                this.Close();
            }
        }
    }
}
