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

        private string newPass;
        private string newName;
        private string newUser;
        private uint newTabNum;
        private uint newEnterNum;

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

        public void Init(string btnName, string oldPass = null, string oldName = null, string oldUser = null, uint oldTabNum = 1, uint oldEnterNum = 1)
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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

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

        private void TbNewPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
