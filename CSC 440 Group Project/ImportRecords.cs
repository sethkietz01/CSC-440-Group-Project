using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace CSC_440_Group_Project
{
    public partial class importRecords : Form
    {
        public importRecords()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
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

                // Close application
                this.Close();
            }
        }

        /*
         *      Steps for Importing a Record
         *      1. Gather if the record is in a file or a folder.
         *          a. Check if the file inside the folder or the file itself is a .csv or .xlsx
         *          b. If the file is not, send error message. 
         *      2. Check if the file has a correct header
         *          a. If not, send error message
         *      3.  Read the file name and store:
         *          a. Course Prefix
         *          b. Course Number
         *          c. Year
         *          d. Semester
         *      4. Read the file line by line- Store:
         *          a. Student Name
         *          b. Student ID
         *          c. Student Grade
         *      5. Finally Open the connection to the database and add
         *      all of the saved information.
         *          a. Open the Student Grades table and import.
         *      6. Recalculate Student GPA
         * 
         */

        private void button1_Click(object sender, EventArgs e)
        {
                // Only allow files with .csv
                openFileDialog1.Filter = "CSV files (*.csv)|*.csv";

                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Ensure the file is an Excel file
                    string fileExtension = System.IO.Path.GetExtension(openFileDialog1.FileName);
                    if (!fileExtension.Equals(".csv"))
                    {
                        MessageBox.Show("Invalid file type. Grades must be imported from an Excel file (.csv).", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Ensure the filename is valid according to the specified format
                    // Following the format, [0] will be the prefix, [1] is the number, [2] is the year, [3] is the semester
                    string filenameString = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    string[] filename = filenameString.Split(' ');
                    string[] fileRows = File.ReadAllLines(openFileDialog1.FileName);
                    if (filename.Length != 4 || filename[0].Length != 3 || filename[1].Length != 3 || filename[2].Length != 4)
                    {
                        MessageBox.Show("Invalid file name. File name must follow the format [Course Prefix] [Course Numer] [Year] [Semester].", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                // Call Import Records Here
                ImportRecordsFromFile(fileRows, filename);
                MessageBox.Show("Grades have been successfully imported.", "Import Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                else if (result == DialogResult.Cancel)
                {

                }
                this.Close();
        }

        private void mainLabel_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // The ImportRecordsFromFile method takes the file name and the rows of the file and imports the records into the database.
        private void ImportRecordsFromFile(String[] fileRows, String[] filename)
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

                    // Check if there is already a grade for that student for that class

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
                }

                // Prep sql select statement for course
                string sqlCourse = "INSERT INTO sklc440courses (coursePrefix, courseNum, year, semester) SELECT @coursePrefix, @courseNum, @year, @semester WHERE NOT EXISTS (SELECT 1 FROM sklc440courses WHERE coursePrefix = @coursePrefix AND courseNum = @courseNum AND year = @year AND semester = @semester);";

                using (MySqlCommand cmdC = new MySqlCommand(sqlCourse, conn))
                {
                    // Add to course table
                    cmdC.Parameters.AddWithValue("@coursePrefix", coursePrefix);
                    cmdC.Parameters.AddWithValue("@courseNum", courseNum);
                    cmdC.Parameters.AddWithValue("@year", year);
                    cmdC.Parameters.AddWithValue("@semester", semester);

                    cmdC.ExecuteNonQuery();
                }
            }
        }
    }
}
