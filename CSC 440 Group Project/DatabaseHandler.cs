using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CSC_440_Group_Project
{
    internal class DatabaseHandler
    {

        public void addRecord(string studentID, string coursePrefix, string courseNum, string grade, string year, string semester)
        {
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
                        checkIfExistsCmd.Parameters.AddWithValue("@StudentID", studentID);
                        checkIfExistsCmd.Parameters.AddWithValue("@CoursePrefix", coursePrefix);
                        checkIfExistsCmd.Parameters.AddWithValue("@CourseNum", courseNum);
                        checkIfExistsCmd.Parameters.AddWithValue("@Year", year);
                        checkIfExistsCmd.Parameters.AddWithValue("@Semester", semester);

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
                        insertCmd.Parameters.AddWithValue("@StudentID", studentID);
                        insertCmd.Parameters.AddWithValue("@CoursePrefix", coursePrefix);
                        insertCmd.Parameters.AddWithValue("@CourseNum", courseNum);
                        insertCmd.Parameters.AddWithValue("@Grade", grade);
                        insertCmd.Parameters.AddWithValue("@Year", year);
                        insertCmd.Parameters.AddWithValue("@Semester", semester);

                        int rowsAffected = insertCmd.ExecuteNonQuery();

                        // If the add was successful, update the GPA for the student and display a confirmation message
                        if (rowsAffected > 0)
                        {
                            Helper.updateGPA(studentID, conn);

                            MessageBox.Show("Record successfully added.", "Add Record Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        public void deleteRecord(string studentID, string coursePrefix, string courseNum, string grade, string year, string semester)
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
                    selectCmd.Parameters.AddWithValue("@StudentID", studentID);
                    selectCmd.Parameters.AddWithValue("@CoursePrefix", coursePrefix);
                    selectCmd.Parameters.AddWithValue("@CourseNum", courseNum);
                    selectCmd.Parameters.AddWithValue("@Year", year);
                    selectCmd.Parameters.AddWithValue("@Semester", semester);

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
                        string checkCoursePrefix = reader["CoursePrefix"].ToString();
                        string checkCourseNum = reader["CourseNum"].ToString();
                        string checkGrade = reader["Grade"].ToString();
                        string checkYear = reader["Year"].ToString();
                        string checkSemester = reader["Semester"].ToString();
                        recordExists = true;

                        // Display the retrieved record to the user, prompting to confirm the deletion
                        string message = "Confirm the deletion of this record:" +
                                         "\nStudent ID: " + studentIDToDelete +
                                         "\nCourse: " + checkCoursePrefix + " " + checkCourseNum +
                                         "\nGrade: " + checkGrade +
                                         "\n" + checkSemester + ", " + checkYear;

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
                            deleteCmd.Parameters.AddWithValue("@StudentID", studentID);
                            deleteCmd.Parameters.AddWithValue("@CoursePrefix", coursePrefix);
                            deleteCmd.Parameters.AddWithValue("@CourseNum", courseNum);
                            deleteCmd.Parameters.AddWithValue("@Year", year);
                            deleteCmd.Parameters.AddWithValue("@Semester", semester);

                            int rowsAffected = deleteCmd.ExecuteNonQuery();

                            // If the deletion was successful, update the student's GPA, display a confirmation message, and return to the main menu
                            if (rowsAffected > 0)
                            {
                                Helper.updateGPA(studentIDToDelete, conn);
                                MessageBox.Show("Record successfully deleted.", "Delete Record Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void modifyRecord(string studentID, string coursePrefix, string courseNum, string grade, string year, string semester)
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
                    selectCmd.Parameters.AddWithValue("@StudentID", studentID);
                    selectCmd.Parameters.AddWithValue("@CoursePrefix", coursePrefix);
                    selectCmd.Parameters.AddWithValue("@CourseNum", courseNum);
                    selectCmd.Parameters.AddWithValue("@Year", year);
                    selectCmd.Parameters.AddWithValue("@Semester", semester);

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
                        string checkStudentID = reader["StudentID"].ToString();
                        string checkCoursePrefix = reader["CoursePrefix"].ToString();
                        string checkCourseNum = reader["CourseNum"].ToString();
                        originalGrade = reader["Grade"].ToString();
                        string checkYear = reader["Year"].ToString();
                        string checkSemester = reader["Semester"].ToString();

                        // Display the retrieved record to the user, prompting to confirm the modification
                        string message = "Confirm the modification of this record:" +
                                         "\nStudent ID: " + checkStudentID +
                                         "\nCourse: " + checkCoursePrefix + " " + checkCourseNum +
                                         "\nGrade: " + originalGrade +
                                         "\n" + checkSemester + ", " + checkYear +
                                         "\n\nChange the grade from " + originalGrade + " to " + grade;

                        DialogResult result = MessageBox.Show(message, "Confirm Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        // If the user presses cancel, stop the transaction
                        if (result == DialogResult.No)
                            return;
                    }

                    // Update the record's grade
                    string updateQuery = "UPDATE sklc440grades SET Grade = @NewGrade WHERE StudentID = @StudentID AND CoursePrefix = @CoursePrefix AND CourseNum = @CourseNum AND Year = @Year AND Semester = @Semester";
                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@NewGrade", grade);
                        updateCmd.Parameters.AddWithValue("@StudentID", studentID);
                        updateCmd.Parameters.AddWithValue("@CoursePrefix", coursePrefix);
                        updateCmd.Parameters.AddWithValue("@CourseNum", courseNum);
                        updateCmd.Parameters.AddWithValue("@Year", year);
                        updateCmd.Parameters.AddWithValue("@Semester", semester);

                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Helper.updateGPA(studentID, conn);
                            MessageBox.Show("Record successfully modified.", "Modify Record Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void printTranscript(string studentID)
        {
            // Connect to the database
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    string gradeQuery = @"
                                    SELECT
                                        g.coursePrefix,
                                        g.courseNum,
                                        g.grade,
                                        g.semester,
                                        g.year,
                                        s.GPA
                                    FROM
                                        sklc440grades g
                                    JOIN
                                        sklc440student s ON g.studentID = s.studentID
                                    WHERE
                                        g.studentID = @StudentID;"
                    ;

                    using (MySqlCommand command = new MySqlCommand(gradeQuery, conn))
                    {
                        command.Parameters.AddWithValue("@StudentID", studentID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            bool firstRecord = true;
                            decimal studentGPA = -1;


                            string transcript = "Transcript for Student " + studentID + "\n\nCourse\tSemester\t\tGrade";

                            while (reader.Read())
                            {
                                if (firstRecord)
                                {
                                    studentGPA = reader.GetDecimal(reader.GetOrdinal("GPA"));
                                    firstRecord = false;
                                }

                                string courseInfo = reader["coursePrefix"].ToString() + " " + reader["courseNum"].ToString();
                                string semesterYear = reader["semester"].ToString() + ", " + reader["year"].ToString();
                                string grade = reader["grade"].ToString();

                                transcript += "\n" + courseInfo + "\t" + semesterYear + "\t\t" + grade;
                            }

                            if (studentGPA != -1)
                            {
                                transcript += $"\n\nCumulative GPA: {studentGPA}";
                                MessageBox.Show(transcript, "Print Transcript Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"No grade records found for student ID: {studentID}", "Print Transcript Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Error printing transcript: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
