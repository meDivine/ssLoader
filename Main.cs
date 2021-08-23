using ssLoader.Arizona;
using ssLoader.SampStoreAPI;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                pathLabel.Text = $"Путь: {folderPath.SelectedPath}";
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
            MessageBox.Show("Залив аккаунтов запущен\nПосле окончания появится окно со статистикой обработки", "Успешный запуск");
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


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pathLabel_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            timeEnd.Text = "Окончание: " + UnixTimeToDateTime(long.Parse(Start.KeyAuthApp.user_data.subscriptions[0].expiry));
        }
        public DateTime UnixTimeToDateTime(long unixtime)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            return dtDateTime;
        }

        private void коэффМашинToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Coeffoc = new Coeffic();
            Coeffoc.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/sampstoreloader");
        }
    }
}
