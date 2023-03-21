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
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\micha\\source\\repos\\reportForm\\reportForm\\login.mdb");
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Users WHERE Username = @username AND Email = @email AND Password = @password;";
            command.Parameters.AddWithValue("@username", userTextBox.Text);
            command.Parameters.AddWithValue("@email", emailTextBox.Text);
            command.Parameters.AddWithValue("@password", passwordTextBox.Text);
            command.Connection.Open();

            var result = command.ExecuteNonQuery();
            Debug.WriteLine(result);
            if (result == 1) 
            {
                MessageBox.Show("Logged in.");
                command.Connection.Close();
                return;
            }

            incorrectAttempts++;
            attemptLabel.Text = $"{3 - incorrectAttempts} remaining.";
        }
    }
}
