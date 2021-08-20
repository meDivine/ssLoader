using ssLoader.Json;
using ssLoader.Json.CarPrice;
using ssLoader.SampStoreAPI;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ssLoader.Arizona
{
    public class AccountSender
    {
        private string getCurrDir = Directory.GetCurrentDirectory();
        int successArizona = 0;

        private int CountPrice(float money, int lvl, int startprice, int typeMoney, int typeLevel)
        {
            var moneyCount = (money / 1000000.0) * typeMoney;
            var levelCount = lvl * typeLevel;
            int result = startprice + levelCount + (int)moneyCount;
            return result;
        }

        private int CarPrice(string input, string type, int typeMoney, double coefficient)
        {
            string line;

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

        public string minifyLong(long value)
        {
            if (value >= 100000000000)
                return (value / 1000000000).ToString("#,0") + "ккк";
            if (value >= 10000000000)
                return (value / 1000000000D).ToString("0.#") + "ккк";
            if (value >= 100000000)
                return (value / 1000000).ToString("#.0") + "кк";
            if (value >= 10000000)
                return (value / 1000000D).ToString("0.#") + "кк";
            if (value >= 1000000)
                return (value / 1000D).ToString("#.0") + "к";
            if (value >= 100000)
                return (value / 1000).ToString("#.0") + "к";
            if (value >= 100000)
                return (value / 1000D).ToString("#.0") + "к";
            if (value >= 10000)
                return (value / 1000D).ToString("0.#") + "к";
            if (value >= 0)
                return (value / 1).ToString("#.0") + "вирт";
            if (value >= 1000)
                return (value / 100D).ToString("#.0") + "к";
            return value.ToString("#.0");
        }

        public async Task SendToSS(string path)
        {
            try
            {
                #region создание экземпляров классов
                var getAccounts = new GetAccounts();
                var accountSender = new AddAccount();
               // var config = new Json.Config();
                //var priceLevel = new Level();
                //var priceStart = new StartPrice();
                #endregion

                #region Десериализация json файлов
                using FileStream api_key = File.OpenRead(@$"{getCurrDir}\Config\Config.json");
                var jsonConfig = await JsonSerializer.DeserializeAsync<Json.Config>(api_key);

                using FileStream openStream = File.OpenRead(@$"{getCurrDir}\Config\Money.json");
                var moneyPrice = await JsonSerializer.DeserializeAsync<Money>(openStream);

                using FileStream levelPriceJson = File.OpenRead(@$"{getCurrDir}\Config\Levels.json");
                var levelPrice = await JsonSerializer.DeserializeAsync<Level>(levelPriceJson);

                using FileStream startPriceJson = File.OpenRead(@$"{getCurrDir}\Config\StartPrice.json");
                var startPrice = await JsonSerializer.DeserializeAsync<StartPrice>(startPriceJson);
                #endregion
                string line;
                #region ArizonaRP
                // var arizonaFormat = new ArizonaFormat();
                var brainburgGoods = getAccounts.CheckNameArizonaBrainburg(path);
                foreach (var ARZ in brainburgGoods)
                {
                    if (File.Exists($@"{path}\Arizona RP\Brainburg\goods\{ARZ}.json"))
                    {
                        float carp = 0;
                        var text = File.ReadAllText($@"{path}\Arizona RP\Brainburg\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        /* var text1 = File.ReadAllText($@"C:\Users\Fesenko.AP\source\repos\BUTCHERS228\ssLoader\bin\Debug\net5.0-windows\Config\Config.json");
                         var result1 = JsonSerializer.Deserialize<Config>(text1);*/
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, 0.6);
                        
                            MessageBox.Show($"{result.nick} ник | {result.money} | цена акк {price + carpr} | лвл {result.lvl} | Кары {carpr} | {result.cars}  | вирт {minifyLong(result.money)}");
                        successArizona++;
                    }
                    // await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                }
                /*
                var ChandlerGoods = getAccounts.CheckNameArizonaChandler(path);
                foreach (var ARZ in ChandlerGoods)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Chandler\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    //MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var Gilbert = getAccounts.CheckNameArizonaGilbert(path);
                foreach (var ARZ in Gilbert)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Gilbert\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    // MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var Glendale = getAccounts.CheckNameArizonaGlendale(path);
                foreach (var ARZ in Glendale)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Glendale\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    //MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var Kingman = getAccounts.CheckNameArizonaGlendale(path);
                foreach (var ARZ in Kingman)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Kingman\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    //MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var Mesa = getAccounts.CheckNameArizonaMesa(path);
                foreach (var ARZ in Mesa)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Mesa\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    // MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var Payson = getAccounts.CheckNameArizonaPayson(path);
                foreach (var ARZ in Payson)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Mesa\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    //MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var Phoenix = getAccounts.CheckNameArizonaPhoenix(path);
                foreach (var ARZ in Payson)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Phoenix\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    //MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var Prescott = getAccounts.CheckNameArizonaPrescott(path);
                foreach (var ARZ in Payson)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Phoenix\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    // MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var RedRock = getAccounts.CheckNameArizonaRedRock(path);
                foreach (var ARZ in Payson)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Red Rock\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    //MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var SaintRose = getAccounts.CheckNameArizonaSaintRose(path);
                foreach (var ARZ in Payson)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Saint Rose\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    //MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var Scottdale = getAccounts.CheckNameArizonaScottdale(path);
                foreach (var ARZ in Payson)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Scottdale\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    // MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var Surprise = getAccounts.CheckNameArizonaScottdale(path);
                foreach (var ARZ in Payson)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Scottdale\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    //MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var Tucson = getAccounts.CheckNameArizonaTucson(path);
                foreach (var ARZ in Payson)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Tucson\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    //MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var Winslow = getAccounts.CheckNameArizonaWinslow(path);
                foreach (var ARZ in Payson)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Winslow\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    //MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }
                var Yuma = getAccounts.CheckNameArizonaYuma(path);
                foreach (var ARZ in Payson)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Yuma\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                    //MessageBox.Show($"ник {result.nick} Пароль: {result.password} Цена виртов {priceMoney} Виртов {result.money} Цена за лям {moneyPrice.ArizonaRP}");
                }*/
                #endregion

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                MessageBox.Show($"Я все {successArizona}");
            }
        }
    }
}
