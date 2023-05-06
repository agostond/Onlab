using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClientApp
{

    public class Password
    {
        public string PageName { get; private set; }
        public uint Id { get; private set; }

        public Password(string pageName, uint id)
        {
            if(pageName== null) throw new ArgumentNullException("You must add a name to the password!");
            PageName = pageName;
            Id = id;
        }

        public override string ToString()
        {
            return PageName;
        }

    }
    internal static class Program
    {
        private static PasswordTool dev = new PasswordTool();

        //private static MainPage Client = new MainPage();

        public static uint MaxPassCount { get { return (uint)dev.GetMaxPassCount(); } }

        public static uint PassCount { get { return (uint)dev.GetPassCount(); } }

        private static List<Password> passList = new List<Password>();
        public static List<Password>  PassList { get { return passList; } }


        public static void Enterpass() {
            if (MainPage.SelectedPassId == 0)
            {
                MessageBox.Show("First you must select a password!", "Invalid selection",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else {
                string pass = dev.GetStringFromPass(MainPage.SelectedPassId, 3);
                if (MessageBox.Show("Entering password in 1 second: " + pass, "Success!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK) {
                    Thread.Sleep(500);
                    dev.WritePassword(MainPage.SelectedPassId, MainPage.PassWriteType);
                }
            }
        }
        private static void Logout(MainPage page) {
            dev.LogOut();
            StartUp(page);
        }

        private static void SetPasslist() {
            if (PassList != null)
            {
                PassList.Clear();
            }
            string[] PasswordsNameList = new string[dev.GetPassCount()];
            PasswordsNameList = dev.ListPassword(PasswordsNameList);
            uint k = 1;
            foreach (string item in PasswordsNameList)
            {
                Password ps = new Password(item, k);
                passList.Add(ps);
                k++;
            }
        }


        static private void AddNewPass(MainPage page) {
            if (PassCount + 1 > MaxPassCount) {
                MessageBox.Show("You can't add a new password because your device is full, please delete unused passwords", "Device is full",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddPassword ap = new AddPassword();
            ap.Init("Create!");
            DialogResult Btn = ap.ShowDialog();
            if (Btn == DialogResult.OK) {
                dev.AddEditPassword(0, ap.NewName, ap.NewUser, ap.NewPass, ap.NewTabNum, ap.NewEnterNum);
            }

            MainPage.ResetSelectedPassId();
            SetPasslist();
            page.RefreshPage();

        }

        static public void EditPass(MainPage page)
        {
            if (MainPage.SelectedPassId == 0)
            {
                MessageBox.Show("First you must select a password!", "Invalid selection",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddPassword ap = new AddPassword();
            ap.Init("Modify!", dev.GetStringFromPass(MainPage.SelectedPassId, 2), dev.GetStringFromPass(MainPage.SelectedPassId, 3), dev.GetStringFromPass(MainPage.SelectedPassId, 0), dev.GetTabCount(MainPage.SelectedPassId), dev.GetEnterCount(MainPage.SelectedPassId));
            DialogResult Btn = ap.ShowDialog();
            if (Btn == DialogResult.OK)
            {
                dev.AddEditPassword(MainPage.SelectedPassId, ap.NewName, ap.NewUser, ap.NewPass, ap.NewTabNum, ap.NewEnterNum);
            }

            MainPage.ResetSelectedPassId();
            SetPasslist();
            page.RefreshPage();
        }

        static public void DeletePass(MainPage page)
        {
            if (MainPage.SelectedPassId == 0)
            {
                MessageBox.Show("First you must select a password!", "Invalid selection",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (MessageBox.Show($"Deleting password: {dev.GetStringFromPass(MainPage.SelectedPassId, 3)}", "Deleting password...", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                dev.DeletePassword(MainPage.SelectedPassId);

                MainPage.ResetSelectedPassId();
                SetPasslist();
                page.RefreshPage();

            }

        }

        static private void StartUp(MainPage page)
        {


            byte[] random = dev.WaitForRandom();

            while (true)
            {
                int action = dev.StartUp();

                if (action == 1)
                {
                    CreateUserPage lp = new CreateUserPage();
                    if (lp.ShowDialog() == DialogResult.OK)
                    {
                        dev.SendMasterPassword(lp.EnteredPass);
                    }
                    else
                    {
                        dev.closeDevice();
                        Environment.Exit(0);
                    }
                }
                else if (action == 2)
                {
                    LoginPage lp = new LoginPage();
                    DialogResult Btn = lp.ShowDialog();
                    if (Btn == DialogResult.OK)
                    {
                        dev.SendResponse(lp.EnteredPass, random);
                        Thread.Sleep(200);
                    }
                    else if (Btn == DialogResult.Continue) 
                    {
                        PassList.Clear();
                        dev.MassDelete();
                        dev.closeDevice();
                        Environment.Exit(0);
                    }
                    else
                    {
                        dev.closeDevice();
                        Environment.Exit(0);
                    }
                }
                else if (action == 3)
                {
                    page.RefreshPage();
                    return;
                }
                else if (action == 4)
                {
                    MessageBox.Show("Wrong Password!", "Invalid password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Failure occurred", "Communication Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dev.closeDevice();
                    Environment.Exit(0);
                }

                Thread.Sleep(200);
            }
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            if (!dev.openDevice()) {
                MessageBox.Show("Password tool not found, please connect it via USB and restart the application!", "Communication Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            MainPage Client = new MainPage();

            StartUp(Client);
            SetPasslist();
            Client.RefreshPage();

            Client.LogoutBtnPushed += Logout;
            Client.EnterBtnPushed += Enterpass;
            Client.AddBtnPushed += AddNewPass;
            Client.EditBtnPushed += EditPass;
            Client.DeleteBtnPushed += DeletePass;

            Application.Run(Client);


            dev.closeDevice();
        }
    }
}