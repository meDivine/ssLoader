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
        private string getCurrDir = Directory.GetCurrentDirectory();

        public Config()
        {
            onStart();
            InitializeComponent();
        }

        private void refreshbutton_Click(object sender, EventArgs e)
        {
            try
            {
                string json = File.ReadAllText(@$"{getCurrDir}\Config\Config.json");
                dynamic jsonObj = JsonConvert.DeserializeObject<Configurate>(json);
                jsonObj.api_key = refreshTextBox.Text;
                using StreamWriter file = File.CreateText(@$"{getCurrDir}\Config\Config.json");
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(file, jsonObj);
                MessageBox.Show("API - ключ обновлен", "Выполнено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");    
            }

        }

        private void refreshTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }
        private async void onStart()
        {
            using FileStream configFile = File.OpenRead(@$"{getCurrDir}\Config\Config.json");
            var cfg = await System.Text.Json.JsonSerializer.DeserializeAsync<Configurate>(configFile);
            refreshTextBox.Text = cfg.api_key;
        }
    }
}
