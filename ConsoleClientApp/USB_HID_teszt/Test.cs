using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USB_HID_teszt
{
    internal class Test
    {
        UsbHid usbhid = new UsbHid();

        public void Run()
        {
            //USB\VID_0483&PID_5750&REV_0200

            //usbhid.OpenDevice(0x16c0, 0x5df);
            usbhid.OpenDevice(0x0483, 0x5750);
            usbhid.WriteFeature(new byte[] { 0xbe, 4, 0, 2, 0x63, (byte)'\0', 0x22, 0x22, (byte)'\0' });
            Thread.Sleep(1000);
            byte[] Feature = usbhid.ReadFeature();
            if (Feature != null)
            {
                string stringFeature = BitConverter.ToString(Feature);
                System.Console.WriteLine(stringFeature);
            }
            usbhid.CloseDevice();

        }

    }
}
