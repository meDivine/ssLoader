using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ssLoader
{
    class gets
    {
        private static string GetHDDSerialNo()
        {
            ManagementObjectCollection instances = new ManagementClass("Win32_LogicalDisk").GetInstances();
            string text = "";
            foreach (ManagementBaseObject managementBaseObject in instances)
            {
                ManagementObject managementObject = (ManagementObject)managementBaseObject;
                text += Convert.ToString(managementObject["VolumeSerialNumber"]);
            }
            return text;
        }

        private static string GetComputerName()
        {
            ManagementObjectCollection instances = new ManagementClass("Win32_ComputerSystem").GetInstances();
            string result = string.Empty;
            foreach (ManagementBaseObject managementBaseObject in instances)
            {
                result = (string)((ManagementObject)managementBaseObject)["Name"];
            }
            return result;
        }
        private static string GetProcessorInformation()
        {
            ManagementObjectCollection instances = new ManagementClass("win32_processor").GetInstances();
            string result = string.Empty;
            foreach (ManagementBaseObject managementBaseObject in instances)
            {
                ManagementObject managementObject = (ManagementObject)managementBaseObject;
                string text = (string)managementObject["Name"];
                result = string.Concat(new string[]
                {text,", ",(string)managementObject["Caption"],", ",(string)managementObject["SocketDesignation"]});
            }
            return result;
        }
        private static string GetAccountName()
        {
            foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_UserAccount").Get())
            {
                ManagementObject managementObject = (ManagementObject)managementBaseObject;
                try
                {
                    return managementObject.GetPropertyValue("Name").ToString();
                }
                catch
                {
                }
            }
            return "User Account Name: Unknown";
        }
        private static string GetProcessorId()
        {
            ManagementObjectCollection instances = new ManagementClass("win32_processor").GetInstances();
            string result = string.Empty;
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    result = ((ManagementObject)enumerator.Current).Properties["processorID"].Value.ToString();
                }
            }
            return result;
        }

    }
}
