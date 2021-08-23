using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace ssLoader
{
    public partial class Levels : Form
    {
        public Levels()
        {
            OnStart();
            InitializeComponent();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            OnReload();
        }
        private string getCurrDir = Directory.GetCurrentDirectory();
        private async void OnStart()
        {
            using FileStream levelFile = File.OpenRead(@$"{getCurrDir}\Config\Levels.json");
            var lvl = await System.Text.Json.JsonSerializer.DeserializeAsync<Json.Level>(levelFile);
            textBoxArizonaRP.Text = lvl.ArizonaRP.ToString();
            textBoxAdvanceRP.Text = lvl.AdvanceRP.ToString();
            textBoxAmazingRP.Text = lvl.AmazingRP.ToString();
            textBoxDiamondRP.Text = lvl.DiamondRP.ToString();
            textBoxEvolveRP.Text = lvl.EvolveRP.ToString();
            textBoxGTARP.Text = lvl.GTARP.ToString();
            textBoxRadmirRP.Text = lvl.RadmirRP.ToString();
            textBoxRodinaRP.Text = lvl.RodinaRP.ToString();
            textBoxSampRP.Text = lvl.SampRP.ToString();
            textBoxTrinityRP.Text = lvl.TrinityRP.ToString();
        }
        private  void OnReload()
        {
            try
            {
                string json = File.ReadAllText(@$"{getCurrDir}\Config\Levels.json");
                dynamic jsonObj = JsonConvert.DeserializeObject<Json.Level>(json);
                jsonObj.ArizonaRP = int.Parse(textBoxArizonaRP.Text);
                jsonObj.AdvanceRP = int.Parse(textBoxAdvanceRP.Text);
                jsonObj.AmazingRP = int.Parse(textBoxAmazingRP.Text);
                jsonObj.DiamondRP = int.Parse(textBoxDiamondRP.Text);
                jsonObj.EvolveRP = int.Parse(textBoxEvolveRP.Text);
                jsonObj.GTARP = int.Parse(textBoxGTARP.Text);
                jsonObj.RadmirRP = int.Parse(textBoxRadmirRP.Text);
                jsonObj.RodinaRP = int.Parse(textBoxRodinaRP.Text);
                jsonObj.SampRP = int.Parse(textBoxSampRP.Text);
                jsonObj.TrinityRP = int.Parse(textBoxTrinityRP.Text);
                using StreamWriter file = File.CreateText(@$"{getCurrDir}\Config\Levels.json");
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                 serializer.Serialize(file, jsonObj);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK);
            }
            finally
            {
                MessageBox.Show("Обработка окончена");
            }
        }
    }
}
