using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace reportForm
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private int incorrectAttempts = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
           
        }

        private void loginTimer_Tick(object sender, EventArgs e)
        {
            
            if(progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value++;
                passProgLabel.Text = $"You have {progressBar1.Maximum - progressBar1.Value} seconds to login.";

            }
            else
            {
                loginTimer.Stop();
                MessageBox.Show("Out of time.");
                this.Close();
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            loginTimer.Start();

            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=login.mdb");
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Users WHERE Username = @username AND Email = @email AND Password = @password;";
            command.Parameters.AddWithValue("@username", userTextBox.Text);
            command.Parameters.AddWithValue("@email", emailTextBox.Text);
            command.Parameters.AddWithValue("@password", passwordTextBox.Text);
            command.Connection.Open();

            var result = command.ExecuteReader(); //Create reader
            int c = 0;
            while (result.Read()) //reader object returns True if it advances to a new line.
            {
                c++;
            }
            if (c == 1) //If reader found a line.
            {
                MessageBox.Show("Logged in");
                command.Connection.Close();
                loginTimer.Stop();
                progressBar1.Value = 0;
                return;
            }
            else //Not found
            {
                MessageBox.Show("Not found");
            }
            c = 0; //Reset count to 0;

            command.Connection.Close ();

            incorrectAttempts++;
            attemptLabel.Text = $"{3 - incorrectAttempts} remaining.";
            if (incorrectAttempts== 3) {
                this.Close();
            }
        }
    }
}
