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
                // Connect to the database
                string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();

                        // Query the database to make sure the requested record to delete exists
                        string selectQuery = "SELECT StudentID, CoursePrefix, CourseNum, Grade, Year, Semester FROM sklc440grades WHERE StudentID = @StudentID AND CoursePrefix = @CoursePrefix AND CourseNum = @CourseNum AND Year = @Year AND Semester = @Semester";
                        MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                        selectCmd.Parameters.AddWithValue("@StudentID", StudentIDIn.Text);
                        selectCmd.Parameters.AddWithValue("@CoursePrefix", CoursePrefixIn.Text);
                        selectCmd.Parameters.AddWithValue("@CourseNum", CourseNumIn.Text);
                        selectCmd.Parameters.AddWithValue("@Year", YearIn.Text);
                        selectCmd.Parameters.AddWithValue("@Semester", SemesterIn.Text);

                        bool recordExists = false;
                        string studentIDToDelete = "";
                        using (MySqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            // If there is no matching record, stop the transaction
                            if (!reader.HasRows)
                            {
                                MessageBox.Show("The requested record does not exist in the database.", "Delete Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Retrieve the record from the database
                            reader.Read();
                            studentIDToDelete = reader["StudentID"].ToString();
                            string coursePrefix = reader["CoursePrefix"].ToString();
                            string courseNum = reader["CourseNum"].ToString();
                            string grade = reader["Grade"].ToString();
                            string year = reader["Year"].ToString();
                            string semester = reader["Semester"].ToString();
                            recordExists = true;

                            // Display the retrieved record to the user, prompting to confirm the deletion
                            string message = "Confirm the deletion of this record:" +
                                             "\nStudent ID: " + studentIDToDelete +
                                             "\nCourse: " + coursePrefix + " " + courseNum +
                                             "\nGrade: " + grade +
                                             "\n" + semester + ", " + year;

                            DialogResult result = MessageBox.Show(message, "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            // If the user presses cancel, stop the transaction
                            if (result == DialogResult.No)
                                return;
                        } 

                        if (recordExists)
                        {
                            // Delete the record from the database
                            string deleteQuery = "DELETE FROM sklc440grades WHERE StudentID = @StudentID AND CoursePrefix = @CoursePrefix AND CourseNum = @CourseNum AND Year = @Year AND Semester = @Semester";
                            using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                            {
                                deleteCmd.Parameters.AddWithValue("@StudentID", StudentIDIn.Text);
                                deleteCmd.Parameters.AddWithValue("@CoursePrefix", CoursePrefixIn.Text);
                                deleteCmd.Parameters.AddWithValue("@CourseNum", CourseNumIn.Text);
                                deleteCmd.Parameters.AddWithValue("@Year", YearIn.Text);
                                deleteCmd.Parameters.AddWithValue("@Semester", SemesterIn.Text);

                                int rowsAffected = deleteCmd.ExecuteNonQuery();

                                // If the deletion was successful, update the student's GPA, display a confirmation message, and return to the main menu
                                if (rowsAffected > 0)
                                {
                                    Helper.updateGPA(studentIDToDelete, conn);
                                    MessageBox.Show("Record successfully deleted.", "Delete Record Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                                else
                                    MessageBox.Show("Failed to delete the record.", "Delete Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"Error deleting record: {ex.Message}");
                        MessageBox.Show($"Database error: {ex.Message}", "Delete Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
