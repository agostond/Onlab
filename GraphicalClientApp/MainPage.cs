using Microsoft.VisualBasic.ApplicationServices;
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



    public delegate void BtnPushedDelegate();
    public delegate void BtnNoStaticPushedDelegate(MainPage page);


    public partial class MainPage : Form
    {
        public event BtnNoStaticPushedDelegate LogoutBtnPushed;
        public event BtnPushedDelegate EnterBtnPushed;
        public event BtnNoStaticPushedDelegate AddBtnPushed;
        public event BtnNoStaticPushedDelegate EditBtnPushed;
        public event BtnNoStaticPushedDelegate DeleteBtnPushed;

        private static uint selectedPassId = 0;
        public static uint SelectedPassId {
            get {
                return selectedPassId;
            }
            private set {
                selectedPassId = value;
            }


        }

        public static uint passWriteType = 2;
        public static uint PassWriteType
        {
            get { return passWriteType; }
            private set {
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
        public static void ResetSelectedPassId(){
            selectedPassId = 0;
        }

        public void RefreshPage() {

            LbPasswordList.Items.Clear();

            if (Program.PassList != null)
            {
                foreach (var item in Program.PassList)
                {
                    LbPasswordList.Items.Add(item);
                }
            }
            LbPassCounter.Text = $"{Program.PassCount}/{Program.MaxPassCount} Passwords";
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            LogoutBtnPushed?.Invoke(this);
        }

        private void MainPage_Load(object sender, EventArgs e)
        {

        }

        private void MyClose(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show("bezárulok épp");
        }

        private void LbPasswordList_DoubleClick(object sender, EventArgs e)
        {
            Password SelectedPass = (Password)LbPasswordList.Items[LbPasswordList.SelectedIndex];
            SelectedPassId = SelectedPass.Id;
            if (rBtnEnter.Checked) {
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

        private void LbPasswordList_Click(object sender, EventArgs e)
        {
            if (LbPasswordList.SelectedIndex < 0) {
                return;
            }
              Password SelectedPass = (Password)LbPasswordList.Items[LbPasswordList.SelectedIndex];
              SelectedPassId = SelectedPass.Id;
        }

        private void BtnAddPass_Click(object sender, EventArgs e)
        {

            AddBtnPushed?.Invoke(this);
        }

        private void BtnEnter_Click(object sender, EventArgs e)
        {
            EnterBtnPushed?.Invoke();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            EditBtnPushed?.Invoke(this);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteBtnPushed?.Invoke(this);
        }

        private void rBtnEnteringBoth_Click(object sender, EventArgs e)
        {
            PassWriteType = 2;
        }

        private void rBtnEnteringPassword_CheckedChanged(object sender, EventArgs e)
        {
            PassWriteType = 0;
        }

        private void rBtnEnteringUsername_CheckedChanged(object sender, EventArgs e)
        {
            PassWriteType = 1;
        }
    }
}
