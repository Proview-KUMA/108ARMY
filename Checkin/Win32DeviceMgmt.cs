using System;
using System.Text;
using System.Runtime.InteropServices;

namespace DeviceInterfaceConsole
{
    public class Win32DeviceMgmt
    {
		internal static Int32 ERROR_NO_MORE_FILES = 259;

		internal static Int32 LINE_LEN = 256;
        
		internal static Int32 DIGCF_DEFAULT =  0x00000001;  // only valid with DIGCF_DEVICEINTERFACE
        internal static Int32 DIGCF_PRESENT  = 0x00000002;
        internal static Int32 DIGCF_ALLCLASSES = 0x00000004;
        internal static Int32 DIGCF_PROFILE = 0x00000008;
        internal static Int32 DIGCF_DEVICEINTERFACE = 0x00000010;
		
		internal static Int32 SPINT_ACTIVE  = 0x00000001;
		internal static Int32 SPINT_DEFAULT = 0x00000002;
		internal static Int32 SPINT_REMOVED = 0x00000004;

        [StructLayout(LayoutKind.Sequential)]
        internal struct SP_DEVINFO_DATA
        {
            /// <summary>
            /// Size of structure in bytes
            /// </summary>
            public Int32  cbSize;
            /// <summary>
            /// GUID of the device interface class
            /// </summary>
            public Guid   ClassGuid;
            /// <summary>
            /// Handle to this device instance
            /// </summary>
            public Int32  DevInst;
            /// <summary>
            /// Reserved; do not use. 
            /// </summary>
            public UIntPtr Reserved;
        };

        [StructLayout(LayoutKind.Sequential)]
        internal struct SP_DEVICE_INTERFACE_DATA
        {
            /// <summary>
            /// Size of the structure, in bytes
            /// </summary>
            public Int32 cbSize; 
            /// <summary>
            /// GUID of the device interface class
            /// </summary>
            public Guid InterfaceClassGuid; 
            /// <summary>
            /// 
            /// </summary>
            public Int32 Flags; 
            /// <summary>
            /// Reserved; do not use.
            /// </summary>
            public IntPtr Reserved;

        };

		[StructLayout(LayoutKind.Sequential)]
		internal struct SP_DRVINFO_DATA 
		{
			public Int32     cbSize;
			public Int32     DriverType;
			public UIntPtr	 Reserved;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=256)]
			public String    Description;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=256)]
			public String    MfgName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=256)]
			public String    ProviderName;
			public FILETIME	 DriverDate;
			public Int64	 DriverVersion;
		};


        [DllImport("setupapi.dll")]
		internal static extern IntPtr SetupDiGetClassDevsEx(IntPtr ClassGuid, [MarshalAs(UnmanagedType.LPStr)]String enumerator, IntPtr hwndParent, Int32 Flags, IntPtr DeviceInfoSet, [MarshalAs(UnmanagedType.LPStr)]String MachineName, IntPtr Reserved);

        [DllImport("setupapi.dll")]
        internal static extern Int32 SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

		[DllImport("setupapi.dll")]
		internal static extern Int32 SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet, IntPtr DeviceInfoData, IntPtr InterfaceClassGuid, Int32 MemberIndex, ref  SP_DEVINFO_DATA DeviceInterfaceData);

		[DllImport("setupapi.dll")]
		internal static extern Int32 SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, Int32 MemberIndex, ref  SP_DEVINFO_DATA DeviceInterfaceData);

		[DllImport("setupapi.dll")]
		internal static extern Int32 SetupDiClassNameFromGuid(ref Guid ClassGuid, StringBuilder className, Int32 ClassNameSize, ref Int32 RequiredSize);
		
		[DllImport("setupapi.dll")]
		internal static extern Int32 SetupDiGetClassDescription(ref Guid ClassGuid, StringBuilder classDescription, Int32 ClassDescriptionSize, ref Int32 RequiredSize);
		
		[DllImport("setupapi.dll")]
		internal static extern Int32 SetupDiGetDeviceInstanceId(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, StringBuilder DeviceInstanceId, Int32 DeviceInstanceIdSize, ref Int32 RequiredSize);

		[DllImport("kernel32.dll")]
		internal static extern Int32 GetLastError();
    }
}
