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



    public delegate void LogoutBtnPushedDelegate();



    public partial class MainPage : Form
    {
        public event LogoutBtnPushedDelegate LogoutBtnPushed;
        public MainPage()
        {
            InitializeComponent();
            if (Program.PassList != null)
            {
                foreach (var item in Program.PassList)
                {
                    LbPasswordList.Items.Add(item);
                }
            }

        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            LogoutBtnPushed?.Invoke();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void MyClose(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show("bezárulok épp");
        }

        private void LbPasswordList_DoubleClick(object sender, EventArgs e)
        {
            Password SelectedPass = (Password)LbPasswordList.Items[LbPasswordList.SelectedIndex];
            MessageBox.Show(SelectedPass.Id.ToString());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
