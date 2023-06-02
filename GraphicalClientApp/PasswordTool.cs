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
using System.Security.Cryptography;

namespace ClientApp

{
    internal class PasswordTool
    {
        /*
         *Possible answers in startup
         */
        private const int AuthenticationFail = 5;
        private const int AuthenticationSucces = 10;
        private const int create = 0xAA;
        private const int authenticate = 0xBB;
        private const int authenticated = 0xCC;


        private const int validate = 0xFF;

        /*
         *ID to feature report communication, this must be the first byte of the meassage
         */
        private const int id = 0xbe;

        /*
         * Constants for communication, Cheksum is used for synchronising messages to answers, ChecksumPlace defines which byte is it
         */
        private const int messageSize = 65;
        private const int CheckSumPlace = 64;

        /*
         * Commands, these are the second byte of the message
         */
        private const int RECEIVERAND = 0xAB;
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


        //Cheksum is used for synchronising messages to answers
        private uint checkSum = 0;

        //Used for USB communication
        UsbHid usbhid = new UsbHid();

        /**
          * @brief Opens the password tool device.
          *
          * @note It finds the device via VID and PID.
          *
          * @retval Succes or fail.
          *
          */
        public bool openDevice() {

            return usbhid.OpenDevice(0x0483, 0x5750); //device VID and PID
        }

        /**
          * @brief Closes the password tool device.
          *
          */
        public void closeDevice() {
            usbhid.CloseDevice();
        }


        /**
          * @brief Encrypts a byte array.
          *
          * @param password: The byte array to be encrypted.
          *
          * @retval The encrypted array.
          *
          */
        public byte[] HashPassword(byte[] password)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hashedPassword = sha.ComputeHash(password);
            return hashedPassword;
        }


        /**
          * @brief Combines two byte array into one.
          *
          * @param first: The first part of the new byte array.
          * 
          * @param second: The second part of the new byte array.
          *
          * @retval The combined byte array.
          *
          */
        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }


        /**
          * @brief Creates an UTF8 coded byte array from password tool byte array
          *
          * @note We store every character in one byte on the device, so for special characters uiniqe coding is needed
          * 
          * @note We slice UTF8 codes in half and only use the second part of it. In this way we can store less characters, but we can add every special hungarian character
          *
          * @param element: The byte array to convert.
          * 
          * @retval The UTF8 coded string.
          *
          */
        static string ByteToUtf(byte[] element)
        {
            string ret;
            int i = 0;
            int k = 0;
            byte[] utfbytes = new byte[(element.Length * 2)];
            for (i = 0; i < element.Length - 1; i++, k++)
            {
                //end of string
                if (element[k] == 0)
                {
                    utfbytes[i] = element[k];
                    break;
                }

                // "ő" charcter
                else if (element[k] == 0xF5)
                {
                    utfbytes[i] = 0xC5;
                    i++;
                    utfbytes[i] = 0x91;
                }

                // "Ő" charcter
                else if (element[k] == 0xD5)
                {
                    utfbytes[i] = 0xC5;
                    i++;
                    utfbytes[i] = 0x90;
                }

                // "ű" charcter
                else if (element[k] == 0xfb)
                {
                    utfbytes[i] = 0xC5;
                    i++;
                    utfbytes[i] = 0xb1;
                }

                // "Ű" charcter
                else if (element[k] == 0xdb)
                {
                    utfbytes[i] = 0xC5;
                    i++;
                    utfbytes[i] = 0xb0;
                }

                // "ő" charcter
                else
                {
                    //the characters whose second byte is above 127 uses 195 for the first byte in hungarian characters
                    if (element[k] > 127)
                    {
                        utfbytes[i] = 195;
                        i++;
                    }

                    //the characters whose second byte is below 127 are stored in one byte, so we have to do nothing
                    utfbytes[i] = element[k];
                }
            }
            ret = Encoding.UTF8.GetString(utfbytes);
            return ret;
        }


        /**
          * @brief Creates from an UTF8 coded byte array another byte array which only stores characters in only one byte
          *
          * @note We store every character in one byte on the device, so for special characters uiniqe coding is needed
          * 
          * @note We slice UTF8 codes in half and only use the second part of it. In this way we can store less characters, but we can add every special hungarian character
          *
          * @param s: The byte array to convert.
          * 
          * @retval The one byte coded string.
          *
          */
        static byte[] GetStringBytes(string s)
        {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < s.Length; i++)
            {
                var ss = s[i];
                var bb = Encoding.UTF8.GetBytes(ss.ToString());

                //special characters are getting unique codes
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

                //cutting the charcters half
                bytes.Add(bb.Last());
            }

            bytes.Add((byte)0);

            return bytes.ToArray();
        }

        /**
          * @brief Function for Syncronised communication.
          *
          * @note First we ask something from the device, after we wait for the response.
          * 
          * @note This function uses a basic checksum for cheking if we got the valid answer for our message.
          *
          * @param message: The message send to device
          * 
          * @retval The answer from the device.
          *
          */
        private byte[] ReadWrite(byte[] message)
        {
            checkSum++;
            byte[] SendMessage = new byte[messageSize];
            message.CopyTo(SendMessage, 0);
            //cheksum is only one byte so we have to reset it
            if (checkSum > 255)
            {
                checkSum = 0;
            }
            //setting the checksum
            SendMessage[CheckSumPlace] = (byte)checkSum;
            //sending message to device
            usbhid.WriteFeature(SendMessage);
            Thread.Sleep(50);
            var time = DateTime.UtcNow;

            //Timeout check max 1 second
            while ((DateTime.UtcNow - time).TotalSeconds < 1)
            {
                byte[] Answer = usbhid.ReadFeature();
                Thread.Sleep(30);
                if (!Answer.All(x => x == 0))
                {
                    //cheking checksum
                    if (checkSum == Answer[CheckSumPlace])
                    {
                        return Answer;
                    }
                }
            }
            throw new Exception("Communication fail");


        }
        /**
          * @brief Asks the device for a random numbers, and retuns it
          *
          * @retval The arrived random numbers in a byte array.
          *
          */
        public byte[] WaitForRandom()
        {
            int Status = GetStatus();

            if (Status == authenticated || Status == AuthenticationSucces) {

                return new byte[] { };
            }

            byte[] RndNum = ReadWrite(new byte[] { id, RECEIVERAND });

            return RndNum;

        }


        /**
          * @brief Read the main password from the user and calculates CR response with the received random byte stream
          * 
          * @param password: main password from the user
          * 
          * @param random: the random byte stream received from the Password Tool 
          * 
          *
          */
        public void SendResponse(string password, byte[] random)
        {
            byte[] salt = new byte[12];
            Array.Copy(random, 1, salt, 0, 12);
            byte[] passwordBytesRaw = GetStringBytes(password);
            byte[] passwordBytes = new byte[passwordBytesRaw.Length - 1];
            Array.Copy(passwordBytesRaw, 0, passwordBytes, 0, passwordBytesRaw.Length - 1);
            byte[] hashedPassword = HashPassword(passwordBytes);
            byte[] HMAC = Combine(hashedPassword, salt);

            byte[] hashedHMAC = HashPassword(HMAC);
            byte[] sendData = Combine(new byte[] { id, (byte)validate }, hashedHMAC);
            usbhid.WriteFeature(sendData);
        }




        /**
          * @brief Asks the status of the device, and returns what should the user do.
          * 
          * @retval: 1: No existing user found, we must create one.
          *          2: User found we must add the correct master password or delete previous user.
          *          3: User already logged in, device works normally.
          *          4: Answer to an incrrect password.
          *          0: Something went wrong
          * 
          */
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


        /**
          * @brief Asks the device for maximum number of records  can be stored on it.
          * 
          * @retval: Returns the answer.
          * 
          */
        public int GetMaxPassCount() {
            //the arrived answer
            byte[] MaxPassCount = ReadWrite(new byte[] { id, SEND_MAX_PASS_COUNT });
            //the real answer begins form the second byte of the message
            return MaxPassCount[1];
        }

        /**
          * @brief Asks the device for current number of records stored on it.
          * 
          * @retval: Returns the answer.
          * 
          */
        public int GetPassCount()
        {
            //the arrived answer
            byte[] PassCount = ReadWrite(new byte[] { id, SEND_PASS_COUNT });
            //the real answer begins form the second byte of the message
            return PassCount[1];
        }

        /**
          * @brief Asks the device for MASS DELETE
          * 
          * @note: This function deletes all records and master password from the device
          * 
          */
        public void MassDelete() {

            //the command
            usbhid.WriteFeature(new byte[] { id, DELETE_ALL });
            Thread.Sleep(500);
        }


        /**
          * @brief Used to teach a new master password to device
          * 
          * @note  This message in not encrypted
          * 
          * @param masterPassword: the new masterPassword
          * 
          */
        public void SendMasterPassword(string masterPassword)
        {
            byte[] bMasterPassword = GetStringBytes(masterPassword);
            //creating a valid message form the master password
            bMasterPassword = Combine(new byte[] {id, (byte)validate}, bMasterPassword);
            bMasterPassword = Combine(bMasterPassword, new byte[] { (byte)'\0' });
            //the command
            usbhid.WriteFeature(bMasterPassword);
        }


        /**
          * @brief Asks the device for current login state.
          * 
          * @retval: Returns the answer.
          * 
          */
        private int GetStatus() {
            //the arrived answer
            byte[] Status = ReadWrite(new byte[] { id, SEND_STATUS });
            //the real answer begins form the second byte of the message
            return Status[1];
        }


        /**
          * @brief Asks the device for logout.
          * 
          */
        public void LogOut() {
            //The command
            usbhid.WriteFeature(new byte[] { id, LOGOUT });
        }



        /**
          * @brief Cheks if the given serial number is valid or not..
          * 
          * @param which: The asked record serial number
          * 
          * @retval: valid or argumentExecption
          * 
          */
        private bool CheckValidPassNum(uint which) {
            int PassCount = GetPassCount();

            if (which > PassCount || which == 0)
            {
                throw new ArgumentException();
            }
            return true;

        }


        /**
          * @brief Asks the device about a record enter numbers.
          * 
          * @note Enter number is the number of enters pressed after entering password
          * 
          * @param which: The asked record serial number
          * 
          * @retval: Returns the answer.
          * 
          */
        public uint GetEnterCount(uint which)
        {
            if (CheckValidPassNum(which))
            {
                //the arrived answer
                byte[] EnterCount = ReadWrite(new byte[] { id, SEND_ENTER_NUM, (byte)(which - 1) });
                //the real answer begins form the second byte of the message
                return EnterCount[1];

            }

            return 0;
            
        }


        /**
          * @brief Asks the device about a record tab numbers.
          * 
          * @note Tabulator number is the number of tabulators pressed bewtween username and password.
          * 
          * @param which: The asked record serial number
          * 
          * @retval: Returns the answer.
          * 
          */
        public uint GetTabCount(uint which)
        {
            if (CheckValidPassNum(which))
            {
                //the arrived answer
                byte[] TabCount = ReadWrite(new byte[] { id, SEND_TAB_NUM, (byte)(which - 1) });
                //the real answer begins form the second byte of the message
                return TabCount[1];

            }

            return 0;

        }


        /**
          * @brief Asks the device about a records string.
          * 
          * @param which: The asked record serial number
          * 
          * @param what: There is only 3 option:
          *              SEND_PASS: the string is the records password
          *              SEND_USERNAME : the string is the records username
          *              SEND_PASS_NAME : the string is the records name
          *              
          * @retval: Returns the answer.
          * 
          */
        public string GetStringFromPass(uint which, uint what)
        {
            byte[] PassString = new byte[64];

            if (what != SEND_PASS && what != SEND_USERNAME && what != SEND_PASS_NAME) {
                throw new ArgumentException();
            }


            if (CheckValidPassNum(which))
            {
                int k;
                //the arrived answer
                byte[] PassNameByte = ReadWrite(new byte[] { id, (byte)what, (byte)(which - 1) });
                PassNameByte[messageSize - 1] = (byte)'\0';
                
                for (k = 1; PassNameByte[k] != 0; k++)
                {
                    //the real answer begins form the second byte of the message
                    PassString[k - 1] = PassNameByte[k];
                }
                PassString[k - 1] = (byte)'\0';
                
            }

            //Transforms the password tool's bytes into UTF8 coded characters
            string ret = ByteToUtf(PassString);
            return ret;
            
        }


        /**
          * @brief Creates a string array from every records name on the device.
          * 
          * @param PassList: This array will be overrided with the retval. 
          *              
          * @retval: The created List.
          * 
          */
        public string[] ListPassword(string[] PassList) {

            //getting the number of passwords on the device
            int PassCount = GetPassCount();
            int k;

 

            for (int i = 0; i < PassCount; i++) {

                //Asking and saving every records name
                byte[] PassNameByte = ReadWrite(new byte[] { id, SEND_PASS_NAME, (byte)i });
            
                
                byte[] PassNameChar = new byte[65];
                //safety closeing 0
                PassNameChar[64] = (byte)'\0';

                for (k = 1; PassNameByte[k] != 0 && i < 65; k++) {
                    //the real answer begins form the second byte of the message
                    PassNameChar[k-1] = PassNameByte[k];
                }

                //setting a closing 0 to the real end of the string
                PassNameChar[k - 1] = (byte)'\0';
                
                //adding string to asked array
                PassList[i] = ByteToUtf(PassNameChar);
            }

            return PassList;
        }


        /**
          * @brief Asks the device to enter the selected record.
          * 
          * @param which: The asked record serial number
          * 
          * @param how: There is only 3 option:
          *              0: Only enters password
          *              1: Only enters username
          *              2: Enters both password and username with tabulators and enters
          */
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


        /**
          * @brief Asks the device to delete the selected record.
          * 
          * @param which: The asked record serial number
          *
          */
        public void DeletePassword(uint which)
        {

            if (CheckValidPassNum(which))
            {

                usbhid.WriteFeature(new byte[] { id, DEL_PASS, (byte)(which - 1) });
            }
        }


        /**
          * @brief Stores a new record on the device
          * 
          * @note This function also used for "editing", which delets the old password and adds a new one.
          * 
          * @param which: Serial number of the record to be edited or 0 if we are adding a new record.
          * 
          * @param name: The new page name.
          * 
          * @param username: The new username.
          * 
          * @param password: The new password.
          * 
          * @param tabNum: The number of tabulators between username and password.
          * 
          * @param enterNum: The number of enters after password.
          *
          */
        public void AddEditPassword(uint which, string name, string username, string password, uint tabNum, uint enterNum) {

            //check for storage space on device
            if ((GetPassCount() + 1) > GetMaxPassCount() && which == 0) { 
                throw new Exception("Device is full");
            }

            //encoding every string to 1 byte.
            byte[] bName = GetStringBytes(name);
            bName = Combine(bName, new byte[] { (byte)'\0' });
            byte[] bUsername = GetStringBytes(username);
            bUsername = Combine(bUsername, new byte[] { (byte)'\0' });
            byte[] bPassword = GetStringBytes(password);
            bPassword = Combine(bPassword, new byte[] { (byte)'\0' });
            
            byte[] firstRep;
            byte[] secondRep;
            byte[] thridRep;

            //adding new record
            if (which == 0)
            {
                //first report conatins the page name, enter number and tabulator number
                firstRep = Combine(new byte[] { id, ADDING_PASS, 0, (byte)enterNum, (byte)tabNum }, bName);
                //second report conatins the username
                secondRep = Combine(new byte[] { id, ADDING_PASS }, bPassword);
                //third report conatins the password
                thridRep = Combine(new byte[] { id, ADDING_PASS }, bUsername);
            }

            //editing record
            else if (CheckValidPassNum(which))
            {
                //first report conatins the page name, enter number and tabulator number
                firstRep = Combine(new byte[] { id, EDIT_PASS, (byte)(which-1), (byte)enterNum, (byte)tabNum }, bName);
                //second report conatins the username
                secondRep = Combine(new byte[] { id, EDIT_PASS }, bPassword);
                //third report conatins the password
                thridRep = Combine(new byte[] { id, EDIT_PASS }, bUsername);
            }
            else {
                return;
            }

            //To be able to store 63 character long passwords we have to split the teaching into 3 report
            usbhid.WriteFeature(firstRep);
            Thread.Sleep(150);
            usbhid.WriteFeature(secondRep);
            Thread.Sleep(150);
            usbhid.WriteFeature(thridRep);
            Thread.Sleep(150);

        }

    }
}
