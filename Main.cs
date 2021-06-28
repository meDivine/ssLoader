using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ssLoader.Arizona;
using ssLoader.SampStoreAPI;

namespace ssLoader
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        static FolderBrowserDialog folderPath = new FolderBrowserDialog();
        
        private void folderBrowse_Click(object sender, EventArgs e)
        {
            var accountSender = new AddAccount();
            DialogResult res = folderPath.ShowDialog();

            if (res == DialogResult.OK)
            {
                pathLabel.Text = $"Path: {accountSender.ToBase64("Cfif1998")}";
            }
            /*var Arizona = new Arizona.GetAccounts();
            
            foreach (var test in Arizona.CheckNameArizona(folderPath.SelectedPath))
            {
                MessageBox.Show(test);
            }*/
           
        }

        private void startButton_Click(object sender, EventArgs e)
        {
             var accountSender = new AddAccount();
            //  arzSend.ArizonaSender(folderPath.SelectedPath));
            string password = "Cfif1998";
            Task.Run(() => accountSender.SendApi("1c96b8c2e30f007345c42825d556a0b1", "95.181.158.75:7777", "20", null, "Lucian_Butchers", password, null, "testcode", "test title"));
        }
    }
}
