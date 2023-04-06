using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USB_HID_teszt
{
    internal class Console
    {

        private static PasswordTool dev = new PasswordTool();


        public static void ConsoleInterface() {
            dev.openDevice();

            StartUp();

            int maxPC = dev.GetMaxPassCount();

            while (true)
            {
                System.Console.WriteLine();
                System.Console.WriteLine();
                System.Console.WriteLine();
                string[] PassList = new string[dev.GetPassCount()];
                string[] all = dev.ListPassword(PassList);
                int k = 1;
                foreach (string item in all)
                {
                    System.Console.Write($"{k}. ");
                    System.Console.WriteLine(item);
                    k++;
                }
                int currentPWs = dev.GetPassCount();
                System.Console.WriteLine();
                System.Console.WriteLine($"{currentPWs}/{maxPC} passwords");
                System.Console.WriteLine();
                Menu();
            }
            dev.closeDevice();
            /*
            Test t=new Test();
            t.Run();
            */
        }


        static void GetPasswordCount()
        {

            System.Console.WriteLine("Number of passwords: ");
            System.Console.Write(dev.GetPassCount());
        }
        static void GetNthEnter()
        {
            System.Console.WriteLine("Asking for number of enters...");
            System.Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(System.Console.ReadLine());
            System.Console.WriteLine("Number of enters in this password: ");
            System.Console.Write(dev.GetEnterCount(which));
        }

        static void GetNthTab()
        {
            System.Console.WriteLine("Asking for number of enters...");
            System.Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(System.Console.ReadLine());
            System.Console.WriteLine("Number of tabs in this password: ");
            System.Console.Write(dev.GetTabCount(which));
        }


        static void GetNthName()
        {
            System.Console.WriteLine("Asking for a password name...");
            System.Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(System.Console.ReadLine());
            System.Console.WriteLine("Name of this password: ");
            System.Console.Write(dev.GetStringFromPass(which, 3));
        }

        static void GetNthPassword()
        {
            System.Console.WriteLine("Asking for a password..");
            System.Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(System.Console.ReadLine());
            System.Console.WriteLine("The asked password: ");
            System.Console.Write(dev.GetStringFromPass(which, 2));
        }

        static void GetNthUsername()
        {
            System.Console.WriteLine("Asking for a password's username...");
            System.Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(System.Console.ReadLine());
            System.Console.WriteLine("Username of this password: ");
            System.Console.Write(dev.GetStringFromPass(which, 0));
        }

        static void AddPass()
        {
            System.Console.WriteLine("Adding a new password...");
            System.Console.WriteLine("Enter the name of the new password: ");
            string name = System.Console.ReadLine();
            System.Console.WriteLine("Enter the username: ");
            string username = System.Console.ReadLine();
            System.Console.WriteLine("Enter the password: ");
            string password = System.Console.ReadLine();
            System.Console.WriteLine("Enter how many tabs you want: ");
            uint tabNum = Convert.ToUInt32(System.Console.ReadLine());
            System.Console.WriteLine("Enter how many enters you want: ");
            uint enterNum = Convert.ToUInt32(System.Console.ReadLine());
            dev.AddEditPassword(0, name, username, password, tabNum, enterNum);
        }

        static void DelPass()
        {

            System.Console.WriteLine("Deleting...");
            System.Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(System.Console.ReadLine());
            dev.DeletePassword(which);
        }

        static void EditPass()
        {
            System.Console.WriteLine("Editing a password...");
            System.Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(System.Console.ReadLine());
            System.Console.WriteLine("Enter the new name of the password: ");
            string name = System.Console.ReadLine();
            System.Console.WriteLine("Enter the new username: ");
            string username = System.Console.ReadLine();
            System.Console.WriteLine("Enter the new password: ");
            string password = System.Console.ReadLine();
            System.Console.WriteLine("Enter how many tabs you want: ");
            uint tabNum = Convert.ToUInt32(System.Console.ReadLine());
            System.Console.WriteLine("Enter how many enters you want: ");
            uint enterNum = Convert.ToUInt32(System.Console.ReadLine());
            dev.AddEditPassword(which, name, username, password, tabNum, enterNum);
        }

        static void EnterPass()
        {

            System.Console.WriteLine("Entering...");
            System.Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(System.Console.ReadLine());
            System.Console.WriteLine("Select how to write it (0: only password, 1: only username, 2: both): ");
            uint how = Convert.ToUInt32(System.Console.ReadLine());
            dev.WritePassword(which, how);

        }

        static void CreateUser()
        {
            System.Console.WriteLine("There are no useres on the device, please enter a master password: ");
            string password = System.Console.ReadLine();
            dev.SendMasterPassword(password);
        }

        static void LoginUser()
        {
            System.Console.WriteLine("Please enter the master password: ");
            string password = System.Console.ReadLine();
            dev.SendMasterPassword(password);
        }
        static void Menu()
        {
            System.Console.WriteLine("What do you  want? (1: Enetering a password, 2: Adding a password, 3: Editing a password, 4: Deleting a password,");
            System.Console.WriteLine("5: Getting number of enters, 6: Getting number of tabs, 7: Getting the name of a password, 8: Getting the password,");
            System.Console.WriteLine("9:  Getting the username of a password, 10: Gettin number of passwords, 11: Logout, 12: DELETE ALL DATA AND STOPS THE PROGRAM)");

            uint action = Convert.ToUInt32(System.Console.ReadLine());
            switch (action)
            {
                case 1:
                    {

                        EnterPass();
                        break;
                    }

                case 2:
                    {

                        AddPass();
                        break;
                    }

                case 3:
                    {

                        EditPass();
                        break;
                    }

                case 4:
                    {

                        DelPass();
                        break;
                    }
                case 5:
                    {
                        GetNthEnter();
                        break;
                    }
                case 6:
                    {
                        GetNthTab();
                        break;
                    }
                case 7:
                    {
                        GetNthName();
                        break;
                    }
                case 8:
                    {
                        GetNthPassword();
                        break;
                    }
                case 9:
                    {
                        GetNthUsername();
                        break;
                    }
                case 10:
                    {
                        GetPasswordCount();
                        break;
                    }
                case 11:
                    {
                        dev.LogOut();
                        StartUp();
                        break;
                    }
                case 12:
                    {
                        dev.MassDelete();
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        System.Console.WriteLine("Invalid input!");
                        break;
                    }

            }
        }
        static public void StartUp()
        {
            while (true)
            {
                int action = dev.StartUp();

                if (action == 1)
                {
                    CreateUser();
                }
                else if (action == 2)
                {
                    LoginUser();
                }
                else if (action == 3)
                {
                    return;
                }
                else if (action == 4)
                {
                    System.Console.WriteLine("Incorrect Password");
                }
                else
                {
                    System.Console.WriteLine("Failure accoured");
                }
                Thread.Sleep(500);
            }
        }
    }

}
