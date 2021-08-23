using Newtonsoft.Json;
using ssLoader.Json.CarPrice;
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
    public partial class Coeffic : Form
    {
        public Coeffic()
        {
            OnStart();
            InitializeComponent();
        }
        private string getCurrDir = Directory.GetCurrentDirectory();
        private void Refresh_Click(object sender, EventArgs e)
        {
            OnReload();
        }
        private async void OnStart()
        {
            using FileStream carFile = File.OpenRead(@$"{getCurrDir}\Config\Cars\Coefficient.json");
            var prices = await System.Text.Json.JsonSerializer.DeserializeAsync<Prices>(carFile);
            textBoxArizona.Text = prices.ArizonaRP.ToString();
            textBoxAdvance.Text = prices.AdvanceRP.ToString();
            textBoxAmazing.Text = prices.AmazingRP.ToString();
            textBoxDiamond.Text = prices.DiamondRP.ToString();
            textBoxEvolve.Text = prices.EvolveRP.ToString();
            textBoxGTARP.Text = prices.GTARP.ToString();
            textBoxRadmir.Text = prices.RadmirRP.ToString();
            textBoxRodina.Text = prices.RodinaRP.ToString();
            textBoxTrinity.Text = prices.TrinityRP.ToString();
        }
        private void OnReload()
        {
            try
            {
                string json = File.ReadAllText(@$"{getCurrDir}\Config\Cars\Coefficient.json");
                dynamic jsonObj = JsonConvert.DeserializeObject<Prices>(json);
                jsonObj.ArizonaRP = double.Parse(textBoxArizona.Text);
                jsonObj.AdvanceRP = double.Parse(textBoxAdvance.Text);
                jsonObj.AmazingRP = double.Parse(textBoxAmazing.Text);
                jsonObj.DiamondRP = double.Parse(textBoxDiamond.Text);
                jsonObj.EvolveRP = double.Parse(textBoxEvolve.Text);
                jsonObj.GTARP = double.Parse(textBoxGTARP.Text);
                jsonObj.RadmirRP = double.Parse(textBoxRadmir.Text);
                jsonObj.RodinaRP = double.Parse(textBoxRodina.Text);
                jsonObj.TrinityRP = double.Parse(textBoxTrinity.Text);
                using StreamWriter file = File.CreateText(@$"{getCurrDir}\Config\Cars\Coefficient.json");
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(file, jsonObj);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Введено не число");
            }
        }

        }
    }
