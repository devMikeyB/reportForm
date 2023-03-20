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
            incorrectAttempts++;
            attemptLabel.Text = $"{3 - incorrectAttempts} remaining.";
        }
    }
}
