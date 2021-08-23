using Newtonsoft.Json;
using ssLoader.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssLoader
{
    public partial class StartPrice : Form
    {
        public StartPrice()
        {
            OnStart();
            InitializeComponent();
        }
        private string getCurrDir = Directory.GetCurrentDirectory();
        private void StartPrice_Load(object sender, EventArgs e)
        {
           
        }

        private async void OnStart()
        {
            try
            {
                using FileStream moneyFile = File.OpenRead(@$"{getCurrDir}\Config\StartPrice.json");
                var prices = await System.Text.Json.JsonSerializer.DeserializeAsync<MoneyStart>(moneyFile);
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
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка чтения конфига");
            }
            
        }
        private void OnReload()
        {
            try
            {
                string json = File.ReadAllText(@$"{getCurrDir}\Config\StartPrice.json");
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
                using StreamWriter file = File.CreateText(@$"{getCurrDir}\Config\StartPrice.json");
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(file, jsonObj);
                MessageBox.Show("Цены обновлены", "Обновлено");
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
