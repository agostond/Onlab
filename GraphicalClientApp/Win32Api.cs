using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    internal class Win32Api
    {
        public static class Hid
        {

            [DllImport("hid.dll", SetLastError = true)]
            internal static extern void HidD_GetHidGuid(ref System.Guid HidGuid);

            [DllImport("hid.dll", SetLastError = true)]
            internal static extern Boolean HidD_GetAttributes(SafeFileHandle HidDeviceObject, ref HIDD_ATTRIBUTES Attributes);

            [DllImport("hid.dll", SetLastError = true)]
            internal static extern Boolean HidD_SetFeature(SafeFileHandle HidDeviceObject, Byte[] lpReportBuffer, Int32 ReportBufferLength);

            [DllImport("hid.dll", SetLastError = true)]
            internal static extern Boolean HidD_GetFeature(SafeFileHandle HidDeviceObject, Byte[] lpReportBuffer, Int32 ReportBufferLength);

            [DllImport("hid.dll", SetLastError = true)]
            internal static extern Boolean HidD_GetPreparsedData(SafeFileHandle HidDeviceObject, ref IntPtr PreparsedData);

            [DllImport("hid.dll", SetLastError = true)]
            internal static extern Int32 HidP_GetCaps(IntPtr PreparsedData, ref HIDP_CAPS Capabilities);

            [DllImport("hid.dll", SetLastError = true)]
            internal static extern Boolean HidD_FlushQueue(SafeFileHandle HidDeviceObject);


            [StructLayout(LayoutKind.Sequential)]
            internal struct HIDD_ATTRIBUTES
            {
                internal Int32 Size;
                internal UInt16 VendorID;
                internal UInt16 ProductID;
                internal UInt16 VersionNumber;
            }

            internal struct HIDP_CAPS
            {
                internal Int16 Usage;
                internal Int16 UsagePage;
                internal Int16 InputReportByteLength;
                internal Int16 OutputReportByteLength;
                internal Int16 FeatureReportByteLength;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
                internal Int16[] Reserved;
                internal Int16 NumberLinkCollectionNodes;
                internal Int16 NumberInputButtonCaps;
                internal Int16 NumberInputValueCaps;
                internal Int16 NumberInputDataIndices;
                internal Int16 NumberOutputButtonCaps;
                internal Int16 NumberOutputValueCaps;
                internal Int16 NumberOutputDataIndices;
                internal Int16 NumberFeatureButtonCaps;
                internal Int16 NumberFeatureValueCaps;
                internal Int16 NumberFeatureDataIndices;
            }


        }

        public static class SetupApi
        {


            [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
            internal static extern IntPtr SetupDiGetClassDevs(ref System.Guid ClassGuid, IntPtr Enumerator, IntPtr hwndParent, Int32 Flags);

            [DllImport("setupapi.dll", SetLastError = true)]
            internal static extern Boolean SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet, IntPtr DeviceInfoData, ref System.Guid InterfaceClassGuid, Int32 MemberIndex, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

            [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
            internal static extern Boolean SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, IntPtr DeviceInterfaceDetailData, Int32 DeviceInterfaceDetailDataSize, ref Int32 RequiredSize, IntPtr DeviceInfoData);


            [DllImport("setupapi.dll", SetLastError = true)]
            internal static extern Int32 SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

            internal const Int32 DIGCF_PRESENT = 2;
            internal const Int32 DIGCF_DEVICEINTERFACE = 0X10;


            internal struct SP_DEVICE_INTERFACE_DATA
            {
                internal Int32 cbSize;
                internal System.Guid InterfaceClassGuid;
                internal Int32 Flags;
                internal IntPtr Reserved;
            }
            internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
            {
                internal Int32 cbSize;
                internal String DevicePath;
            }
        }


        public static class Kernel32
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern SafeFileHandle CreateFile(String lpFileName, UInt32 dwDesiredAccess, Int32 dwShareMode, IntPtr lpSecurityAttributes, Int32 dwCreationDisposition, Int32 dwFlagsAndAttributes, Int32 hTemplateFile);


            internal const Int32 FILE_FLAG_OVERLAPPED = 0X40000000;
            internal const Int32 FILE_SHARE_READ = 1;
            internal const Int32 FILE_SHARE_WRITE = 2;
            internal const UInt32 GENERIC_READ = 0X80000000;
            internal const UInt32 GENERIC_WRITE = 0X40000000;
            internal const Int32 INVALID_HANDLE_VALUE = -1;
            internal const Int32 OPEN_EXISTING = 3;
            internal const Int32 WAIT_TIMEOUT = 0X102;
            internal const Int32 WAIT_OBJECT_0 = 0;


        }



        //internal const Int16 FORMAT_MESSAGE_FROM_SYSTEM = 0X1000;

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //internal static extern Int32 FormatMessage(Int32 dwFlags, ref Int64 lpSource, Int32 dwMessageId, Int32 dwLanguageZId, String lpBuffer, Int32 nSize, Int32 Arguments);        


    }
}
