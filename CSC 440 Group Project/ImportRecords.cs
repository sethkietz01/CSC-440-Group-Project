using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
            DatabaseHandler dbHandler = new DatabaseHandler();

            dbHandler.ImportRecordsFolder();

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseHandler dbHandler = new DatabaseHandler();

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
            dbHandler.ImportRecordsFromFile(fileRows, filename);
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

        
    }
}
