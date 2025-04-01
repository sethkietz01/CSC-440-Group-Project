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
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                /**
                 * Need to add code that validates the folder name and imports all excel files in the folder
                 */

                MessageBox.Show("Grades have been successfully imported.", "Import Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create Attributes
            string coursePrefix;
            string courseNum;
            string year;
            string grade;
            string semester;
            string studentID;
            string studentName;

            openFileDialog1.Filter = "CSV files (*.csv)|*.csv|XLSX files (*.xlsx)|*.xlsx";

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Ensure the file is an Excel file
                string fileExtension = System.IO.Path.GetExtension(openFileDialog1.FileName);
                if (!fileExtension.Equals(".csv") && !fileExtension.Equals(".xlsx"))
                {
                    MessageBox.Show("Invalid file type. Grades must be imported from an Excel file (.csv or .xlsx).", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ensure the filename is valid according to the specified format
                // Following the format, [0] will be the prefix, [1] is the number, [2] is the year, [3] is the semester
                string filenameString = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                string[] filename = filenameString.Split(' ');
                if (filename.Length !=4 || filename[0].Length != 3 || filename[1].Length != 3 || filename[2].Length != 4)
                {
                    MessageBox.Show("Invalid file name. File name must follow the format [Course Prefix] [Course Numer] [Year] [Semester].", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Extract the file name to their components
                coursePrefix = filename[0];
                courseNum = filename[1];
                year = filename[2];
                semester = filename[3];

                // Connect to the database
                string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    // Add the course to the database - THIS NEEDS TO BE DONE FIRST
                    string sql = "INSERT INTO sklc440courses (coursePrefix, courseNum, year, semester) VALUES (@coursePrefix, @courseNum, @year, @semester)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@coursePrefix", coursePrefix);
                    cmd.Parameters.AddWithValue("@courseNum", courseNum);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Parameters.AddWithValue("@semester", semester);

                    // Check to make sure there was an update to atleast one row
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Show grades added successfully
                        MessageBox.Show("Grades Imported Successfully!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Show error message to user
                        MessageBox.Show("Failed to add Grade.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                this.Close();
            }
            else if (result == DialogResult.Cancel)
            {

            }
            else
            {
                MessageBox.Show("Import failed, please try again.", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Now we need to actually read the file
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        bool isHeader = true;

                        // Connect to database
                        string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";
                        using (MySqlConnection conn = new MySqlConnection(connStr))
                        {
                            conn.Open();

                            while ((line = reader.ReadLine()) != null)
                            {
                                if (isHeader) // Skip the headers
                                {
                                    isHeader = false;
                                    continue;
                                }

                                string[] data = line.Split(',');

                                if (data.Length != 3)
                                {
                                    MessageBox.Show("Invalid format. Each row must contain: Name, ID, Grade");
                                    return;
                                }

                                studentName = data[0].Trim();
                                studentID = data[1].Trim();
                                grade = data[2].Trim();

                                // Add to student - THIS NEEDS TO BE DONE SECOND
                                string sql = "INSERT INTO sklc440student (studentName, studentID) VALUES (@firstName, @studentID)";
                                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                                {
                                    cmd.Parameters.AddWithValue("@firstName", studentName);
                                    cmd.Parameters.AddWithValue("@studentID", studentID);
                                    cmd.ExecuteNonQuery();
                                }

                                // Change sql statement
                                sql = "INSERT INTO sklc440grades (studentID, grade) VALUES (@studentID, @grade)";
                            }
                        }
                    }
                } catch (IOException ex)
                {

                }
            }
        }

        private void mainLabel_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
