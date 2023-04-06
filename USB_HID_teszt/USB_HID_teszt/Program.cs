using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace USB_HID_teszt
{
    internal class Program
    {
        private static PasswordTool dev = new PasswordTool();


        static void GetPasswordCount()
        {

            Console.WriteLine("Number of passwords: ");
            Console.Write(dev.GetPassCount());
        }
        static void GetNthEnter()
        {
            Console.WriteLine("Asking for number of enters...");
            Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Number of enters in this password: ");
            Console.Write(dev.GetEnterCount(which));
        }

        static void GetNthTab()
        {
            Console.WriteLine("Asking for number of enters...");
            Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Number of tabs in this password: ");
            Console.Write(dev.GetTabCount(which));
        }


        static void GetNthName()
        {
            Console.WriteLine("Asking for a password name...");
            Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Name of this password: ");
            Console.Write(dev.GetStringFromPass(which, 3));
        }

        static void GetNthPassword()
        {
            Console.WriteLine("Asking for a password..");
            Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("The asked password: ");
            Console.Write(dev.GetStringFromPass(which, 2));
        }

        static void GetNthUsername()
        {
            Console.WriteLine("Asking for a password's username...");
            Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Username of this password: ");
            Console.Write(dev.GetStringFromPass(which, 0));
        }

        static void AddPass()
        {
            Console.WriteLine("Adding a new password...");
            Console.WriteLine("Enter the name of the new password: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter the password: ");
            string password = Console.ReadLine();
            Console.WriteLine("Enter how many tabs you want: ");
            uint tabNum = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Enter how many enters you want: ");
            uint enterNum = Convert.ToUInt32(Console.ReadLine());
            dev.AddEditPassword(0, name, username, password, tabNum, enterNum);
        }

        static void DelPass()
        {

            Console.WriteLine("Deleting...");
            Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(Console.ReadLine());
            dev.DeletePassword(which);
        }

        static void EditPass()
        {
            Console.WriteLine("Editing a password...");
            Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Enter the new name of the password: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the new username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter the new password: ");
            string password = Console.ReadLine();
            Console.WriteLine("Enter how many tabs you want: ");
            uint tabNum = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Enter how many enters you want: ");
            uint enterNum = Convert.ToUInt32(Console.ReadLine());
            dev.AddEditPassword(which, name, username, password, tabNum, enterNum);
        }

        static void EnterPass()
        {

            Console.WriteLine("Entering...");
            Console.WriteLine("Select a password: ");
            uint which = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Select how to write it (0: only password, 1: only username, 2: both): ");
            uint how = Convert.ToUInt32(Console.ReadLine());
            dev.WritePassword(which, how);

        }

        static void CreateUser()
        {
            Console.WriteLine("There are no useres on the device, please enter a master password: ");
            string password = Console.ReadLine();
            dev.SendMasterPassword(password);
        }

        static void LoginUser()
        {
            Console.WriteLine("Please enter the master password: ");
            string password = Console.ReadLine();
            dev.SendMasterPassword(password);
        }
        static void Menu()
        {
            Console.WriteLine("What do you  want? (1: Enetering a password, 2: Adding a password, 3: Editing a password, 4: Deleting a password,");
            Console.WriteLine("5: Getting number of enters, 6: Getting number of tabs, 7: Getting the name of a password, 8: Getting the password,");
            Console.WriteLine("9:  Getting the username of a password, 10: Gettin number of passwords, 11: Logout, 12: DELETE ALL DATA AND STOPS THE PROGRAM)");

            uint action = Convert.ToUInt32(Console.ReadLine());
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
                        Console.WriteLine("Invalid input!");
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
                    Console.WriteLine("Incorrect Password");
                }
                else
                {
                    Console.WriteLine("Failure accoured");
                }
                Thread.Sleep(500);
            }
        }


        static void Main(string[] args)
        {
            dev.openDevice();

            StartUp();

            int maxPC = dev.GetMaxPassCount();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                string[] PassList = new string[dev.GetPassCount()];
                string[] all = dev.ListPassword(PassList);
                int k = 1;
                foreach (string item in all)
                {
                    Console.Write($"{k}. ");
                    Console.WriteLine(item);
                    k++;
                }
                int currentPWs = dev.GetPassCount();
                Console.WriteLine();
                Console.WriteLine($"{currentPWs}/{maxPC} passwords");
                Console.WriteLine();
                Menu();
            }
            dev.closeDevice();
            /*
            Test t=new Test();
            t.Run();
            */
        }


    }
}