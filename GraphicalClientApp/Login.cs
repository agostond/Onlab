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
        private string enteredPass = "";

        public string EnteredPass //string which stores the master password entered by the user
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

        /**
          * @brief If there is text in the password textbox, stores it in the private string
          *
          * @note This is an evenet handler for send button pressed.
          * 
          * @note If everything is succes sends a dialog result OK
          *
          */
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

        /**
          * @brief Delets every data from the device
          *
          * @note This is an evenet handler for mass delete button pressed.
          * 
          */
        private void BtnMassDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Deleting all data...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Continue;
            }
        }


        /**
          * @brief Sets the visibility of the password storing text box.
          *
          * @note This is an evenet handler for a chekbox change
          *
          * @note data: default option: not visible (every charcter is replaced with a "*")
          */
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cBShow.Checked)
            {
                TbPassword.PasswordChar = '\0';
            }
            else
            {
                TbPassword.PasswordChar = '*';
            }
        }
    }
}
