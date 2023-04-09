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
    public partial class CreateUserPage : Form
    {
        private string enteredPass;

        public string EnteredPass
        {
            get { return enteredPass; }
            private set
            {
                enteredPass = value.ToString();
            }
        }
        public CreateUserPage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CreateUser_Load(object sender, EventArgs e)
        {

        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbNewPassword.Text))
            {
                MessageBox.Show("You must enter a master password!", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                EnteredPass = TbNewPassword.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void TbNewPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void cBShow_CheckedChanged(object sender, EventArgs e)
        {
            if (cBShow.Checked)
            {
                TbNewPassword.PasswordChar = '\0';
            }
            else {
                TbNewPassword.PasswordChar = '*';
            }
        }
    }
}
