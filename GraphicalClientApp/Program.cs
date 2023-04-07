using System.Xml.Linq;

namespace ClientApp
{

    public class Password
    {
        public string PageName { get; set; }
        public uint Id { get; set; }

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


        private static List<Password> passList = new List<Password>();
        public static List<Password>  PassList { get { return passList; } }
        private static void Logout() {
            dev.LogOut();
            StartUp();
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


        static public void StartUp()
        {
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
                        Environment.Exit(0);
                    }
                }
                else if (action == 2)
                {
                    LoginPage lp = new LoginPage();
                    DialogResult Btn = lp.ShowDialog();
                    if (Btn == DialogResult.OK)
                    {
                        dev.SendMasterPassword(lp.EnteredPass);
                    }
                    else if (Btn == DialogResult.Continue) 
                    {
                        dev.MassDelete();
                        //Environment.Exit(0);
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                else if (action == 3)
                {
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

            StartUp();
            SetPasslist();

            MainPage Client = new MainPage();
            Client.LogoutBtnPushed += Logout;

            Application.Run(Client);


            dev.closeDevice();
        }
    }
}