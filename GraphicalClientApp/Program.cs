namespace ClientApp
{
    internal static class Program
    {
        private static PasswordTool dev = new PasswordTool();
        private static void Logout() {
            dev.LogOut();
            StartUp();
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
                    MessageBox.Show("Wrong Password!");
                }
                else
                {
                    MessageBox.Show("Failiure accoured");
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
                MessageBox.Show("Password tool not found, please connect it via USB and restart the application!");
                Environment.Exit(0);
            }

            StartUp();

            MainPage Client = new MainPage();
            Client.LogoutBtnPushed += Logout;

            Application.Run(Client);


            dev.closeDevice();
        }
    }
}