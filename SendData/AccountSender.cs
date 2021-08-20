using Serilog;
using ssLoader.Json;
using ssLoader.Json.CarPrice;
using ssLoader.SampStoreAPI;
using System;
using System.IO;
using System.Threading;
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
        int successRadmir = 0;
        int successDiamond = 0;
        int successEvolve = 0;
        int successRodina = 0;
        int successTrinity = 0;
        int successGTARP = 0;
        int successAmazing = 0;

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

        private static string MinifyLong(long value)
        {
            if (value >= 0 && value <= 1000)
                return (value / 1D).ToString("0.#") + " вирт $";
            if (value >= 1000 && value <= 10000)
                return (value / 1000D).ToString("#.0") + " к $";

            if (value >= 10000 && value <= 100000)
                return (value / 1000D).ToString("#.0") + " к $";
            if (value >= 100000 && value <= 1000000)
                return (value / 1000D).ToString("#.0") + " к $";

            if (value >= 1000000 && value <= 10000000)
                return (value / 1000000D).ToString("#.0") + " кк $";
            if (value >= 10000000 && value <= 100000000)
                return (value / 1000000D).ToString("#.0") + " кк $";

            if (value >= 100000000 && value <= 1000000000)
                return (value / 1000000D).ToString("#.0") + " кк $";
            if (value >= 1000000000 && value <= 10000000000)
                return (value / 10000000D).ToString("#.0") + " кк $";

            return value.ToString("#.0");
        }

        private string carsInfiTitle(string str)
        {
            if (str == "Нет" || str == "нет" || str == null || str == "Не удалось определить")
                return "";
            else
                return $"[ Авто: {str.Replace("\n", " ").Replace("(Владелец)", "").Replace("[Не припарковано]", "").Replace("\t", " ").Replace("загружается при входе", "").Replace("-", "")}]";
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
                int timing = jsonConfig.timing;
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.File($@"{getCurrDir}\logs\log-.txt", rollingInterval: RollingInterval.Hour).CreateLogger();
                #region ArizonaRP
                // var arizonaFormat = new ArizonaFormat();
                var brainburgGoods = getAccounts.CheckNameArizonaBrainburg(path);
                if (brainburgGoods != null)
                {
                    foreach (var ARZ in brainburgGoods)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Brainburg\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Brainburg\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Brainburg] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Brainburg] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }

                        }
                        Thread.Sleep(timing);
                    }
                }

                var ChandlerGoods = getAccounts.CheckNameArizonaChandler(path);
                if (ChandlerGoods != null)
                {
                    foreach (var ARZ in ChandlerGoods)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Chandler\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Chandler\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Chandler] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Chandler] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }

                        }
                        Thread.Sleep(timing);
                    }
                }
                var Gilbert = getAccounts.CheckNameArizonaGilbert(path);
                if (Gilbert != null)
                {
                    foreach (var ARZ in Gilbert)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Gilbert\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Gilbert\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Gilbert] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Gilbert] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            };
                        }
                        Thread.Sleep(timing);
                    }
                }
                var Glendale = getAccounts.CheckNameArizonaGlendale(path);
                if (Glendale != null)
                {
                    foreach (var ARZ in Glendale)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Glendale\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Glendale\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Glendale] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Glendale] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }

                        }
                        Thread.Sleep(timing);
                    }
                }
                var Kingman = getAccounts.CheckNameArizonaKingman(path);
                if (Kingman != null)
                {
                    foreach (var ARZ in Kingman)
                    {

                        if (File.Exists($@"{path}\Arizona RP\Kingman\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Kingman\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Kingman] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Kingman] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }

                        }
                        Thread.Sleep(timing);
                    }
                }
                var Mesa = getAccounts.CheckNameArizonaMesa(path);
                if (Mesa != null)
                {
                    foreach (var ARZ in Mesa)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Mesa\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Mesa\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Mesa] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Mesa] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }

                        }
                        Thread.Sleep(timing);
                    }
                }
                var Payson = getAccounts.CheckNameArizonaPayson(path);
                if (Payson != null)
                {
                    foreach (var ARZ in Payson)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Payson\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Payson\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Payson] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Payson] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }

                var Phoenix = getAccounts.CheckNameArizonaPhoenix(path);
                if (Phoenix != null)
                {
                    foreach (var ARZ in Phoenix)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Phoenix\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Phoenix\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Phoenix] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Phoenix] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }

                        }
                        Thread.Sleep(timing);
                    }
                }
                var Prescott = getAccounts.CheckNameArizonaPrescott(path);
                if (Prescott != null)
                {
                    foreach (var ARZ in Prescott)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Prescott\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Prescott\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Prescott] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Prescott] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var RedRock = getAccounts.CheckNameArizonaRedRock(path);
                if (RedRock != null)
                {


                    if (RedRock != null)
                    {
                        foreach (var ARZ in RedRock)
                        {
                            if (File.Exists($@"{path}\Arizona RP\Red Rock\goods\{ARZ}.json"))
                            {
                                var text = File.ReadAllText($@"{path}\Arizona RP\Red Rock\goods\{ARZ}.json");
                                var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                                int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                                int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                                int summMoney = price + carpr;
                                var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                                if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                                {
                                    Log.Error($"[ARZ - Red Rock] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                                }
                                else if (resultat.Contains("OK"))
                                {
                                    string[] words = resultat.Split(new char[] { '|' });
                                    string idacc = words[1];
                                    Log.Information($"[ARZ - Red Rock] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                    successArizona++;
                                }
                            }
                            Thread.Sleep(timing);
                        }
                    }
                }
                var SaintRose = getAccounts.CheckNameArizonaSaintRose(path);
                if (SaintRose != null)
                {
                    foreach (var ARZ in SaintRose)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Saint Rose\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Saint Rose\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Saint Rose] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Saint Rose] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }
                        }
                        else continue;
                    }
                }
                var Scottdale = getAccounts.CheckNameArizonaScottdale(path);
                if (Scottdale != null)
                {

                    foreach (var ARZ in Scottdale)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Scottdale\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Scottdale\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Scottdale] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Scottdale] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }

                        }
                        Thread.Sleep(timing);
                    }

                }
                var Surprise = getAccounts.CheckNameArizonaSurprise(path);
                if (Surprise != null)
                {

                    foreach (var ARZ in Surprise)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Surprise\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Surprise\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Surprise] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Surprise] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }
                        }
                        Thread.Sleep(timing);
                    }

                }
                var Tucson = getAccounts.CheckNameArizonaTucson(path);
                if (Tucson != null)
                {
                    foreach (var ARZ in Tucson)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Tucson\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Tucson\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Tucson] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Tucson] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var Winslow = getAccounts.CheckNameArizonaWinslow(path);
                if (Winslow != null)
                {
                    foreach (var ARZ in Winslow)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Winslow\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Winslow\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Winslow] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Winslow] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var Yuma = getAccounts.CheckNameArizonaYuma(path);
                if (Yuma != null)
                {
                    foreach (var ARZ in Yuma)
                    {
                        if (File.Exists($@"{path}\Arizona RP\Yuma\goods\{ARZ}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Arizona RP\Yuma\goods\{ARZ}.json");
                            var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                            int carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Yuma] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARZ - Yuma] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successArizona++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                #endregion
                #region AdvanceRP
                var advanceBlueGoods = getAccounts.CheckNameAdvanceBlue(path);
                if (advanceBlueGoods != null)
                {
                    foreach (var ARP in advanceBlueGoods)
                    {
                        if (File.Exists($@"{path}\Advance RP\Blue\goods\{ARP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Advance RP\Blue\goods\{ARP}.json");
                            var result = JsonSerializer.Deserialize<AdvanceFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.AdvanceRP, moneyPrice.AdvanceRP, levelPrice.AdvanceRP);
                            int carpr = CarPrice(result.cars, "Advance", moneyPrice.AdvanceRP, carPrice.AdvanceRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARP - Blue] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARP - Blue] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successAdvance++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var advanceRedRed = getAccounts.CheckNameAdvanceRed(path);
                if (advanceRedRed != null)
                {

                    foreach (var ARP in advanceRedRed)
                    {
                        if (File.Exists($@"{path}\Advance RP\Red\goods\{ARP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Advance RP\Red\goods\{ARP}.json");
                            var result = JsonSerializer.Deserialize<AdvanceFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.AdvanceRP, moneyPrice.AdvanceRP, levelPrice.AdvanceRP);
                            int carpr = CarPrice(result.cars, "Advance", moneyPrice.AdvanceRP, carPrice.AdvanceRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARP - Red] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARP - Red] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successAdvance++;
                            }
                        }
                        Thread.Sleep(timing);
                    }

                }
                var advanceLimeGoods = getAccounts.CheckNameAdvanceLime(path);
                if (advanceLimeGoods != null)
                {
                    foreach (var ARP in advanceLimeGoods)
                    {
                        if (File.Exists($@"{path}\Advance RP\Lime\goods\{ARP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Advance RP\Lime\goods\{ARP}.json");
                            var result = JsonSerializer.Deserialize<AdvanceFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.AdvanceRP, moneyPrice.AdvanceRP, levelPrice.AdvanceRP);
                            int carpr = CarPrice(result.cars, "Advance", moneyPrice.AdvanceRP, carPrice.AdvanceRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARZ - Lime] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARP - Lime] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successAdvance++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var advanceLimeGreen = getAccounts.CheckNameAdvanceGreen(path);
                if (advanceLimeGreen != null)
                {
                    foreach (var ARP in advanceLimeGreen)
                    {
                        if (File.Exists($@"{path}\Advance RP\Green\goods\{ARP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Advance RP\Green\goods\{ARP}.json");
                            var result = JsonSerializer.Deserialize<AdvanceFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.AdvanceRP, moneyPrice.AdvanceRP, levelPrice.AdvanceRP);
                            int carpr = CarPrice(result.cars, "Advance", moneyPrice.AdvanceRP, carPrice.AdvanceRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[ARP - Green] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[ARP - Green] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successAdvance++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                #endregion
                #region SampRp
                var srpRevo = getAccounts.CheckNameSrpRevolution(path);
                if (srpRevo != null)
                {
                    foreach (var SRP in srpRevo)
                    {
                        if (File.Exists($@"{path}\Samp RP\Revolution\goods\{SRP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Samp RP\Revolution\goods\{SRP}.json");
                            var result = JsonSerializer.Deserialize<SampRPFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.SampRP, moneyPrice.SampRP, levelPrice.SampRP);  
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, price, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[SRP - Revo] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[SRP - Revo] {result.nick} - выставлен на продажу за {price} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successSRP++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var srp02 = getAccounts.CheckNameSrpZerotwo(path);
                if (srp02 != null)
                {
                    foreach (var SRP in srp02)
                    {
                        if (File.Exists($@"{path}\Samp RP\02\goods\{SRP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Samp RP\02\goods\{SRP}.json");
                            var result = JsonSerializer.Deserialize<SampRPFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.SampRP, moneyPrice.SampRP, levelPrice.SampRP);
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, price, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[SRP - 02] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[SRP - 02] {result.nick} - выставлен на продажу за {price} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successSRP++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var Legacy = getAccounts.CheckNameSrplegacytwo(path);
                if (Legacy != null)
                {
                    foreach (var SRP in Legacy)
                    {
                        if (File.Exists($@"{path}\Samp RP\Legacy\goods\{SRP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Samp RP\Legacy\goods\{SRP}.json");
                            var result = JsonSerializer.Deserialize<SampRPFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.SampRP, moneyPrice.SampRP, levelPrice.SampRP);
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, price, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[SRP - Legacy] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[SRP - Legacy] {result.nick} - выставлен на продажу за {price} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successSRP++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var Classic = getAccounts.CheckNameSrpClassic(path);
                if (Classic != null)
                {
                    foreach (var SRP in Classic)
                    {
                        if (File.Exists($@"{path}\Samp RP\Classic\goods\{SRP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Samp RP\Classic\goods\{SRP}.json");
                            var result = JsonSerializer.Deserialize<SampRPFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.SampRP, moneyPrice.SampRP, levelPrice.SampRP);
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, price, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[SRP - Classic] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[SRP - Classic] {result.nick} - выставлен на продажу за {price} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successSRP++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                #endregion
                #region RadmirRP
                var radmir1 = getAccounts.CheckNameRadmir1(path);
                if (radmir1 != null)
                {
                    foreach (var RDR in radmir1)
                    {
                        if (File.Exists($@"{path}\Radmir RP\Server 1\goods\{RDR}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Radmir RP\Server 1\goods\{RDR}.json");
                            var result = JsonSerializer.Deserialize<RadmirFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                            int carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Radmir - 01] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Radmir - 01] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRadmir++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var radmir2 = getAccounts.CheckNameRadmir2(path);
                if (radmir2 != null)
                {
                    foreach (var RDR in radmir2)
                    {
                        if (File.Exists($@"{path}\Radmir RP\Server 2\goods\{RDR}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Radmir RP\Server 2\goods\{RDR}.json");
                            var result = JsonSerializer.Deserialize<RadmirFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                            int carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Radmir - 02] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Radmir - 02] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRadmir++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var radmir3 = getAccounts.CheckNameRadmir3(path);
                if (radmir3 != null)
                {
                    foreach (var RDR in radmir3)
                    {
                        if (File.Exists($@"{path}\Radmir RP\Server 3\goods\{RDR}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Radmir RP\Server 3\goods\{RDR}.json");
                            var result = JsonSerializer.Deserialize<RadmirFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                            int carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Radmir - 03] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Radmir - 03] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRadmir++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var radmir4 = getAccounts.CheckNameRadmir4(path);
                if (radmir4 != null)
                {
                    foreach (var RDR in radmir4)
                    {
                        if (File.Exists($@"{path}\Radmir RP\Server 4\goods\{RDR}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Radmir RP\Server 4\goods\{RDR}.json");
                            var result = JsonSerializer.Deserialize<RadmirFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                            int carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Radmir - 04] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Radmir - 04] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRadmir++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var radmir5 = getAccounts.CheckNameRadmir5(path);
                if (radmir5 != null)
                {
                    foreach (var RDR in radmir5)
                    {
                        if (File.Exists($@"{path}\Radmir RP\Server 5\goods\{RDR}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Radmir RP\Server 5\goods\{RDR}.json");
                            var result = JsonSerializer.Deserialize<RadmirFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                            int carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Radmir - 05] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Radmir - 05] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRadmir++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var radmir6 = getAccounts.CheckNameRadmir6(path);
                if (radmir6 != null)
                {
                    foreach (var RDR in radmir6)
                    {
                        if (File.Exists($@"{path}\Radmir RP\Server 6\goods\{RDR}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Radmir RP\Server 6\goods\{RDR}.json");
                            var result = JsonSerializer.Deserialize<RadmirFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                            int carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Radmir - 06] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Radmir - 06] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRadmir++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var radmir7 = getAccounts.CheckNameRadmir7(path);
                if (radmir7 != null)
                {
                    foreach (var RDR in radmir7)
                    {
                        if (File.Exists($@"{path}\Radmir RP\Server 7\goods\{RDR}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Radmir RP\Server 7\goods\{RDR}.json");
                            var result = JsonSerializer.Deserialize<RadmirFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                            int carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Radmir - 07] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Radmir - 07] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRadmir++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var radmir8 = getAccounts.CheckNameRadmir8(path);
                if (radmir8 != null)
                {
                    foreach (var RDR in radmir8)
                    {
                        if (File.Exists($@"{path}\Radmir RP\Server 8\goods\{RDR}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Radmir RP\Server 8\goods\{RDR}.json");
                            var result = JsonSerializer.Deserialize<RadmirFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                            int carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Radmir - 08] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Radmir - 08] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRadmir++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var radmir9 = getAccounts.CheckNameRadmir9(path);
                if (radmir9 != null)
                {
                    foreach (var RDR in radmir9)
                    {
                        if (File.Exists($@"{path}\Radmir RP\Server 9\goods\{RDR}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Radmir RP\Server 9\goods\{RDR}.json");
                            var result = JsonSerializer.Deserialize<RadmirFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                            int carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Radmir - 09] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Radmir - 09] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRadmir++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var radmir10 = getAccounts.CheckNameRadmir10(path);
                if (radmir10 != null)
                {
                    foreach (var RDR in radmir10)
                    {
                        if (File.Exists($@"{path}\Radmir RP\Server 10\goods\{RDR}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Radmir RP\Server 10\goods\{RDR}.json");
                            var result = JsonSerializer.Deserialize<RadmirFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                            int carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Radmir - 10] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Radmir - 10] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRadmir++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var radmir11 = getAccounts.CheckNameRadmir11(path);
                if (radmir11 != null)
                {
                    foreach (var RDR in radmir11)
                    {
                        if (File.Exists($@"{path}\Radmir RP\Server 11\goods\{RDR}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Radmir RP\Server 11\goods\{RDR}.json");
                            var result = JsonSerializer.Deserialize<RadmirFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                            int carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Radmir - 11] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Radmir - 11] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRadmir++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var radmir12 = getAccounts.CheckNameRadmir12(path);
                if (radmir12 != null)
                {
                    foreach (var RDR in radmir12)
                    {
                        if (File.Exists($@"{path}\Radmir RP\Server 12\goods\{RDR}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Radmir RP\Server 12\goods\{RDR}.json");
                            var result = JsonSerializer.Deserialize<RadmirFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                            int carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Radmir - 12] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Radmir - 12] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRadmir++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var radmir13 = getAccounts.CheckNameRadmir13(path);
                if (radmir13 != null)
                {
                    foreach (var RDR in radmir13)
                    {
                        if (File.Exists($@"{path}\Radmir RP\Server 13\goods\{RDR}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Radmir RP\Server 13\goods\{RDR}.json");
                            var result = JsonSerializer.Deserialize<RadmirFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                            int carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Radmir - 13] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Radmir - 13] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRadmir++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                #endregion
                #region DiamondRP
                var diamondEmerald = getAccounts.CheckNameDiamondEmerald(path);
                if (diamondEmerald != null)
                {
                    foreach (var DRP in diamondEmerald)
                    {
                        if (File.Exists($@"{path}\Diamond RP\Emerald\goods\{DRP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Diamond RP\Emerald\goods\{DRP}.json");
                            var result = JsonSerializer.Deserialize<DiamondFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.DiamondRP, moneyPrice.DiamondRP, levelPrice.DiamondRP);
                            int carpr = CarPrice(result.cars, "Diamond", moneyPrice.DiamondRP, carPrice.DiamondRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Diamond - Emerald] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Diamond - Emerald] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successDiamond++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var diamondTrilliant = getAccounts.CheckNameDiamondTrilliant(path);
                if (diamondTrilliant != null)
                {
                    foreach (var DRP in diamondTrilliant)
                    {
                        if (File.Exists($@"{path}\Diamond RP\Trilliant\goods\{DRP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Diamond RP\Trilliant\goods\{DRP}.json");
                            var result = JsonSerializer.Deserialize<DiamondFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.DiamondRP, moneyPrice.DiamondRP, levelPrice.DiamondRP);
                            int carpr = CarPrice(result.cars, "Diamond", moneyPrice.DiamondRP, carPrice.DiamondRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Diamond - Trilliant] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Diamond - Trilliant] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successDiamond++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var diamondRuby = getAccounts.CheckNameDiamondRuby(path);
                if (diamondRuby != null)
                {
                    foreach (var DRP in diamondRuby)
                    {
                        if (File.Exists($@"{path}\Diamond RP\Trilliant\goods\{DRP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Diamond RP\Trilliant\goods\{DRP}.json");
                            var result = JsonSerializer.Deserialize<DiamondFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.DiamondRP, moneyPrice.DiamondRP, levelPrice.DiamondRP);
                            int carpr = CarPrice(result.cars, "Diamond", moneyPrice.DiamondRP, carPrice.DiamondRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Diamond - Ruby] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Diamond - Ruby] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successDiamond++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                #endregion
                #region EvolveRP
                var Evolve01 = getAccounts.CheckNameEvolve1(path);
                if (Evolve01 != null)
                {
                    foreach (var ERP in Evolve01)
                    {
                        if (File.Exists($@"{path}\Evolve RP\01\goods\{ERP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Evolve RP\01\goods\{ERP}.json");
                            var result = JsonSerializer.Deserialize<EvolveFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.EvolveRP, moneyPrice.EvolveRP, levelPrice.EvolveRP);
                            int carpr = CarPrice(result.cars, "Evolve", moneyPrice.EvolveRP, carPrice.EvolveRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Evolve RP - 01] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Evolve RP - 01] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successEvolve++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var Evolve02 = getAccounts.CheckNameEvolve2(path);
                if (Evolve02 != null)
                {
                    foreach (var ERP in Evolve02)
                    {
                        if (File.Exists($@"{path}\Evolve RP\01\goods\{ERP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Evolve RP\01\goods\{ERP}.json");
                            var result = JsonSerializer.Deserialize<EvolveFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.EvolveRP, moneyPrice.EvolveRP, levelPrice.EvolveRP);
                            int carpr = CarPrice(result.cars, "Evolve", moneyPrice.EvolveRP, carPrice.EvolveRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Evolve RP - 02] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Evolve RP - 02] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successEvolve++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var Evolve03 = getAccounts.CheckNameEvolve3(path);
                if (Evolve03 != null)
                {
                    foreach (var ERP in Evolve03)
                    {
                        if (File.Exists($@"{path}\Evolve RP\03\goods\{ERP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Evolve RP\03\goods\{ERP}.json");
                            var result = JsonSerializer.Deserialize<EvolveFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.EvolveRP, moneyPrice.EvolveRP, levelPrice.EvolveRP);
                            int carpr = CarPrice(result.cars, "Evolve", moneyPrice.EvolveRP, carPrice.EvolveRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Evolve RP - 03] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Evolve RP - 03] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successEvolve++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                #endregion
                #region RodinaRP
                var RodinaVO = getAccounts.CheckNameRodinaVO(path);
                if (RodinaVO != null)
                {
                    foreach (var RRP in RodinaVO)
                    {
                        if (File.Exists($@"{path}\Rodina RP\Восточный Округ\goods\{RRP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Rodina RP\Восточный Округ\goods\{RRP}.json");
                            var result = JsonSerializer.Deserialize<RodinaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RodinaRP, moneyPrice.RodinaRP, levelPrice.RodinaRP);
                            int carpr = CarPrice(result.cars, "Rodina", moneyPrice.RodinaRP, carPrice.RodinaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Rodina RP - ВО] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Rodina RP - ВО] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRodina++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var RodinaSO = getAccounts.CheckNameRodinaSO(path);
                if (RodinaSO != null)
                {
                    foreach (var RRP in RodinaSO)
                    {
                        if (File.Exists($@"{path}\Rodina RP\Северный Округ\goods\{RRP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Rodina RP\Северный Округ\goods\{RRP}.json");
                            var result = JsonSerializer.Deserialize<RodinaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RodinaRP, moneyPrice.RodinaRP, levelPrice.RodinaRP);
                            int carpr = CarPrice(result.cars, "Rodina", moneyPrice.RodinaRP, carPrice.RodinaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Rodina RP - СО] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Rodina RP - СО] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRodina++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var RodinaCO = getAccounts.CheckNameRodinaCO(path);
                if (RodinaCO != null)
                {
                    foreach (var RRP in RodinaCO)
                    {
                        if (File.Exists($@"{path}\Rodina RP\Центральный Округ\goods\{RRP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Rodina RP\Центральный Округ\goods\{RRP}.json");
                            var result = JsonSerializer.Deserialize<RodinaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RodinaRP, moneyPrice.RodinaRP, levelPrice.RodinaRP);
                            int carpr = CarPrice(result.cars, "Rodina", moneyPrice.RodinaRP, carPrice.RodinaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Rodina RP - ЦО] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Rodina RP - ЦО] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRodina++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var RodinaYO = getAccounts.CheckNameRodinaYO(path);
                if (RodinaYO != null)
                {
                    foreach (var RRP in RodinaYO)
                    {
                        if (File.Exists($@"{path}\Rodina RP\Южный Округ\goods\{RRP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Rodina RP\Южный Округ\goods\{RRP}.json");
                            var result = JsonSerializer.Deserialize<RodinaFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.RodinaRP, moneyPrice.RodinaRP, levelPrice.RodinaRP);
                            int carpr = CarPrice(result.cars, "Rodina", moneyPrice.RodinaRP, carPrice.RodinaRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Rodina RP - ЮО] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Rodina RP - ЮО] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successRodina++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                #endregion
                #region TrinityRP
                var trinity1 = getAccounts.CheckNameTrinity1(path);
                if (trinity1 != null)
                {
                    foreach (var TRP in trinity1)
                    {
                        if (File.Exists($@"{path}\Trinity RP\01\goods\{TRP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Trinity RP\01\goods\{TRP}.json");
                            var result = JsonSerializer.Deserialize<TrinityFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.TrinityRP, moneyPrice.TrinityRP, levelPrice.TrinityRP);
                            int carpr = CarPrice(result.cars, "Trinity", moneyPrice.TrinityRP, carPrice.TrinityRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Trinity - 01] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Trinity - 01] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successTrinity++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var trinity2 = getAccounts.CheckNameTrinity2(path);
                if (trinity2 != null)
                {
                    foreach (var TRP in trinity2)
                    {
                        if (File.Exists($@"{path}\Trinity RP\02\goods\{TRP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Trinity RP\02\goods\{TRP}.json");
                            var result = JsonSerializer.Deserialize<TrinityFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.TrinityRP, moneyPrice.TrinityRP, levelPrice.TrinityRP);
                            int carpr = CarPrice(result.cars, "Trinity", moneyPrice.TrinityRP, carPrice.TrinityRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[Trinity - 02] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[Trinity - 02] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successTrinity++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                #endregion
                #region GTARP
                var gtarp1 = getAccounts.CheckNameGTARP1(path);
                if (gtarp1 != null)
                {
                    foreach (var GTARP in gtarp1)
                    {
                        if (File.Exists($@"{path}\GTA RP\#1\goods\{GTARP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\GTA RP\#1\goods\{GTARP}.json");
                            var result = JsonSerializer.Deserialize<GTARPFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.GTARP, moneyPrice.GTARP, levelPrice.GTARP);
                            int carpr = CarPrice(result.cars, "GTARP", moneyPrice.GTARP, carPrice.GTARP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[GTA RP - #1] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[GTA RP - #1] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successGTARP++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var gtarp2 = getAccounts.CheckNameGTARP2(path);
                if (gtarp2 != null)
                {
                    foreach (var GTARP in gtarp2)
                    {
                        if (File.Exists($@"{path}\GTA RP\#2\goods\{GTARP}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\GTA RP\#2\goods\{GTARP}.json");
                            var result = JsonSerializer.Deserialize<GTARPFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.GTARP, moneyPrice.GTARP, levelPrice.GTARP);
                            int carpr = CarPrice(result.cars, "GTARP", moneyPrice.GTARP, carPrice.GTARP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[GTA RP - #2] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[GTA RP - #2] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successGTARP++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                #endregion
                #region AmazingRP
                var amazingred = getAccounts.CheckNameAmazingRed(path);
                if (amazingred != null)
                {
                    foreach (var Amazing in amazingred)
                    {
                        if (File.Exists($@"{path}\Amazing RP\Red\goods\{Amazing}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Amazing RP\Red\goods\{Amazing}.json");
                            var result = JsonSerializer.Deserialize<AmazingFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.AmazingRP, moneyPrice.AmazingRP, levelPrice.AmazingRP);
                            int carpr = CarPrice(result.cars, "Amazing", moneyPrice.AmazingRP, carPrice.AmazingRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[AMAZING RP - RED] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[AMAZING RP - RED] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successAmazing++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var amazingyellow = getAccounts.CheckNameAmazingYellow(path);
                if (amazingyellow != null)
                {
                    foreach (var Amazing in amazingyellow)
                    {
                        if (File.Exists($@"{path}\Amazing RP\Yellow\goods\{Amazing}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Amazing RP\Yellow\goods\{Amazing}.json");
                            var result = JsonSerializer.Deserialize<AmazingFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.AmazingRP, moneyPrice.AmazingRP, levelPrice.AmazingRP);
                            int carpr = CarPrice(result.cars, "Amazing", moneyPrice.AmazingRP, carPrice.AmazingRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[AMAZING RP - YELLOW] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[AMAZING RP - YELLOW] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successAmazing++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var amazinggreen = getAccounts.CheckNameAmazingGreen(path);
                if (amazinggreen != null)
                {
                    foreach (var Amazing in amazinggreen)
                    {
                        if (File.Exists($@"{path}\Amazing RP\Green\goods\{Amazing}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Amazing RP\Green\goods\{Amazing}.json");
                            var result = JsonSerializer.Deserialize<AmazingFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.AmazingRP, moneyPrice.AmazingRP, levelPrice.AmazingRP);
                            int carpr = CarPrice(result.cars, "Amazing", moneyPrice.AmazingRP, carPrice.AmazingRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[AMAZING RP - GREEN] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[AMAZING RP - GREEN] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successAmazing++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }

                var amazingazure = getAccounts.CheckNameAmazingAzure(path);
                if (amazingazure != null)
                {
                    foreach (var Amazing in amazingazure)
                    {
                        if (File.Exists($@"{path}\Amazing RP\Azure\goods\{Amazing}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Amazing RP\Azure\goods\{Amazing}.json");
                            var result = JsonSerializer.Deserialize<AmazingFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.AmazingRP, moneyPrice.AmazingRP, levelPrice.AmazingRP);
                            int carpr = CarPrice(result.cars, "Amazing", moneyPrice.AmazingRP, carPrice.AmazingRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[AMAZING RP - AZURE] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[AMAZING RP - AZURE] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successAmazing++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                var amazingsilver = getAccounts.CheckNameAmazingSilver(path);
                if (amazingsilver != null)
                {
                    foreach (var Amazing in amazingsilver)
                    {
                        if (File.Exists($@"{path}\Amazing RP\Silver\goods\{Amazing}.json"))
                        {
                            var text = File.ReadAllText($@"{path}\Amazing RP\Silver\goods\{Amazing}.json");
                            var result = JsonSerializer.Deserialize<AmazingFormat>(text.Replace("\r\n", ""));
                            int price = CountPrice(result.money, result.lvl, startPrice.AmazingRP, moneyPrice.AmazingRP, levelPrice.AmazingRP);
                            int carpr = CarPrice(result.cars, "Amazing", moneyPrice.AmazingRP, carPrice.AmazingRP);
                            int summMoney = price + carpr;
                            var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $" {result.lvl} уровень | {MinifyLong(result.money)} на руках {carsInfiTitle(result.cars)}"));
                            if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                            {
                                Log.Error($"[AMAZING RP - SILVER] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                            }
                            else if (resultat.Contains("OK"))
                            {
                                string[] words = resultat.Split(new char[] { '|' });
                                string idacc = words[1];
                                Log.Information($"[AMAZING RP - SILVER] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                                successAmazing++;
                            }
                        }
                        Thread.Sleep(timing);
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                int allaccs = successArizona + successAdvance + successAmazing + successDiamond + successEvolve + successGTARP + successRadmir + successRodina + successSRP + successTrinity;
                MessageBox.Show($"Залито: \nArizona RP {successArizona}\nAdvance RP {successAdvance}\nSamp-RP {successSRP}]\nRadmir-RP {successRadmir}\nDiamond-RP {successDiamond}\nEvolve-RP {successEvolve}\nRodina-RP {successRodina}\nTrinity-RP {successTrinity}\nGTA-RP {successGTARP}\nAmazing-RP {successAmazing}\nВсего - {allaccs}", "Обработка завершена", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
    }
}
