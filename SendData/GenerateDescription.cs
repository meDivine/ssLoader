using ssLoader.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ssLoader.SendData
{
    public class GenerateDescription
    {
        private string getCurrDir = Directory.GetCurrentDirectory();
        public int CarPrice(string input, string type, int typeMoney, double coefficient)
        {
            string line;
            if (input == null) return 0;
            double carp = 0;
            var file = new StreamReader($@"{getCurrDir}\Config\Cars\{type}.txt");
            double result = 0;
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split(new char[] { ':' });
                string carname = words[0];
                string carprice = words[1];
                if (input.Contains(carname))
                {
                    carp += float.Parse(carprice);
                    result = ((carp / 1000000.0) * coefficient) * typeMoney;
                }
            }
            return (int)result;
        }
        public int CountPrice(float money, int lvl, int MoneyStart, int typeMoney, int typeLevel)
        {
            var moneyCount = (money / 1000000.0) * typeMoney;
            var levelCount = lvl * typeLevel;
            int result = MoneyStart + levelCount + (int)moneyCount;
            return result;
        }


        public async Task<bool> accFilterAsync(string project, int money, int price, int lvl)
        {
            using FileStream sets = File.OpenRead(@$"{getCurrDir}\Config\Servers\{project}.json");
            var jsonConfig = await JsonSerializer.DeserializeAsync<ServerSets>(sets);
            if (money >= jsonConfig.min_virts && price >= jsonConfig.min_price && lvl >= jsonConfig.min_lvl)
                return true;
            else
                return false;
        }

        public async Task<bool> checkStatusSell(string project)
        {
            await using FileStream sets = File.OpenRead(@$"{getCurrDir}\Config\Servers\{project}.json");
            var jsonConfig = await JsonSerializer.DeserializeAsync<ServerSets>(sets);
            if (jsonConfig.SellStatus)
                return true;
            else
                return false;
        }

        public async Task<bool> checkStatusMail(string project, string mail)
        {
            using FileStream sets = File.OpenRead(@$"{getCurrDir}\Config\Servers\{project}.json");
            var jsonConfig = await JsonSerializer.DeserializeAsync<ServerSets>(sets);
            if (mail.Contains("Привязана") && jsonConfig.mail_guard == true)
                return false;
            else
                return true;
        }

        public string MinifyLong(long value)
        {
            if (value >= 0 && value <= 1000)
                return (value / 1D).ToString("0.#") + " вирт $";
            if (value >= 1000 && value <= 10000)
                return (value / 1000D).ToString("#.0") + " тыс. $";
            if (value >= 10000 && value <= 100000)
                return (value / 1000D).ToString("#.0") + " тыс. $";
            if (value >= 100000 && value <= 1000000)
                return (value / 1000D).ToString("#.0") + " тыс. $";
            if (value >= 1000000 && value <= 10000000)
                return (value / 1000000D).ToString("#.0") + " млн. $";
            if (value >= 10000000 && value <= 100000000)
                return (value / 1000000D).ToString("#.0") + " млн. $";
            if (value >= 100000000 && value <= 1000000000)
                return (value / 1000000D).ToString("#.0") + " млн. $";
            if (value >= 1000000000 && value <= 10000000000)
                return (value / 10000000D).ToString("#.0") + " млн. $";
            return value.ToString("#.0");
        }

        public string CarsInfiTitle(string str)
        {
            if (str == "Нет" || str == "нет" || str == null || str == "Не удалось определить")
                return "";
            else
                return $"[ Авто: {str.Replace("\n", " ").Replace("(Владелец)", "").Replace("[Не припарковано]", "").Replace("\t", " ").Replace("загружается при входе", "").Replace("-", "").Replace("(Владелец[Не припарковано]", "")}]";
        }

        public async Task<string> GenerateTitleAsync(int lvl, int money, string cars)
        {
            await using FileStream sets = File.OpenRead(@$"{getCurrDir}\Config\Config.json");
            var jsonConfig = await JsonSerializer.DeserializeAsync<Configurate>(sets);
            string title = jsonConfig.title_message.Replace("%money%", MinifyLong(money)).Replace("%money2%", money.ToString()).Replace("%level%", lvl.ToString()).Replace("%cars%", CarsInfiTitle(cars));
            return title;
        }
        
    }
}
