using Newtonsoft.Json;
using ssLoader.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace ssLoader
{
    public partial class Moneys : Form
    {
        public Moneys()
        {
            OnStart();
            InitializeComponent();
        }
        private string getCurrDir = Directory.GetCurrentDirectory();

        private async void OnStart()
        {
            using FileStream moneyFile = File.OpenRead(@$"{getCurrDir}\Config\Money.json");
            var prices = await System.Text.Json.JsonSerializer.DeserializeAsync<Money>(moneyFile);
            textBoxArizona.Text = prices.ArizonaRP.ToString();
            textBoxAdvance.Text = prices.AdvanceRP.ToString();
            textBoxAmazing.Text = prices.AmazingRP.ToString();
            textBoxDiamond.Text = prices.DiamondRP.ToString();
            textBoxEvolve.Text = prices.EvolveRP.ToString();
            textBoxGTARP.Text = prices.GTARP.ToString();
            textBoxRadmir.Text = prices.RadmirRP.ToString();
            textBoxRodina.Text = prices.RodinaRP.ToString();
            textBoxSRP.Text = prices.SampRP.ToString();
            textBoxTrinity.Text = prices.TrinityRP.ToString();
        }

        private void OnReload()
        {
            try
            {
                string json = File.ReadAllText(@$"{getCurrDir}\Config\Money.json");
                dynamic jsonObj = JsonConvert.DeserializeObject<Money>(json);
                jsonObj.ArizonaRP = int.Parse(textBoxArizona.Text);
                jsonObj.AdvanceRP = int.Parse(textBoxAdvance.Text);
                jsonObj.AmazingRP = int.Parse(textBoxAmazing.Text);
                jsonObj.DiamondRP = int.Parse(textBoxDiamond.Text);
                jsonObj.EvolveRP = int.Parse(textBoxEvolve.Text);
                jsonObj.GTARP = int.Parse(textBoxGTARP.Text);
                jsonObj.RadmirRP = int.Parse(textBoxRadmir.Text);
                jsonObj.RodinaRP = int.Parse(textBoxRodina.Text);
                jsonObj.SampRP = int.Parse(textBoxSRP.Text);
                jsonObj.TrinityRP = int.Parse(textBoxTrinity.Text);
                using StreamWriter file = File.CreateText(@$"{getCurrDir}\Config\Money.json");
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(file, jsonObj);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Введено не число");
            }

        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            OnReload();
        }
    }
}
