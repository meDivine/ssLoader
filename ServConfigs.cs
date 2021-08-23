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
    public partial class ServConfigs : Form
    {
        private string getCurrDir = Directory.GetCurrentDirectory();
        public ServConfigs()
        {
            OnStart();
            InitializeComponent();
        }
        private async void OnStart()
        {
            using FileStream ArizonaFile = File.OpenRead(@$"{getCurrDir}\Config\Servers\Arizona RP.json");
            var jsonArizona = await System.Text.Json.JsonSerializer.DeserializeAsync<ServerSets>(ArizonaFile);
            ArizonaCheckBox.Checked = jsonArizona.SellStatus;
            ArizonaMinPrice.Text = jsonArizona.min_price.ToString();
            ArizonaMinVirts.Text = jsonArizona.min_virts.ToString();
            ArizonaMinLVL.Text = jsonArizona.min_lvl.ToString();
            ArzCheckBoxMail.Checked = jsonArizona.mail_guard;
            using FileStream AmazingFile = File.OpenRead(@$"{getCurrDir}\Config\Servers\Amazing RP.json");
            var jsonAmazing = await System.Text.Json.JsonSerializer.DeserializeAsync<ServerSets>(AmazingFile);
            AmazingcheckBox2.Checked = jsonAmazing.SellStatus;
            AmazingMinPrice.Text = jsonAmazing.min_price.ToString();
            AmazingMinVirts.Text = jsonAmazing.min_virts.ToString();
            AmazingMinLvl.Text = jsonAmazing.min_lvl.ToString();
            AmazingMail.Checked = jsonAmazing.mail_guard;
            using FileStream AdvanceFile = File.OpenRead(@$"{getCurrDir}\Config\Servers\Advance RP.json");
            var jsonAdvance = await System.Text.Json.JsonSerializer.DeserializeAsync<ServerSets>(AdvanceFile);
            AdvancecheckBox2.Checked = jsonAdvance.SellStatus;
            AdvanceMinPrice.Text = jsonAdvance.min_price.ToString();
            AdvanceMinVirts.Text = jsonAdvance.min_virts.ToString();
            AdvanceMinLvl.Text = jsonAdvance.min_lvl.ToString();
            AdvanceMail.Checked = jsonAdvance.mail_guard;
            using FileStream DiamondFile = File.OpenRead(@$"{getCurrDir}\Config\Servers\Diamond RP.json");
            var jsonDiamond = await System.Text.Json.JsonSerializer.DeserializeAsync<ServerSets>(DiamondFile);
            DiamondCheckBox.Checked = jsonDiamond.SellStatus;
            DiamondMinPrice.Text = jsonDiamond.min_price.ToString();
            DiamomdMinVirts.Text = jsonDiamond.min_virts.ToString();
            DiamondMinLVL.Text = jsonDiamond.min_lvl.ToString();
            DiamondMail.Checked = jsonDiamond.mail_guard;
            using FileStream GTARPFile = File.OpenRead(@$"{getCurrDir}\Config\Servers\GTA RP.json");
            var jsonGTARP = await System.Text.Json.JsonSerializer.DeserializeAsync<ServerSets>(GTARPFile);
            GTARPChechBox.Checked = jsonGTARP.SellStatus;
            GTARPMinPrice.Text = jsonGTARP.min_price.ToString();
            GTARPMinVirts.Text = jsonGTARP.min_virts.ToString();
            GTARPMinLVL.Text = jsonGTARP.min_lvl.ToString();
            GTARPMail.Checked = jsonGTARP.mail_guard;
            using FileStream EvolveFile = File.OpenRead(@$"{getCurrDir}\Config\Servers\Evolve RP.json");
            var jsonEvolve = await System.Text.Json.JsonSerializer.DeserializeAsync<ServerSets>(EvolveFile);
            EvolveCheckBox.Checked = jsonEvolve.SellStatus;
            EvolveTextBox.Text = jsonEvolve.min_price.ToString();
            EvolveVirts.Text = jsonEvolve.min_virts.ToString();
            EvolveLVL.Text = jsonEvolve.min_lvl.ToString(); 
            EvolveMail.Checked = jsonEvolve.mail_guard;
            using FileStream RadmirFile = File.OpenRead(@$"{getCurrDir}\Config\Servers\Radmir RP.json");
            var jsonRadmir = await System.Text.Json.JsonSerializer.DeserializeAsync<ServerSets>(RadmirFile);
            RadmirRPCheckBox.Checked = jsonRadmir.SellStatus;
            RadmirRPMinPrice.Text = jsonRadmir.min_price.ToString();
            RadmirRPMinVirts.Text = jsonRadmir.min_virts.ToString();
            RadmirRPMinLVL.Text = jsonRadmir.min_lvl.ToString();
            RadmirMail.Checked = jsonRadmir.mail_guard;
            using FileStream RodinaFile = File.OpenRead(@$"{getCurrDir}\Config\Servers\Rodina RP.json");
            var jsonRodina = await System.Text.Json.JsonSerializer.DeserializeAsync<ServerSets>(RodinaFile);
            RodinaCheckBox.Checked = jsonRodina.SellStatus;
            RodinaMinProce.Text = jsonRodina.min_price.ToString();
            RodinaMinVirts.Text = jsonRodina.min_virts.ToString();
            RodinaMinLVL.Text = jsonRodina.min_lvl.ToString();
            RodinaMail.Checked = jsonRodina.mail_guard;
            using FileStream SRPFile = File.OpenRead(@$"{getCurrDir}\Config\Servers\Samp RP.json");
            var jsonSRP = await System.Text.Json.JsonSerializer.DeserializeAsync<ServerSets>(SRPFile);
            SRPCB.Checked = jsonSRP.SellStatus;
            SRPMinPrice.Text = jsonSRP.min_price.ToString();
            SRPMinVirts.Text = jsonSRP.min_virts.ToString();
            SRPMinLVL.Text = jsonSRP.min_lvl.ToString();
            SRPMail.Checked = jsonSRP.mail_guard;
            using FileStream TrinityFile = File.OpenRead(@$"{getCurrDir}\Config\Servers\Trinity RP.json");
            var jsonTRP = await System.Text.Json.JsonSerializer.DeserializeAsync<ServerSets>(TrinityFile);
            TrinityCB.Checked = jsonTRP.SellStatus;
            TrinityMinPrice.Text = jsonTRP.min_price.ToString();
            TrinityMinVirts.Text = jsonTRP.min_virts.ToString();
            TrinityMinLVL.Text = jsonTRP.min_lvl.ToString();
            TrinityMail.Checked = jsonTRP.mail_guard;
        }
        private void OnReload()
        {
            try
            {
                string jsonARZ = File.ReadAllText(@$"{getCurrDir}\Config\Servers\Arizona RP.json");
                dynamic jsonObj = JsonConvert.DeserializeObject<ServerSets>(jsonARZ);

                using StreamWriter file = File.CreateText(@$"{getCurrDir}\Config\Servers\Arizona RP.json");
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(file, jsonObj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}
