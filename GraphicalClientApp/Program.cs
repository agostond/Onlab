using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClientApp
{
    internal static class Program
    {
        //used to acces the device
        private static PasswordTool dev = new PasswordTool();

        //variable to store how many record can be added to the device
        public static uint MaxPassCount { get { return (uint)dev.GetMaxPassCount(); } }

        //variable to store how many records are currently in the device
        public static uint PassCount { get { return (uint)dev.GetPassCount(); } }

        //Every records pagename with an ID is listed her.
        private static List<Password> passList = new List<Password>();
        public static List<Password>  PassList { get { return passList; } }


        /**
          * @brief Types the selected password in.
          *
          * @note This is an event handler function.
          *
          */
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

        /**
          * @brief Logs out the device.
          *
          * @note This is an event handler function.
          *
          */
        private static void Logout(MainPage page) {
            dev.LogOut();
            StartUp(page);
        }

        /**
          * @brief Sets the record list up.
          *
          * @note First asks the device for all the records
          * 
          * @note then creates a Password struct from them and adds them to a text box.
          * 
          */
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

        /**
          * @brief If the device is not full pops an Addpasword form up.
          *
          * @note Addpasword form will bes set for adding a new record.
          *
          * @note This is an event handler function.
          *
          */
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

            //After adding a password we must reset the selected ID because that is likely to change
            MainPage.ResetSelectedPassId();
            //Adds the new password into the list
            SetPasslist();
            //Refreshes the form to show every change
            page.RefreshPage();

        }

        /**
          * @brief Pops an Addpasword form up.
          * 
          *  @note The form will be filled with every data of th previous record.
          *
          * @note This is an event handler function.
          *
          */
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

            //After editing a record we must reset the selected ID because that is likely to change
            MainPage.ResetSelectedPassId();
            //Adds the new record to the list and removes the old one.
            SetPasslist();
            //Refreshes the form to show every change
            page.RefreshPage();
        }

        /**
          * @brief Deletes the selected record.
          *
          * @note This is an event handler function.
          *
          */
        static public void DeletePass(MainPage page)
        {
            if (MainPage.SelectedPassId == 0)
            {
                MessageBox.Show("First you must select a password!", "Invalid selection",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Security question about the intended operation  
            else if (MessageBox.Show($"Deleting password: {dev.GetStringFromPass(MainPage.SelectedPassId, 3)}", "Deleting password...", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                dev.DeletePassword(MainPage.SelectedPassId);

                //After deleting a password we must reset the selected ID because that is likely to change
                MainPage.ResetSelectedPassId();
                //removes the selected record from the list
                SetPasslist();
                //Refreshes the form to show every change
                page.RefreshPage();

            }

        }


        /**
          * @brief Function which handles first launch
          *
          * @note The program will stay in this function until a valid authentication is not happening
          *
          */
        static private void StartUp(MainPage page)
        {

            //Getting a random number for device. It's used for encryption
            byte[] random = dev.WaitForRandom();

            while (true)
            {
                //returns the login state of the device
                int action = dev.StartUp();

                //No existing user found, we must create one
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

                //User found we must add the correct master password or delete previous user.
                else if (action == 2)
                {
                    LoginPage lp = new LoginPage();
                    DialogResult Btn = lp.ShowDialog();
                    //Sending a password
                    if (Btn == DialogResult.OK)
                    {
                        dev.SendResponse(lp.EnteredPass, random);
                        Thread.Sleep(200);
                    }
                    //Asking for mass delete
                    else if (Btn == DialogResult.Continue) 
                    {
                        PassList.Clear();
                        dev.MassDelete();
                        dev.closeDevice();
                        //if we delete every data we must shot down the application
                        Environment.Exit(0);
                    }
                    else
                    {
                        dev.closeDevice();
                        Environment.Exit(0);
                    }
                }

                //User already logged in, application can start normally
                else if (action == 3)
                {
                    page.RefreshPage();
                    return;
                }

                //Error recieving for wrong passwords
                else if (action == 4)
                {
                    MessageBox.Show("Wrong Password!", "Invalid password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //Communication error, invalid messange arrived
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

            //cheks if device is connected
            if (!dev.openDevice()) {
                MessageBox.Show("Password tool not found, please connect it via USB and restart the application!", "Communication Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            //creates the main UI
            MainPage Client = new MainPage();

            //First set up
            StartUp(Client);
            SetPasslist();
            Client.RefreshPage();


            //Subscribe to mainform events
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