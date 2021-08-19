using Newtonsoft.Json;
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
        private async void OnReload()
        {
            try
            {
                string json = File.ReadAllText(@$"{getCurrDir}\Config\Levels.json");
                dynamic jsonObj = JsonConvert.DeserializeObject<Levels>(json);
                jsonObj.ArizonaRP = textBoxArizonaRP.Text;
                jsonObj.AdvanceRP = textBoxAdvanceRP.Text;
                jsonObj.AmazingRP = textBoxAmazingRP.Text;
                jsonObj.DiamondRP = textBoxDiamondRP.Text;
                jsonObj.EvolveRP = textBoxEvolveRP.Text;
                jsonObj.GTARP = textBoxGTARP.Text;
                jsonObj.RadmirRP = textBoxRadmirRP.Text;
                jsonObj.RodinaRP = textBoxRodinaRP.Text;
                jsonObj.SampRP = textBoxSampRP.Text;
                jsonObj.TrinityRP = textBoxTrinityRP.Text;
                using StreamWriter file = File.CreateText(@$"{getCurrDir}\Config\Levels.json");
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                await serializer.Serialize(file, jsonObj);
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
