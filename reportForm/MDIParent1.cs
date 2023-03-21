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
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;
        public List<Form> forms = new List<Form>();

        public MDIParent1()
        {
            InitializeComponent();
        }
        public List<Form> formsList()
        {
            return forms;
        }

        private Form ShowNewForm(object sender, EventArgs e, Form form)
        {
            Form childForm = new Form();
            childForm = form;
            childForm.MdiParent = this;
            childForm.Text = form.Text;
            childForm.Show();
            forms.Add(childForm);
            return childForm;
        }


        private void MDIParent1_Load(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                childFormNumber = 0;
                foreach (Form form in this.MdiChildren)
                {
                    childFormNumber++;
                    if (form.GetType().ToString() == "reportForm.LoginForm")
                    {
                        MessageBox.Show("Login Form already open");
                        return;
                    }
                }
                LoginForm loginForm = (LoginForm)ShowNewForm(sender, e, new LoginForm());
                //EventHandler 
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: \n" + ex.Message);
            }

        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {   childFormNumber= 0;
                foreach (Form form in this.MdiChildren)
                { childFormNumber++;
                    if (form.GetType().ToString() == "reportForm.ReportForm")
                    {
                        MessageBox.Show("Report Form already open");
                        return;
                    }
                }
                ShowNewForm(sender, e, new ReportForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: \n" + ex.Message);
            }
        }
    }
}
