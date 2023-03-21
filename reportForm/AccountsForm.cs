using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reportForm
{
    public partial class AccountsForm : Form
    {
        public AccountsForm()
        {
            InitializeComponent();
        }
        public int selectedRow;

        private void AccountsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'loginDataSet.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.loginDataSet.Users);
            selectedRow = dataGridView1.CurrentCell.RowIndex;
        }

        private void nextToolStripButton_Click(object sender, EventArgs e)
        {
            previousToolStripButton.Enabled = true;
            firstRecordToolStripButton.Enabled = true;
            nextToolStripButton.Enabled = true;
            lastRecordToolStripButton.Enabled = true;
            if (selectedRow < dataGridView1.RowCount-2)
            {
                dataGridView1.Rows[selectedRow].Selected = false;
                selectedRow++;
                dataGridView1.Rows[selectedRow].Selected = true;
                if (selectedRow == dataGridView1.RowCount - 2)
                {
                    nextToolStripButton.Enabled = false;
                    lastRecordToolStripButton.Enabled = false;
                }
            }
            else
            {
                nextToolStripButton.Enabled = false;
                lastRecordToolStripButton.Enabled = false;
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            /*nextToolStripButton.Enabled = true;
            previousToolStripButton.Enabled = true;
            lastRecordToolStripButton.Enabled = true;
            firstRecordToolStripButton.Enabled = true;
            if (selectedRow == dataGridView1.RowCount - 3)
            {
                nextToolStripButton.Enabled= false;
                lastRecordToolStripButton.Enabled= false;
                return;
            }
            if (selectedRow == 0)
            {
                previousToolStripButton.Enabled= false;
                firstRecordToolStripButton.Enabled= false;
                return;
            }*/

        }

        private void previousToolStripButton_Click(object sender, EventArgs e)
        {
            previousToolStripButton.Enabled = true;
            firstRecordToolStripButton.Enabled = true;
            nextToolStripButton.Enabled = true;
            lastRecordToolStripButton.Enabled= true;
            if (selectedRow> 0)
            {
                dataGridView1.Rows[selectedRow].Selected = false;
                selectedRow--;
                dataGridView1.Rows[selectedRow].Selected = true;
                if (selectedRow == 0)
                {
                    previousToolStripButton.Enabled = false;
                    firstRecordToolStripButton.Enabled = false;
                }
            }
        }

        private void firstRecordToolStripButton_Click(object sender, EventArgs e)
        {
            if (selectedRow != 0)
            {
                dataGridView1.Rows[selectedRow].Selected = false;
                selectedRow = 0;
                dataGridView1.Rows[selectedRow].Selected = true;
                previousToolStripButton.Enabled = false;
                firstRecordToolStripButton.Enabled = false;
                lastRecordToolStripButton.Enabled = true;
                nextToolStripButton.Enabled = true;
            }
        }

        private void lastRecordToolStripButton_Click(object sender, EventArgs e)
        {
            if (selectedRow != dataGridView1.RowCount-2) {
                dataGridView1.Rows[selectedRow].Selected = false;
                selectedRow = dataGridView1.RowCount-2;
                dataGridView1.Rows[selectedRow].Selected = true;
                previousToolStripButton.Enabled = true;
                lastRecordToolStripButton.Enabled = false;
                nextToolStripButton.Enabled = false;
                firstRecordToolStripButton.Enabled = true;
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //e.
        }

        private void dataGridView1_Click(object sender, DataGridViewCellEventArgs e)
        {

                dataGridView1.Rows[selectedRow].Selected = false;
                selectedRow = e.RowIndex;
                dataGridView1.Rows[selectedRow].Selected = true;
                Debug.WriteLine("Clicked");
        }

        private void searchToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=login.mdb");
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Users WHERE Username = @search OR Email = @search OR Password = @search;";
                command.Parameters.AddWithValue("@search", searchToolStripTextBox.Text);
                command.Connection.Open();

                using (OleDbDataAdapter da = new OleDbDataAdapter(command))
                {
                    DataTable dt = new DataTable("Accounts");
                    da.Fill(dt);
                    dataGridView1.DataSource= dt;
            }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showAllRecordsToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=login.mdb");
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Users";

                using (OleDbDataAdapter da = new OleDbDataAdapter(command))
                {
                    DataTable dt = new DataTable("Accounts");
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
