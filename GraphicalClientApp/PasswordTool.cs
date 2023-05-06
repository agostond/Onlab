using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClientApp;


namespace ClientApp
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

        public bool openDevice() {

            return usbhid.OpenDevice(0x0483, 0x5750); //device VID and PID
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
            throw new Exception("Communication fail");


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
            byte[] bMasterPassword = GetStringBytes(masterPassword);
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

        public uint GetEnterCount(uint which)
        {
            if (CheckValidPassNum(which))
            {
                byte[] EnterCount = ReadWrite(new byte[] { id, SEND_ENTER_NUM, (byte)(which - 1) });
                return EnterCount[1];

            }

            return 0;
            
        }

        public uint GetTabCount(uint which)
        {
            if (CheckValidPassNum(which))
            {
                byte[] TabCount = ReadWrite(new byte[] { id, SEND_TAB_NUM, (byte)(which - 1) });
                return TabCount[1];

            }

            return 0;

        }

        public string GetStringFromPass(uint which, uint what)
        {
            byte[] PassString = new byte[64];

            if (what != SEND_PASS && what != SEND_USERNAME && what != SEND_PASS_NAME) {
                throw new ArgumentException();
            }


            if (CheckValidPassNum(which))
            {
                int k;
                byte[] PassNameByte = ReadWrite(new byte[] { id, (byte)what, (byte)(which - 1) });
                PassNameByte[messageSize - 1] = (byte)'\0';
                
                for (k = 1; PassNameByte[k] != 0; k++)
                {
                    PassString[k - 1] = PassNameByte[k];
                }
                PassString[k - 1] = (byte)'\0';
                
            }

            string ret = ByteToUtf(PassString);
            return ret;
            
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
            
                
                byte[] PassNameChar = new byte[64];
                PassNameChar[63] = (byte)'\0';

                for (k = 1; PassNameByte[k] != 0; k++) {
                    PassNameChar[k-1] = PassNameByte[k];
                }
                PassNameChar[k - 1] = (byte)'\0';
               
                PassList[i] = ByteToUtf(PassNameChar);
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
                Thread.Sleep(1000);
            }
        }

        public void DeletePassword(uint which) {

            if (CheckValidPassNum(which)){

                usbhid.WriteFeature(new byte[] { id, DEL_PASS, (byte)(which - 1)});
            }
        }

        static string ByteToUtf(byte[] element)
        {
            string ret;
            int i = 0;
            int k = 0;
            byte[] utfbytes = new byte[(element.Length * 2)];
            for (i = 0; i < element.Length-1; i++, k++)
            {
                if (element[k] == 0) {
                    utfbytes[i] = element[k];
                    break;
                }
                else if (element[k] == 0xF5)
                {
                    utfbytes[i] = 0xC5;
                    i++;
                    utfbytes[i] = 0x91;
                }

                else if (element[k] == 0xD5)
                {
                    utfbytes[i] = 0xC5;
                    i++;
                    utfbytes[i] = 0x90;
                }

                else if (element[k] == 0xfb)
                {
                    utfbytes[i] = 0xC5;
                    i++;
                    utfbytes[i] = 0xb1;
                }

                else if (element[k] == 0xdb)
                {
                    utfbytes[i] = 0xC5;
                    i++;
                    utfbytes[i] = 0xb0;
                }

                else
                {
                    if (element[k] > 127)
                    {
                        utfbytes[i] = 195; //C3
                        i++;
                    }
                    utfbytes[i] = element[k];
                }
            }
            ret = Encoding.UTF8.GetString(utfbytes);
            return ret;
        }

        static byte[] GetStringBytes(string s)
        {
            List<byte> bytes= new List<byte>();
            for (int i=0;i<s.Length;i++)
            {
                var ss = s[i];
                var bb = Encoding.UTF8.GetBytes(ss.ToString());
                
                if (s[i] == 'ő')
                {
                    bb[1] = 0xF5;
                }
                if (s[i] == 'Ő')
                {
                    bb[1] = 0xD5;
                }
                if (s[i] == 'ű')
                {
                    bb[1] = 0xFB;
                }
                if (s[i] == 'Ű')
                {
                    bb[1] = 0xDB;
                }
                
                bytes.Add(bb.Last());
            }
            bytes.Add((byte)0);
            return bytes.ToArray();
        }

        public void AddEditPassword(uint which, string name, string username, string password, uint tabNum, uint enterNum) {

            if ((GetPassCount() + 1) > GetMaxPassCount() && which == 0) { 
                throw new Exception("Device is full");
            }

            
            byte[] bName = GetStringBytes(name);// Encoding.GetEncoding("ISO-8859-2").GetBytes(name);
            bName = Combine(bName, new byte[] { (byte)'\0' });
            byte[] bUsername = GetStringBytes(username);//Encoding.GetEncoding("iso-8859-2").GetBytes(username);
            bUsername = Combine(bUsername, new byte[] { (byte)'\0' });
            byte[] bPassword = GetStringBytes(password);// Encoding.GetEncoding("iso-8859-2").GetBytes(password);
            bPassword = Combine(bPassword, new byte[] { (byte)'\0' });
            

            /*
            byte[] bName = Encoding.ASCII.GetBytes(name);
            bName = Combine(bName, new byte[] { (byte)'\0' });
            byte[] bUsername = Encoding.ASCII.GetBytes(username);
            bUsername = Combine(bUsername, new byte[] { (byte)'\0' });
            byte[] bPassword = Encoding.ASCII.GetBytes(password);
            bPassword = Combine(bPassword, new byte[] { (byte)'\0' });
            */

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
