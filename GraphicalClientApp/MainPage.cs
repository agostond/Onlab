using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{



    public delegate void BtnPushedDelegate();
    public delegate void BtnNoStaticPushedDelegate(MainPage page);


    public partial class MainPage : Form
    {
        /*
         *Events for every button
         */
        public event BtnNoStaticPushedDelegate? LogoutBtnPushed;
        public event BtnPushedDelegate? EnterBtnPushed;
        public event BtnNoStaticPushedDelegate? AddBtnPushed;
        public event BtnNoStaticPushedDelegate? EditBtnPushed;
        public event BtnNoStaticPushedDelegate? DeleteBtnPushed;

        private static uint selectedPassId = 0; //stores which record is selected by user
        public static uint SelectedPassId
        {
            get
            {
                return selectedPassId;
            }
            private set
            {
                selectedPassId = value;
            }


        }

        public static uint passWriteType = 2; //selects the typing type. 2 = username and password
                                              //                         0 = only password
                                              //                         1 = only username
        public static uint PassWriteType
        {
            get { return passWriteType; }
            private set
            {
                if (value < 3)
                {
                    passWriteType = value;
                }
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        /**
          * @brief Sets an invalid record selection.
          *
          */
        public static void ResetSelectedPassId()
        {
            selectedPassId = 0;
        }


        /**
          * @brief Sets up the record list.
          *
          */
        public void RefreshPage()
        {

            LbPasswordList.Items.Clear();

            if (Program.PassList != null)
            {
                foreach (var item in Program.PassList)
                {
                    LbPasswordList.Items.Add(item);
                }
            }

            //Sets a text which shows current record count/max recordcount
            LbPassCounter.Text = $"{Program.PassCount}/{Program.MaxPassCount} Passwords";
        }


        /**
          * @brief Logout event.
          *
          */
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            LogoutBtnPushed?.Invoke(this);
        }


        /**
          * @brief user can select a default double click action, this function handles that.
          *
          * @note If no action selected this function does nothing.
          *
          */
        private void LbPasswordList_DoubleClick(object sender, EventArgs e)
        {
            Password SelectedPass = (Password)LbPasswordList.Items[LbPasswordList.SelectedIndex];
            SelectedPassId = SelectedPass.Id;
            if (rBtnEnter.Checked)
            {
                Program.Enterpass();
            }
            if (rBtnEdit.Checked)
            {
                Program.EditPass(this);
            }
            if (rBtnDelete.Checked)
            {
                Program.DeletePass(this);
            }
        }


        /**
          * @brief User can select a record by clicking on it and this function handles that.
          *
          */
        private void LbPasswordList_Click(object sender, EventArgs e)
        {
            if (LbPasswordList.SelectedIndex < 0)
            {
                return;
            }
            Password SelectedPass = (Password)LbPasswordList.Items[LbPasswordList.SelectedIndex];
            SelectedPassId = SelectedPass.Id;
        }

        /**
          * @brief Record adding button event.
          *
          */
        private void BtnAddPass_Click(object sender, EventArgs e)
        {

            AddBtnPushed?.Invoke(this);
        }


        /**
          * @brief Record typing button event.
          *
          */
        private void BtnEnter_Click(object sender, EventArgs e)
        {
            EnterBtnPushed?.Invoke();
        }


        /**
          * @brief Record editing button event.
          *
          */
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            EditBtnPushed?.Invoke(this);
        }


        /**
          * @brief Record deleting button event
          *
          */
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteBtnPushed?.Invoke(this);
        }


        /**
          * @brief Record typing mode setter event.
          *
          * @note Typing both username and password.
          *
          */
        private void rBtnEnteringBoth_Click(object sender, EventArgs e)
        {
            PassWriteType = 2;
        }

        /**
          * @brief Record typing mode setter event.
          *
          * @note Typing only password.
          *
          */
        private void rBtnEnteringPassword_CheckedChanged(object sender, EventArgs e)
        {
            PassWriteType = 0;
        }

        /**
          * @brief Record typing mode setter event.
          *
          * @note Typing only username.
          *
          */
        private void rBtnEnteringUsername_CheckedChanged(object sender, EventArgs e)
        {
            PassWriteType = 1;
        }
    }
}
