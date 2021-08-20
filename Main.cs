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
                pathLabel.Text = $"Path: {res}";
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
            var arzSend = new AccountSender();
            Task.Run(() => arzSend.SendToSS(folderPath.SelectedPath));
            //string password = "Cfif1998";
            // Task.Run(() => accountSender.SendApi("1c96b8c2e30f007345c42825d556a0b1", "95.181.158.75:7777", "20", null, "Lucian_Butchers", password, null, "testcode", "test title"));
        }

        private void aPIKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form config = new Config();
            config.Show();
        }

        private void moneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form moneys = new Moneys();
            moneys.Show();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void levelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Levels = new Levels();
            Levels.Show();
        }

        public void invoker(Label label, string s)
        {
            Invoke(new Action(() => { label1.Text = s; }));
        }

        public void setLabelTextFromThread(Label label, string text)
        {

            label.Invoke((MethodInvoker)delegate
            {
                label.Text = text;
            });
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
