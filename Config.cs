using Newtonsoft.Json;
using ssLoader.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace ssLoader
{
    public partial class Config : Form
    {
        private string getCurrDir = Directory.GetCurrentDirectory();

        public Config()
        {
            OnStart();
            InitializeComponent();
        }

        private async void OnStart()
        {
            using FileStream cfg = File.OpenRead(@$"{getCurrDir}\Config\Config.json");
            var jsonText = await System.Text.Json.JsonSerializer.DeserializeAsync<Configurate>(cfg);
            refreshTextBox.Text = jsonText.api_key;
            sellermsg.Text = jsonText.seller_message;
            textBox1.Text = jsonText.timing.ToString();
            textBoxTitle.Text = jsonText.title_message.ToString();
            proxyCheckBox.Checked = jsonText.proxy;
        }

        private void OnReload()
        {
            try
            {
                string json = File.ReadAllText(@$"{getCurrDir}\Config\Config.json");
                dynamic jsonObj = JsonConvert.DeserializeObject<Configurate>(json);
                jsonObj.api_key = refreshTextBox.Text;
                jsonObj.seller_message = sellermsg.Text;
                jsonObj.timing = int.Parse(textBox1.Text);
                jsonObj.title_message = textBoxTitle.Text;
                jsonObj.proxy = proxyCheckBox.Checked;
                using StreamWriter file = File.CreateText(@$"{getCurrDir}\Config\Config.json");
                Newtonsoft.Json.JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, jsonObj);
                MessageBox.Show("Обновление завершено", "Обновлено");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Введено не число");
            }

        }

        private void refreshbutton_Click(object sender, EventArgs e)
        {
            OnReload();
        }

        private void refreshTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void proxyCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
