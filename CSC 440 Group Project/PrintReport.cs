using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

            // Connect to the database
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";

            string studentID = textBox1.Text.Trim();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open(); // Open the connection once here

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
                                string semesterYear = reader["semester"].ToString() + " " + reader["year"].ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
