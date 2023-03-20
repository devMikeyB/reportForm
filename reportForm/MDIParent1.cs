using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void ShowNewForm(object sender, EventArgs e, Form form)
        {
            Form childForm = new Form();
            childForm = form;
            childForm.MdiParent = this;
            childForm.Text = form.Text + $" [{childFormNumber}]";
            childForm.Show();
            childFormNumber++;  
            forms.Add(childForm);
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            if (ActiveMdiChild == null)
            {
                ShowNewForm(sender, e, new LoginForm());
            } else
            {
                MDIParent1.ActiveForm.Focus();
                MessageBox.Show("Login form already open.");
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: \n" + ex.Message);
            }

        }
    }
}
