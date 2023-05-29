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
    public partial class AddPassword : Form
    {

        private string newPass = "";   //string which stores the password entered by the user
        private string newName = "";   //string which stores the page name entered by the user
        private string newUser = "";   //string which stores the username entered by the user
        private uint newTabNum;   //stores how many tabs the user wants between an username and a password
        private uint newEnterNum; //stores how many enters the user wants after the password

        public string NewPass
        {
            get { return newPass; }
            private set
            {
                if (value != null)
                {
                    newPass = value;
                }
            }

        }

        public string NewName
        {
            get { return newName; }
            private set
            {
                if (value != null)
                {
                    newName = value;
                }
            }

        }

        public string NewUser
        {
            get { return newUser; }
            private set
            {
                if (value != null)
                {
                    newUser = value;
                }
            }

        }

        public uint NewTabNum
        {
            get { return newTabNum; }
            private set { newTabNum = value; }
        }

        public uint NewEnterNum
        {
            get { return newEnterNum; }
            private set { newEnterNum = value; }
        }
        public AddPassword()
        {
            InitializeComponent();
        }

        /**
          * @brief Sets up the form with user data.
          *
          * @param btnName: Here you can change the buttons text.
          * 
          * @param olPass: Default text of the password textbox.
          * 
          * @param oldName: Default text of the pagename textbox.
          * 
          * @param oldUser: Default text of the username textbox.
          * 
          * @param oldTabNum: Sets the default value for tabNum numeric filed.
          * 
          * @param oldEnterNum: Sets the default value for enterNum numeric filed.
          */
        public void Init(string btnName, string oldPass = "", string oldName = "", string oldUser = "", uint oldTabNum = 1, uint oldEnterNum = 1)
        {

            if (btnName != null)
            {
                BtnSend.Text = btnName;
            }

            if (oldPass != null)
            {
                TbNewPassword.Text = oldPass;
            }

            if (oldName != null)
            {
                TbNewPageName.Text = oldName;
            }

            if (oldUser != null)
            {
                TbNewUsername.Text = oldUser;
            }

            nUDEnter.Value = oldEnterNum;
            nUDTab.Value = oldTabNum;
        }

        /**
          * @brief Checks every entered data, if everything is OK, then sets the variables.
          *
          * @note This is an evenet handler for send button pressed.
          * 
          * @note If everything is succes sends a dialog result OK
          *
          */
        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbNewPassword.Text))
            {
                MessageBox.Show("You must enter a password!", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(TbNewUsername.Text))
            {
                MessageBox.Show("You must enter a username!", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(TbNewPageName.Text))
            {
                MessageBox.Show("You must enter a page name!", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                NewTabNum = (uint)nUDTab.Value;
                NewEnterNum = (uint)nUDEnter.Value;
                NewPass = TbNewPassword.Text;
                NewName = TbNewPageName.Text;
                NewUser = TbNewUsername.Text;
                this.DialogResult = DialogResult.OK;
            }


        }

        /**
          * @brief Sends a dialog result cancel
          *
          */
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
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
