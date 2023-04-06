using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using USB_HID_teszt;


namespace USB_HID_teszt
{ 
    internal class PasswordTool
    {

        private const int AuthenticationFail = 5;
        private const int AuthenticationSucces = 10;
        private const int create = 0xAA;
        private const int authenticate = 0xBB;
        private const int authenticated = 0xCC;
        private const int notAuthenticated = 0xDD;
        private const int validate = 0xFF;
        private const int id = 0xbe;

        private const int messageSize = 65;
        private const int CheckSumPlace = 64;

        private const int SEND_USERNAME = 0;
        private const int SEND_PASS_COUNT = 1;
        private const int SEND_PASS = 2;
        private const int SEND_PASS_NAME = 3;
        private const int ENTER_PASS = 4;
        private const int ADDING_PASS = 5;
        private const int EDIT_PASS = 6;
        private const int DEL_PASS = 7;
        private const int SEND_TAB_NUM = 8;
        private const int SEND_ENTER_NUM = 9;
        private const int SEND_MAX_PASS_COUNT = 10;
        private const int LOGOUT = 11;
        private const int SEND_STATUS = 12;
        private const int DELETE_ALL = 13;


        private uint checkSum = 0;


        UsbHid usbhid = new UsbHid();

        public void openDevice() {
            usbhid.OpenDevice(0x0483, 0x5750); //device VID and PID
        }

        public void closeDevice() {
            usbhid.CloseDevice();
        }

        public int StartUp()
        {

            int Status = GetStatus();

            if (Status == create)
            {
                return 1;
            }
            else if (Status == authenticate)
            {
                return 2;
            }
            else if (Status == authenticated || Status == AuthenticationSucces) {
                return 3;
            }
            else if (Status == AuthenticationFail)
            {
                return 4;
            }
            return 0;
        }

        
        private byte[] ReadWrite(byte[] message)
        {
            checkSum++;
            byte[] SendMessage = new byte[messageSize];
            message.CopyTo(SendMessage, 0);
            if (checkSum > 255) {
                checkSum = 0;
            }
            SendMessage[CheckSumPlace] = (byte)checkSum;
            usbhid.WriteFeature(SendMessage);
            Thread.Sleep(50);
            var time = DateTime.UtcNow;
            while ((DateTime.UtcNow - time).TotalSeconds < 1)
            {
                byte[] Answer = usbhid.ReadFeature();
                Thread.Sleep(30);
                if (!Answer.All(x => x == 0)) {
                    if (checkSum == Answer[CheckSumPlace]) {
                        return Answer;
                    }
                }
            }
            return null;


        }

        public int GetMaxPassCount() {

            byte[] MaxPassCount = ReadWrite(new byte[] { id, SEND_MAX_PASS_COUNT });
            return MaxPassCount[1];
        }

        public int GetPassCount()
        {
            byte[] PassCount = ReadWrite(new byte[] { id, SEND_PASS_COUNT });
            return PassCount[1];
        }

        public void MassDelete() {

            usbhid.WriteFeature(new byte[] { id, DELETE_ALL });
            Thread.Sleep(500);
        }

        public void SendMasterPassword(string masterPassword)
        {
            byte[] bMasterPassword = Encoding.ASCII.GetBytes(masterPassword);
            bMasterPassword = Combine(new byte[] {id, (byte)validate}, bMasterPassword);
            bMasterPassword = Combine(bMasterPassword, new byte[] { (byte)'\0' });
            usbhid.WriteFeature(bMasterPassword);
        }

        private int GetStatus() {
            byte[] Status = ReadWrite(new byte[] { id, SEND_STATUS });
            return Status[1];
        }

        public void LogOut() {
            usbhid.WriteFeature(new byte[] { id, LOGOUT });
        }

        private bool CheckValidPassNum(uint which) {
            int PassCount = GetPassCount();

            if (which > PassCount || which == 0)
            {
                throw new ArgumentException();
            }
            return true;

        }

        public int GetEnterCount(uint which)
        {
            if (CheckValidPassNum(which))
            {
                byte[] EnterCount = ReadWrite(new byte[] { id, SEND_ENTER_NUM, (byte)(which - 1) });
                return EnterCount[1];

            }

            return 0;
            
        }

        public int GetTabCount(uint which)
        {
            if (CheckValidPassNum(which))
            {
                byte[] TabCount = ReadWrite(new byte[] { id, SEND_TAB_NUM, (byte)(which - 1) });
                return TabCount[1];

            }

            return 0;

        }

        public char[] GetStringFromPass(uint which, uint what)
        {
            char[] PassString = new char[64];

            if (what != SEND_PASS && what != SEND_USERNAME && what != SEND_PASS_NAME) {
                throw new ArgumentException();
            }


            if (CheckValidPassNum(which))
            {
                int k;
                byte[] PassNameByte = ReadWrite(new byte[] { id, (byte)what, (byte)(which - 1) });
                PassNameByte[65] = (byte)'\0';
                for (k = 1; PassNameByte[k] != 0; k++)
                {
                    char value = (char)PassNameByte[k];
                    PassString[k - 1] = value;
                }
                PassString[k - 1] = '\0';

            }

            return PassString;
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
        public string[] ListPassword(string[] PassList) {

            int PassCount = GetPassCount();
            int k;

 

            for (int i = 0; i < PassCount; i++) {

                byte[] PassNameByte = ReadWrite(new byte[] { id, SEND_PASS_NAME, (byte)i });
                char[] PassNameChar = new char[64];
                PassNameChar[63] = '\0';

                for (k = 1; PassNameByte[k] != 0; k++) {
                    char value = (char)PassNameByte[k];
                    PassNameChar[k-1] =  value;
                }
                PassNameChar[k - 1] = '\0';
                PassList[i] = new string(PassNameChar);
            }

            return PassList;
        }

        public void WritePassword(uint which,uint how){


            if (CheckValidPassNum(which))
            {

                if (how > 2)
                {
                    throw new ArgumentException();
                }
                else
                {
                    usbhid.WriteFeature(new byte[] { id, ENTER_PASS, (byte)(which - 1), (byte)how });
                }
                Thread.Sleep(3000);
            }
        }

        public void DeletePassword(uint which) {

            if (CheckValidPassNum(which)){

                usbhid.WriteFeature(new byte[] { id, DEL_PASS, (byte)(which - 1)});
            }
        }

        public void AddEditPassword(uint which, string name, string username, string password, uint tabNum, uint enterNum) {

            if ((GetPassCount() + 1) > GetMaxPassCount() && which == 0) { 
                throw new Exception("Device is full");
            }

            byte[] bName = Encoding.ASCII.GetBytes(name);
            bName = Combine(bName, new byte[] { (byte)'\0' });
            byte[] bUsername = Encoding.ASCII.GetBytes(username);
            bUsername = Combine(bUsername, new byte[] { (byte)'\0' });
            byte[] bPassword = Encoding.ASCII.GetBytes(password);
            bPassword = Combine(bPassword, new byte[] { (byte)'\0' });

            byte[] firstRep;
            byte[] secondRep;
            byte[] thridRep;

            if (which == 0)
            {
                firstRep = Combine(new byte[] { id, ADDING_PASS, 0, (byte)enterNum, (byte)tabNum }, bName);
                secondRep = Combine(new byte[] { id, ADDING_PASS }, bPassword);
                thridRep = Combine(new byte[] { id, ADDING_PASS }, bUsername);
            }

            else if (CheckValidPassNum(which))
            {
                firstRep = Combine(new byte[] { id, EDIT_PASS, (byte)(which-1), (byte)enterNum, (byte)tabNum }, bName);
                secondRep = Combine(new byte[] { id, EDIT_PASS }, bPassword);
                thridRep = Combine(new byte[] { id, EDIT_PASS }, bUsername);
            }
            else {
                return;
            }

            usbhid.WriteFeature(firstRep);
            Thread.Sleep(150);
            usbhid.WriteFeature(secondRep);
            Thread.Sleep(150);
            usbhid.WriteFeature(thridRep);
            Thread.Sleep(150);

        }

    }
}
