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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void importRecordsButton_Click(object sender, EventArgs e)
        {
            importRecords importRecordsForm = new importRecords();
            importRecordsForm.ShowDialog();
        }

        private void addRecordButton_Click(object sender, EventArgs e)
        {
            Add_Record addRecordForm = new Add_Record();
            addRecordForm.ShowDialog();
        }

        private void deleteRecordButton_Click(object sender, EventArgs e)
        {
            Delete_Record deleteRecordForm = new Delete_Record();
            deleteRecordForm.ShowDialog();
        }

        private void modifyRecordButton_Click(object sender, EventArgs e)
        {
            ModifyRecord modifyRecordForm = new ModifyRecord();
            modifyRecordForm.ShowDialog();
        }

        private void printReportButton_Click(object sender, EventArgs e)
        {
            Print_Report printReportForm = new Print_Report();
            printReportForm.ShowDialog();
        }

        

        
    }
}
