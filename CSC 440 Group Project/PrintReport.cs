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

        private void submitPrintButton_Click(object sender, EventArgs e)
        {
            DatabaseHandler dbHandler = new DatabaseHandler();

            dbHandler.printTranscript(textBox1.Text);

            this.Close();
        }

        private void cancelPrintButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
