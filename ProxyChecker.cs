using ssLoader.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssLoader
{
    public partial class ProxyChecker : Form
    {
        public ProxyChecker()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string getCurrDir = Directory.GetCurrentDirectory();
            File.WriteAllText($"{getCurrDir}]proxy.txt", String.Empty);
            var proxies = new ProxyCheck();
            foreach (string i in richTextBox1.Lines)
            {
                //Thread mythread = new Thread(proxies.Check);
                //mythread.Start(i);
                //  Thread.Sleep(100);
                Task.Run(() => proxies.Check(i));
            }
            MessageBox.Show("Проверка запущена, проверка закончится через 1 минуту");
        }
    }
}
