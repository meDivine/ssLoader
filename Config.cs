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
    public partial class Config : Form
    {
        public Config()
        {
            OnConnect();
            InitializeComponent();
        }

        private string getCurrDir = Directory.GetCurrentDirectory();
        private async void OnConnect()
        {
            using FileStream moneyFile = File.OpenRead(@$"{getCurrDir}\Config\Config.json");
            var cfg = await System.Text.Json.JsonSerializer.DeserializeAsync<Configurate>(moneyFile);
            refreshTextBox.Text = cfg.api_key;
            messageboxSeler.Text = cfg.seller_message;
        }

        private void OnRefresh()
        {
            try
            {
                string json = File.ReadAllText(@$"{getCurrDir}\Config\Config.json");
                dynamic jsonObj = JsonConvert.DeserializeObject<Configurate>(json);
                jsonObj.api_key = refreshTextBox.Text;
                jsonObj.seller_message = messageboxSeler.Text;
                using StreamWriter file = File.CreateText(@$"{getCurrDir}\Config\Config.json");
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, jsonObj);
                MessageBox.Show("Данные обновлены", "Выполнено");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        private void refreshTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void refreshbutton_Click(object sender, EventArgs e)
        {
            OnRefresh();
        }
        // Encoding.UTF8.GetString
    }
}
