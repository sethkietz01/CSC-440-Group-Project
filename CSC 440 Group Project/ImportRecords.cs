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
                string filenameString = Path.GetFileName(openFileDialog1.FileName);
                string[] filename = filenameString.Split(' ');
                if (filename[0].Length != 3 || filename[1].Length != 3 || filename[2].Length != 4)
                {
                    MessageBox.Show("Invalid file name. File name must follow the format [Course Prefix] [Course Numer] [Year] [Semester].", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                /**
                 * Need to add code to parse data from the file and add records to the database
                 */

                MessageBox.Show("Grades have been successfully imported.", "Import Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else if (result == DialogResult.Cancel)
            {

            }
            else
            {
                MessageBox.Show("Import failed, please try again.", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
