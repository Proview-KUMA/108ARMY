using System;
using System.Management;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Lib
{
    /// <summary>
    /// This Clase Must Reference System.Management To This Project. Project -> Add Reference .
    /// Select The Tab ID is ".Net" and Add System.Management .
    /// </summary>
    class DeviceID
    {
        private Dictionary<string, string> _DeviceIdList = new Dictionary<string, string>();

        public Dictionary<string, string> DeviceIdList
        {
            get {
                return _DeviceIdList;
            }
            set {
                _DeviceIdList = value;
            }
        }
        
        public DeviceID()
        { 
        
        }

        public void GetDeviceID()
        {
            ManagementPath path = new ManagementPath();
            ManagementClass devs = null;
            path.Server = ".";
            path.NamespacePath = @"root\CIMV2";
            path.RelativePath = @"Win32_PnPentity";
            using (devs = new ManagementClass(new ManagementScope(path), path, new ObjectGetOptions(null, new TimeSpan(0, 0, 0, 2), true)))
            {
                ManagementObjectCollection moc = devs.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    PropertyDataCollection devsProperties = mo.Properties;
                    foreach (PropertyData devProperty in devsProperties)
                    {
                        if (devProperty.Name == "DeviceID")
                        {
                            _DeviceIdList.Add(devProperty.Value.ToString(), devProperty.Value.ToString());
                        }
                    }
                }
            }          
        }
    }
}
