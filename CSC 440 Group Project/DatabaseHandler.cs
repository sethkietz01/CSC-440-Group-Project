using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CSC_440_Group_Project
{
    /// <summary>
    /// Performs all database interactions
    /// </summary>
    internal class DatabaseHandler
    {
        /// <summary>
        /// Imports grade records from a folder that contains one or more .CSV file(s)
        /// </summary>
        public void ImportRecordsFolder()
        {
            // Allow the user to select a folder from their desktop
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    // Variables - Store the folders path and the files inside the folder
                    string folderPath = folderDialog.SelectedPath;
                    string[] files = Directory.GetFiles(folderPath);

                    // Parse through the folder to select each file
                    foreach (string file in files)
                    {
                        // Variables - Store the file name and the file extension how many rows it contains (for checking usability) and the file name for each column
                        string filenameString = Path.GetFileNameWithoutExtension(file);
                        string fileExtension = Path.GetExtension(file);
                        string[] filename = filenameString.Split(' ');
                        string[] fileRows = File.ReadAllLines(file);

                        // Check if the file we are currently looking at is in .csv format
                        if (fileExtension != ".csv" && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                        {
                            // File was not in .csv format - Output error message and send user back to the import screen
                            MessageBox.Show("Invalid file type. Grades must be imported from an Excel file (.csv).", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            // File was in .csv format - Check if the file name is valid
                            // Following the format, [0] will be the prefix, [1] is the number, [2] is the year, [3] is the semester
                            if (filename.Length != 4 || filename[0].Length != 3 || filename[1].Length != 3 || filename[2].Length != 4)
                            {
                                // File was not in the correct format - Output error message and send user back to the import screen
                                // Show the user which file name is incorrect
                                MessageBox.Show("Invalid file name." + filenameString + " File name must follow the format [Course Prefix] [Course Numer] [Year] [Semester].", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Call Import Records Here
                            ImportRecordsFromFile(fileRows, filename);
                        }
                    }
                    // Show the user that the import was successful
                    MessageBox.Show("Grades have been successfully imported.", "Import Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == DialogResult.Cancel)
                {
                    // Do nothing
                }

            }
        }

        /// <summary>
        /// Takes imports the grade records in a .CSV file into the database.
        /// </summary>
        /// <param name="fileRows">The number of grade records in the file</param>
        /// <param name="filename">The name of the file of grade records</param>
        public void ImportRecordsFromFile(String[] fileRows, String[] filename)
        {
            // Create Attributes
            // These are taken from the file name
            string coursePrefix;
            string courseNum;
            string year;
            string semester;

            // These are taken from the file lines
            string grade;
            string studentID;
            string studentName;

            // Connect to the database
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                // Extract the file name to their components
                coursePrefix = filename[0];
                courseNum = filename[1];
                year = filename[2];
                semester = filename[3];

                // Prep the sql statement for grades
                string sqlGrades = "INSERT INTO sklc440grades (studentID, coursePrefix, courseNum, grade, year, semester) SELECT @studentID, @coursePrefix, @courseNum, @grade, @year, @semester WHERE NOT EXISTS (SELECT 1 FROM sklc440grades WHERE studentID = @studentID AND coursePrefix = @coursePrefix AND courseNum = @courseNum AND year = @year AND semester = @semester);";
                MySqlCommand cmdG = new MySqlCommand(sqlGrades, conn);

                // Prep sql statement for student
                string sqlStudent = "INSERT IGNORE INTO sklc440student (studentID, studentName) VALUES (@studentID, @studentName)";
                MySqlCommand cmdS = new MySqlCommand(sqlStudent, conn);

                // Initialize how many cols are present for each student/row
                // For our case it is 3: Student Name, Student ID, and Grade
                int colsPerStudent = 3;

                // Start at i=1 since i=0 will be the headers
                for (int student = 1; student < fileRows.Length; student++)
                {
                    Console.WriteLine(fileRows[student]);
                    //Split the current student line into columns
                    string[] columns = fileRows[student].Split(',');

                    //Ensure the student we grabbed has all 3 columns needed
                    if (columns.Length < colsPerStudent)
                    {
                        MessageBox.Show($"Invalid data on line {student + 1}. {columns.Length}", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    // Store each column to its own variable
                    studentName = columns[0].Trim();
                    studentID = columns[1].Trim();
                    grade = columns[2].Trim();

                    // Add to grades table
                    cmdG.Parameters.AddWithValue("@studentID", studentID);
                    cmdG.Parameters.AddWithValue("@coursePrefix", coursePrefix);
                    cmdG.Parameters.AddWithValue("@courseNum", courseNum);
                    cmdG.Parameters.AddWithValue("@grade", grade);
                    cmdG.Parameters.AddWithValue("year", year);
                    cmdG.Parameters.AddWithValue("semester", semester);

                    // Check if there is already a student for that ID

                    // Add to student table
                    cmdS.Parameters.AddWithValue("@studentID", studentID);
                    cmdS.Parameters.AddWithValue("@studentName", studentName);

                    //Execute Queries
                    cmdG.ExecuteNonQuery();
                    cmdS.ExecuteNonQuery();

                    // clear parameters for another student
                    cmdG.Parameters.Clear();
                    cmdS.Parameters.Clear();

                    updateGPA(studentID, conn);
                }

                // Prep sql select statement for course
                string sqlCourse = "INSERT INTO sklc440courses (coursePrefix, courseNum, year, semester, hours) SELECT @coursePrefix, @courseNum, @year, @semester, @hours WHERE NOT EXISTS (SELECT 1 FROM sklc440courses WHERE coursePrefix = @coursePrefix AND courseNum = @courseNum AND year = @year AND semester = @semester AND hours = @hours);";

                string hours = InputHours();

                using (MySqlCommand cmdC = new MySqlCommand(sqlCourse, conn))
                {
                    // Add to course table
                    cmdC.Parameters.AddWithValue("@coursePrefix", coursePrefix);
                    cmdC.Parameters.AddWithValue("@courseNum", courseNum);
                    cmdC.Parameters.AddWithValue("@year", year);
                    cmdC.Parameters.AddWithValue("@semester", semester);
                    cmdC.Parameters.AddWithValue("@hours", hours);

                    cmdC.ExecuteNonQuery();

                }

            }
        }

        /// <summary>
        /// Gets the number of credit hours for a new course from user keyboard input
        /// </summary>
        /// <returns>The course's credit hours</returns>
        private string InputHours()
        {
            // New course was added - Need to gather the hours for the course
            Form hoursPromptForm = new Form()
            {
                Width = 350,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Course Hours",
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label inputHoursLabel = new Label() { Left = 10, Top = 10, Text = "Enter the number of hours for the course:", AutoSize = true };
            System.Windows.Forms.TextBox inputHoursTextBox = new System.Windows.Forms.TextBox() { Left = 10, Top = 35, Width = 300 };

            System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button() { Text = "OK", Left = 220, Width = 90, Top = 70, DialogResult = DialogResult.OK };
            hoursPromptForm.AcceptButton = confirmation;

            hoursPromptForm.Controls.Add(inputHoursLabel);
            hoursPromptForm.Controls.Add(inputHoursTextBox);
            hoursPromptForm.Controls.Add(confirmation);

            DialogResult result = hoursPromptForm.ShowDialog();
            return result == DialogResult.OK ? inputHoursTextBox.Text : null;
        }

        /// <summary>
        /// Adds a single grade record to the database
        /// </summary>
        /// <param name="studentID">The target student's ID</param>
        /// <param name="coursePrefix">The prefix of the course to of thegrade record for</param>
        /// <param name="courseNum">The identifing number of the course of the grade record for</param>
        /// <param name="grade">The grade that the student received</param>
        /// <param name="year">The year that the course was taken</param>
        /// <param name="semester">The semester that the course was taken</param>
        public void addRecord(string studentID, string coursePrefix, string courseNum, string grade, string year, string semester)
        {
            // Don't allow a grade record with a future year
            if (int.Parse(year) > System.DateTime.Now.Year)
                return;

            // Don't allow a grade record with a future semester
            if (semester == "Fall" && System.DateTime.Now.Month < 12 && int.Parse(year) == System.DateTime.Now.Year)
                return;

            if (semester == "Summer" && System.DateTime.Now.Month < 8 && int.Parse(year) == System.DateTime.Now.Year)
                return;

            if (semester == "Spring" && System.DateTime.Now.Month < 5 && int.Parse(year) == System.DateTime.Now.Year)
                return;


            // Connect to the database
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // Check if the student exists
                    string checkStudentQuery = "SELECT COUNT(*) FROM sklc440student WHERE studentID = @StudentID";
                    using (MySqlCommand checkStudentCmd = new MySqlCommand(checkStudentQuery, conn))
                    {
                        checkStudentCmd.Parameters.AddWithValue("@StudentID", studentID);
                        long studentExists = (long)checkStudentCmd.ExecuteScalar();

                        if (studentExists == 0)
                        {
                            MessageBox.Show($"Student with ID '{studentID}' does not exist.", "Add Record Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Exit the method if the student doesn't exist
                        }
                    }

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
                            updateGPA(studentID, conn);

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

        /// <summary>
        /// Deletes a grade record from the database
        /// </summary>
        /// <param name="studentID">The target student's ID</param>
        /// <param name="coursePrefix">The prefix of the course to of thegrade record for</param>
        /// <param name="courseNum">The identifing number of the course of the grade record for</param>
        /// <param name="grade">The grade that the student received</param>
        /// <param name="year">The year that the course was taken</param>
        /// <param name="semester">The semester that the course was taken</param>
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
                                updateGPA(studentIDToDelete, conn);
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

        /// <summary>
        /// Modifies the grade received for a specific grade record
        /// </summary>
        /// <param name="studentID">The target student's ID</param>
        /// <param name="coursePrefix">The prefix of the course to of thegrade record for</param>
        /// <param name="courseNum">The identifing number of the course of the grade record for</param>
        /// <param name="grade">The grade that the student received</param>
        /// <param name="year">The year that the course was taken</param>
        /// <param name="semester">The semester that the course was taken</param>
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
                            updateGPA(studentID, conn);
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

        /// <summary>
        /// Prints the GPA and all grade records for a specific student
        /// </summary>
        /// <param name="studentID">The target student's ID</param>
        public void printTranscript(string studentID)
        {
            // Connect to the database
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    updateGPA(studentID, conn);

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

        /// <summary>
        /// Calculates the cumulative GPA for a student, retrieving course hours from the database.
        /// </summary>
        /// <param name="studentID">The ID of the student.</param>
        /// <param name="conn">The MySQL connection object.</param>
        /// <returns>The calculated GPA, or -1.0 if an error occurs.</returns>
        public static double calculateGPA(string studentID, MySqlConnection conn)
        {
            try
            {
                // Get all grades and associated course information for the student
                string gradeQuery = @"
                    SELECT g.grade, c.hours
                    FROM sklc440grades g
                    INNER JOIN sklc440courses c ON g.coursePrefix = c.coursePrefix AND g.courseNum = c.courseNum
                    WHERE g.studentID = @studentID";

                double totalGradePoints = 0;
                int totalAttemptedCredits = 0;

                using (MySqlCommand gradeCmd = new MySqlCommand(gradeQuery, conn))
                {
                    gradeCmd.Parameters.AddWithValue("@studentID", studentID);
                    using (MySqlDataReader gradeReader = gradeCmd.ExecuteReader())
                    {
                        while (gradeReader.Read())
                        {
                            char grade = gradeReader.GetChar("grade");
                            int hours = gradeReader.GetInt32("hours");
                            double gradePoint = 0;

                                    Console.WriteLine("grade = " + grade + "\nhours = " + hours);

                            switch (grade)
                            {
                                case 'A':
                                    gradePoint = 4.0;
                                    break;
                                case 'B':
                                    gradePoint = 3.0;
                                    break;
                                case 'C':
                                    gradePoint = 2.0;
                                    break;
                                case 'D':
                                    gradePoint = 1.0;
                                    break;
                                case 'F':
                                    gradePoint = 0.0;
                                    break;
                                default:
                                    hours = 0;
                                    break;
                            }

                            totalGradePoints += gradePoint * hours;
                            totalAttemptedCredits += hours;
                        }
                    }
                }

                // Calculate and return GPA
                if (totalAttemptedCredits > 0)
                    return Math.Round(totalGradePoints / totalAttemptedCredits, 2);
                else
                    return 0.0;
            }
            catch (MySqlException ex)
            {
                // Handle database connection or query errors
                Console.WriteLine($"Error in calculateGPA: {ex.Message}");
                return -1.0; // Or throw an exception
            }
        }

        public static void updateGPA(string studentID, MySqlConnection conn)
        {
            try
            {

                double gpa = calculateGPA(studentID, conn); // Pass the open connection

                // Update the Student table with the calculated GPA
                string updateQuery = "UPDATE sklc440student SET GPA = @gpa WHERE studentID = @studentID";
                using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                {
                    updateCmd.Parameters.AddWithValue("@gpa", gpa);
                    updateCmd.Parameters.AddWithValue("@studentID", studentID);

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"GPA updated successfully for student ID: {studentID}");
                    }
                    else
                    {
                        Console.WriteLine($"No student found with ID: {studentID} to update GPA.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Handle database connection or query errors
                Console.WriteLine($"Error updating GPA: {ex.Message}");
            }
        }
    }
}
