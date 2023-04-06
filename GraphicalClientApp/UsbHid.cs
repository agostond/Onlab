using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    internal class UsbHid
    {
        Guid GetHidGuid()
        {
            Guid guid = Guid.Empty;
            Win32Api.Hid.HidD_GetHidGuid(ref guid);
            return guid;

        }

        string[] GetDevicePaths(Guid guid)
        {
            IntPtr deviceinfoset = new IntPtr();

            deviceinfoset = Win32Api.SetupApi.SetupDiGetClassDevs(ref guid, IntPtr.Zero, IntPtr.Zero, Win32Api.SetupApi.DIGCF_DEVICEINTERFACE | Win32Api.SetupApi.DIGCF_PRESENT);

            Win32Api.SetupApi.SP_DEVICE_INTERFACE_DATA deviceinterfacedata = new Win32Api.SetupApi.SP_DEVICE_INTERFACE_DATA();
            deviceinterfacedata.cbSize = Marshal.SizeOf(deviceinterfacedata);

            int memberindex = 0;
            int buffersize = 0;
            List<string> paths = new List<string>();
            do
            {

                if (
                    Win32Api.SetupApi.SetupDiEnumDeviceInterfaces(deviceinfoset, IntPtr.Zero, ref guid, memberindex, ref deviceinterfacedata)
                    )
                {

                    Win32Api.SetupApi.SetupDiGetDeviceInterfaceDetail(deviceinfoset, ref deviceinterfacedata, IntPtr.Zero, 0, ref buffersize, IntPtr.Zero);


                    IntPtr detailadatabuffer = Marshal.AllocHGlobal(buffersize);

                    //_SP_DEVICE_INTERFACE_DETAIL_DATA_A  -> DWORD  cbSize;
                    Marshal.WriteInt32(detailadatabuffer, (IntPtr.Size == 4) ? (4 + Marshal.SystemDefaultCharSize) : 8); 
                    //{
                    //    Win32Api.SetupApi.SP_DEVICE_INTERFACE_DETAIL_DATA detaildata=new Win32Api.SetupApi.SP_DEVICE_INTERFACE_DETAIL_DATA();
                    //    detaildata.cbSize = Marshal.SizeOf(detaildata);
                    //    Marshal.StructureToPtr(detaildata, detailadatabuffer, true);
                    //}
                    if (
                        Win32Api.SetupApi.SetupDiGetDeviceInterfaceDetail(deviceinfoset, ref deviceinterfacedata, detailadatabuffer, buffersize, ref buffersize, IntPtr.Zero)
                        )
                    {
                        IntPtr pathname = new IntPtr(detailadatabuffer.ToInt64()+4 );//+ 4
                        paths.Add(Marshal.PtrToStringAuto(pathname));
                    }

                    Marshal.FreeHGlobal(detailadatabuffer);



                }
                else
                {
                    break;

                }

                memberindex++;

            } while (true);


            if (deviceinfoset != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(deviceinfoset);
            }


            return paths.ToArray();

        }

        string devicename;
        SafeFileHandle DeviceHandle;
        Win32Api.Hid.HIDP_CAPS? Capabilities;
        bool opened = false;
        public bool Opened
        {
            get { return opened; }
        }

        public bool OpenDevice(int vid, int pid)
        {
            CloseDevice();
            string[] paths = GetDevicePaths(GetHidGuid());
            bool found = false;
            for (int i = 0; i < paths.Length && !found; i++)
            {
                string path = paths[i];
                SafeFileHandle filehandle = Win32Api.Kernel32.CreateFile(path, 0, Win32Api.Kernel32.FILE_SHARE_READ | Win32Api.Kernel32.FILE_SHARE_WRITE, IntPtr.Zero, Win32Api.Kernel32.OPEN_EXISTING, 0, 0);

                if (!filehandle.IsInvalid)
                {
                    Win32Api.Hid.HIDD_ATTRIBUTES attribs = new Win32Api.Hid.HIDD_ATTRIBUTES();
                    attribs.Size = Marshal.SizeOf(attribs);

                    if (Win32Api.Hid.HidD_GetAttributes(filehandle, ref attribs))
                    {
                        if (attribs.VendorID == vid && attribs.ProductID == pid)
                        {
                            devicename = path;
                            DeviceHandle = filehandle;
                            found = true;
                            break;

                        }
                    }

                    filehandle.Close();

                }

            }
            if (found)
            {
                Win32Api.Hid.HIDP_CAPS caps = new Win32Api.Hid.HIDP_CAPS();
                Capabilities = null;
                if (GetCaps(DeviceHandle, ref caps))
                {
                    Capabilities = caps;
                    opened = true;
                }
            }
            return opened;

        }


        bool GetCaps(SafeFileHandle devicehandle, ref Win32Api.Hid.HIDP_CAPS caps)
        {
            IntPtr dataptr = IntPtr.Zero;

            if (
                Win32Api.Hid.HidD_GetPreparsedData(devicehandle, ref dataptr)
                )
            {
                if (0 != Win32Api.Hid.HidP_GetCaps(dataptr, ref caps))
                {
                    return true;
                }

            }

            return false;

        }


        public void CloseDevice()
        {
            opened = false;
            devicename = null;
            if (DeviceHandle != null && !DeviceHandle.IsInvalid)
            {
                DeviceHandle.Close();
                DeviceHandle = null;
            }
        }

        public int FeatureDataLength
        {
            get
            {
                if (Opened && Capabilities.HasValue)
                {
                    return Capabilities.Value.FeatureReportByteLength;
                }

                return 0;
            }
        }

        public byte[] ReadFeature()
        {
            if (Opened)
            {

                if (Capabilities.HasValue && Capabilities.Value.FeatureReportByteLength > 0)
                {
                    int len = Capabilities.Value.FeatureReportByteLength;
                    byte[] inbuffer = new byte[len];

                    if (Win32Api.Hid.HidD_GetFeature(DeviceHandle, inbuffer, inbuffer.Length))
                    {
                        return inbuffer;
                    }

                }

            }
            return null;

        }

        public bool WriteFeature(byte[] data)
        {
            if (Opened)
            {

                if (Capabilities.HasValue && Capabilities.Value.FeatureReportByteLength > 0)
                {
                    int len = Capabilities.Value.FeatureReportByteLength;
                    byte[] outbuffer = new byte[len];
                    for (int i = 0; i < len; i++)
                    {
                        if (i < data.Length)
                        {
                            outbuffer[i] = data[i];
                        }
                        else
                        {
                            outbuffer[i] = 0;
                        }
                    }

                    if (Win32Api.Hid.HidD_SetFeature(DeviceHandle, outbuffer, outbuffer.Length))
                    {
                        return true;
                    }

                }

            }

            return false;
        }



    }
}
