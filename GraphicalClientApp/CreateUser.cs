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
        private string enteredPass = ""; //string which stores the master password entered by the user

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

        /**
          * @brief If there is text in the password textbox, stores it in the private string
          *
          * @note This is an evenet handler for send button pressed.
          *
          */
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

        /**
          * @brief Sets the visibility of the password storing text box.
          *
          * @note This is an evenet handler for a chekbox change
          *
          * @note data: default option: not visible (every charcter is replaced with a "*")
          */
        private void cBShow_CheckedChanged(object sender, EventArgs e)
        {
            if (cBShow.Checked)
            {
                TbNewPassword.PasswordChar = '\0';
            }
            else
            {
                TbNewPassword.PasswordChar = '*';
            }
        }
    }
}
