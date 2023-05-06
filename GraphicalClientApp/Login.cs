using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class LoginPage : Form
    {
        private string enteredPass;

        public string EnteredPass
        {
            get { return enteredPass; }
            private set
            {
                enteredPass = value.ToString(); ;
            }
        }
        public LoginPage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbPassword.Text))
            {
                MessageBox.Show("You must enter a master password!", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                EnteredPass = TbPassword.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void BtnMassDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Deleting all data...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                this.DialogResult = DialogResult.Continue;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cBShow.Checked) {
                TbPassword.PasswordChar = '\0';
            }
            else {
                TbPassword.PasswordChar = '*';
            }
        }
    }
}
