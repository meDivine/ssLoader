using ssLoader.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssLoader
{
    public partial class Moneys : Form
    {
        public Moneys()
        {
            onStart();
            InitializeComponent();
        }
        private string getCurrDir = Directory.GetCurrentDirectory();

        private async void onStart() 
        {
            using FileStream moneyFile = File.OpenRead(@$"{getCurrDir}\Config\Money.json");
            var prices = await JsonSerializer.DeserializeAsync<Money>(moneyFile);
            textBoxArizona.Text = prices.ArizonaRP.ToString();
            textBoxAdvance.Text = prices.AdvanceRP.ToString();
            textBoxAmazing.Text = prices.AmazingRP.ToString();
            textBoxDiamond.Text = prices.DiamondRP.ToString();
            textBoxEvolve.Text = prices.EvolveRP.ToString();
            textBoxGTARP.Text = prices.GTARP.ToString();
            textBoxRadmir.Text = prices.RadmirRP.ToString();
            textBoxRodina.Text = prices.RodinaRP.ToString();
            textBoxSRP.Text = prices.SampRP.ToString();
            textBoxTrinity.Text  = prices.TrinityRP.ToString();
        }

        private async void onReload()
        {
            using FileStream moneyFile = File.OpenRead(@$"{getCurrDir}\Config\Money.json");
            var prices = await JsonSerializer.DeserializeAsync<Money>(moneyFile);
            prices.ArizonaRP = float.Parse(textBoxArizona.Text);

        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            onReload();
        }
    }
}
