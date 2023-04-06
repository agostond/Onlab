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
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            LogoutBtnPushed?.Invoke();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {

        }
    }
}
