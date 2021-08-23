using KeyAuth;
using Newtonsoft.Json;
using ssLoader.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssLoader
{
    public partial class Start : Form
    {
        static string name = "SS-LOADER"; 
        static string ownerid = "xDnPUORbjj"; 
        static string secret = "7a4e6b0767258227dc3ab4d5e4147c30a9120d799c069fa88d5b8ca3690eeecd"; 
        static string version = "1.0"; 
         public static api KeyAuthApp = new api(name, ownerid, secret, version);
        public Start()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private string getCurrDir = Directory.GetCurrentDirectory();
        private void checkButton_Click(object sender, EventArgs e)
        {
            if (KeyAuthApp.license(textBox1.Text))
            {
                string json = File.ReadAllText(@$"{getCurrDir}\Config\Config.json");
                dynamic jsonObj = JsonConvert.DeserializeObject<Configurate>(json);
                jsonObj.license = textBox1.Text;
                using StreamWriter file = File.CreateText(@$"{getCurrDir}\Config\Config.json");
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, jsonObj);
                Main main = new Main();
                main.Show();
                this.Hide();
            }
        }
        public static NetworkInterface GetActiveEthernetOrWifiNetworkInterface()
        {
            var Nic = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                a => a.OperationalStatus == OperationalStatus.Up &&
                (a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || a.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                a.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily.ToString() == "InterNetwork"));

            return Nic;
        }

        public static void SetDNS(string DnsString)
        {
            string[] Dns = { DnsString };
            var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
            if (CurrentInterface == null) return;

            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();
            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    if (objMO["Description"].ToString().Equals(CurrentInterface.Description))
                    {
                        ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
                        if (objdns != null)
                        {
                            objdns["DNSServerSearchOrder"] = Dns;
                            objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
                        }
                    }
                }
            }
        }

        public static void UnsetDNS()
        {
            var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
            if (CurrentInterface == null) return;

            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();
            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    if (objMO["Description"].ToString().Equals(CurrentInterface.Description))
                    {
                        ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
                        if (objdns != null)
                        {
                            objdns["DNSServerSearchOrder"] = null;
                            objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
                        }
                    }
                }
            }
        }
        private void Start_Load(object sender, EventArgs e)
        {
            onLoadAsync();
            SetDNS("1.1.1.1"); 
            KeyAuthApp.init();
        }
        async Task onLoadAsync()
        {
            using FileStream cfg = File.OpenRead(@$"{getCurrDir}\Config\Config.json");
            var jsonText = await System.Text.Json.JsonSerializer.DeserializeAsync<Configurate>(cfg);
            textBox1.Text = jsonText.license;
        }
    }
}
