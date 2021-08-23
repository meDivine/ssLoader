using Serilog;
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
        int successRadmir = 0;
        int successDiamond = 0;
        int successEvolve = 0;
        int successRodina = 0;
        int successTrinity = 0;
        int successGTARP = 0;
        int successAmazing = 0;
        private int arizonasmoney = 0;

        private int CountPrice(float money, int lvl, int MoneyStart, int typeMoney, int typeLevel)
        {
            var moneyCount = (money / 1000000.0) * typeMoney;
            var levelCount = lvl * typeLevel;
            int result = MoneyStart + levelCount + (int)moneyCount;
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

        private async Task<bool> accFilterAsync(string project, int money, int price, int lvl)
        {
            using FileStream sets = File.OpenRead(@$"{getCurrDir}\Config\Servers\{project}.json");
            var jsonConfig = await JsonSerializer.DeserializeAsync<ServerSets>(sets);
            if (money >= jsonConfig.min_virts && price >= jsonConfig.min_price && lvl >= jsonConfig.min_lvl)
                return true;
            else
                return false;
        }

        private async Task<bool> checkStatusSell(string project)
        {
            using FileStream sets = File.OpenRead(@$"{getCurrDir}\Config\Servers\{project}.json");
            var jsonConfig = await JsonSerializer.DeserializeAsync<ServerSets>(sets);
            if (jsonConfig.SellStatus)
                return true;
            else
                return false;
        }

        private string MinifyLong(long value)
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

        private string carsInfiTitle(string str)
        {
            if (str == "Нет" || str == "нет" || str == null || str == "Не удалось определить")
                return "";
            else
                return $"[ Авто: {str.Replace("\n", " ").Replace("(Владелец)", "").Replace("[Не припарковано]", "").Replace("\t", " ").Replace("загружается при входе", "").Replace("-", "").Replace("(Владелец[Не припарковано]", "")}]";
        }

        private async Task SellTemplate(string Project, string server, string path, string nick)
        {
            var getAccounts = new GetAccounts();
            var accountSender = new AddAccount();
            using FileStream api_key = File.OpenRead(@$"{getCurrDir}\Config\Config.json");
            var jsonConfig = await JsonSerializer.DeserializeAsync<Configurate>(api_key);

            using FileStream openStream = File.OpenRead(@$"{getCurrDir}\Config\Money.json");
            var moneyPrice = await JsonSerializer.DeserializeAsync<Money>(openStream);

            using FileStream levelPriceJson = File.OpenRead(@$"{getCurrDir}\Config\Levels.json");
            var levelPrice = await JsonSerializer.DeserializeAsync<Level>(levelPriceJson);

            using FileStream MoneyStartJson = File.OpenRead(@$"{getCurrDir}\Config\StartPrice.json");
            var MoneyStart = await JsonSerializer.DeserializeAsync<MoneyStart>(MoneyStartJson);

            using FileStream carPriceJson = File.OpenRead(@$"{getCurrDir}\Config\Cars\Coefficient.json");
            var carPrice = await JsonSerializer.DeserializeAsync<Prices>(carPriceJson);

            if (File.Exists($@"{path}\{Project}\{server}\goods\{nick}.json"))
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.File($@"{getCurrDir}\logs\log-.txt", rollingInterval: RollingInterval.Hour).CreateLogger();
                var text = File.ReadAllText($@"{path}\{Project}\{server}\goods\{nick}.json");
                var result = JsonSerializer.Deserialize<AccountFormat>(text.Replace("\r\n", ""));
                int price = 0;
                int carpr = 0;
                int summMoney = 0;
                if (Project == "Arizona RP")
                {
                    price = CountPrice(result.money, result.lvl, MoneyStart.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                    carpr = CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                    summMoney = price + carpr;
                    arizonasmoney += result.money;
                    successArizona++;
                }

                if (Project == "Amazing RP")
                {
                    price = CountPrice(result.money, result.lvl, MoneyStart.AmazingRP, moneyPrice.AmazingRP, levelPrice.AmazingRP);
                    carpr = CarPrice(result.cars, "Amazing", moneyPrice.AmazingRP, carPrice.AmazingRP);
                    summMoney = price + carpr;
                    successAmazing++;
                }
                if (Project == "Advance RP")
                {
                    price = CountPrice(result.money, result.lvl, MoneyStart.AdvanceRP, moneyPrice.AdvanceRP, levelPrice.AdvanceRP);
                    carpr = CarPrice(result.cars, "Advance", moneyPrice.AdvanceRP, carPrice.AdvanceRP);
                    summMoney = price + carpr;
                    successAdvance++;
                }
                if (Project == "Diamond RP")
                {
                    price = CountPrice(result.money, result.lvl, MoneyStart.DiamondRP, moneyPrice.DiamondRP, levelPrice.DiamondRP);
                    carpr = CarPrice(result.cars, "Diamond", moneyPrice.DiamondRP, carPrice.DiamondRP);
                    summMoney = price + carpr;
                    successDiamond++;
                }
                if (Project == "Evolve RP")
                {
                    price = CountPrice(result.money, result.lvl, MoneyStart.EvolveRP, moneyPrice.EvolveRP, levelPrice.EvolveRP);
                    carpr = CarPrice(result.cars, "Evolve", moneyPrice.EvolveRP, carPrice.EvolveRP);
                    summMoney = price + carpr;
                    successEvolve++;
                }
                if (Project == "GTA RP")
                {
                    price = CountPrice(result.money, result.lvl, MoneyStart.GTARP, moneyPrice.GTARP, levelPrice.GTARP);
                    carpr = CarPrice(result.cars, "GTARP", moneyPrice.GTARP, carPrice.GTARP);
                    summMoney = price + carpr;
                    successGTARP++;
                }
                if (Project == "Radmir RP")
                {
                    price = CountPrice(result.money, result.lvl, MoneyStart.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                    carpr = CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                    summMoney = price + carpr;
                    successRadmir++;
                }
                if (Project == "Rodina RP")
                {
                    price = CountPrice(result.money, result.lvl, MoneyStart.RodinaRP, moneyPrice.RodinaRP, levelPrice.RodinaRP);
                    carpr = CarPrice(result.cars, "Rodina", moneyPrice.RodinaRP, carPrice.RodinaRP);
                    summMoney = price + carpr;
                    successRodina++;
                }
                if (Project == "Trinity RP")
                {
                    price = CountPrice(result.money, result.lvl, MoneyStart.TrinityRP, moneyPrice.TrinityRP, levelPrice.TrinityRP);
                    carpr = CarPrice(result.cars, "Trinity", moneyPrice.TrinityRP, carPrice.TrinityRP);
                    summMoney = price + carpr;
                    successTrinity++;
                }
                if (Project == "Samp RP")
                {
                    price = CountPrice(result.money, result.lvl, MoneyStart.SampRP, moneyPrice.SampRP, levelPrice.SampRP);
                    summMoney = price;
                    successSRP++;
                }

                if (await accFilterAsync(Project, result.money, summMoney, result.lvl))
                {

                    MessageBox.Show($"{Project} | Сервер {server} Ник {result.nick} Цена: {summMoney} денег: {MinifyLong(result.money)} уровень {result.lvl}");
                    successArizona++;
                    
                }
                /* var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, $"✔️ {result.lvl} уровень ✔️ {MinifyLong(result.money)} на руках ✔️ {carsInfiTitle(result.cars)}"));
                 if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                 {
                     Log.Error($"[ARZ - {server}] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                 }
                 else if (resultat.Contains("OK"))
                 {
                     string[] words = resultat.Split(new char[] { '|' });
                     string idacc = words[1];
                     Log.Information($"[ARZ - {server}] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                     successArizona++;
                 }*/
            }
            int timing = jsonConfig.timing;
        }

        public async Task SendToSS(string path)
        {
            try
            {
                var getAccounts = new GetAccounts();
                var accountSender = new AddAccount();
                #region ArizonaRP
                if (await checkStatusSell("Arizona RP"))
                {

                    var brainburgGoods = getAccounts.CheckNameArizonaBrainburg(path);
                    var ChandlerGoods = getAccounts.CheckNameArizonaChandler(path);
                    var Gilbert = getAccounts.CheckNameArizonaGilbert(path);
                    var Glendale = getAccounts.CheckNameArizonaGlendale(path);
                    var Kingman = getAccounts.CheckNameArizonaKingman(path);
                    var Mesa = getAccounts.CheckNameArizonaMesa(path);
                    var Payson = getAccounts.CheckNameArizonaPayson(path);
                    var Phoenix = getAccounts.CheckNameArizonaPhoenix(path);
                    var Prescott = getAccounts.CheckNameArizonaPrescott(path);
                    var RedRock = getAccounts.CheckNameArizonaRedRock(path);
                    var SaintRose = getAccounts.CheckNameArizonaSaintRose(path);
                    var Scottdale = getAccounts.CheckNameArizonaScottdale(path);
                    var Surprise = getAccounts.CheckNameArizonaSurprise(path);
                    var Tucson = getAccounts.CheckNameArizonaTucson(path);
                    var Winslow = getAccounts.CheckNameArizonaWinslow(path);
                    var Yuma = getAccounts.CheckNameArizonaYuma(path);
                    var ShowLow = getAccounts.CheckNameArizonaShowLow(path);
                    if (brainburgGoods != null)
                    {
                        foreach (var nick in brainburgGoods)
                        {
                            await SellTemplate("Arizona RP", "Brainburg", path, nick);
                        }
                    }
                    if (ChandlerGoods != null)
                    {
                        foreach (var nick in ChandlerGoods)
                        {
                            await SellTemplate("Arizona RP", "Chandler", path, nick);
                        }
                    }
                    if (Gilbert != null)
                    {
                        foreach (var nick in Gilbert)
                        {
                            await SellTemplate("Arizona RP", "Gilbert", path, nick);
                        }
                    }
                    if (Glendale != null)
                    {
                        foreach (var nick in Glendale)
                        {
                            await SellTemplate("Arizona RP", "Glendale", path, nick);
                        }
                    }
                    if (Kingman != null)
                    {
                        foreach (var nick in Kingman)
                        {
                            await SellTemplate("Arizona RP", "Kingman", path, nick);
                        }
                    }
                    if (Mesa != null)
                    {
                        foreach (var nick in Mesa)
                        {
                            await SellTemplate("Arizona RP", "Mesa", path, nick);
                        }
                    }
                    if (Payson != null)
                    {
                        foreach (var nick in Payson)
                        {
                            await SellTemplate("Arizona RP", "Phoenix", path, nick);
                        }
                    }
                    if (Phoenix != null)
                    {
                        foreach (var nick in Phoenix)
                        {
                            await SellTemplate("Arizona RP", "Phoenix", path, nick);
                        }
                    }
                    if (Prescott != null)
                    {
                        foreach (var nick in Prescott)
                        {
                            await SellTemplate("Arizona RP", "Prescott", path, nick);
                        }
                    }
                    if (RedRock != null)
                    {
                        foreach (var nick in RedRock)
                        {
                            await SellTemplate("Arizona RP", "Red Rock", path, nick);
                        }
                    }
                    if (SaintRose != null)
                    {
                        foreach (var nick in SaintRose)
                        {
                            await SellTemplate("Arizona RP", "Saint Rose", path, nick);
                        }
                    }
                    if (Scottdale != null)
                    {
                        foreach (var nick in Scottdale)
                        {
                            await SellTemplate("Arizona RP", "Scottdale", path, nick);
                        }
                    }
                    if (Surprise != null)
                    {
                        foreach (var nick in Surprise)
                        {
                            await SellTemplate("Arizona RP", "Surprise", path, nick);
                        }
                    }
                    if (Tucson != null)
                    {
                        foreach (var nick in Tucson)
                        {
                            await SellTemplate("Arizona RP", "Tucson", path, nick);
                        }
                    }
                    if (Winslow != null)
                    {
                        foreach (var nick in Winslow)
                        {
                            await SellTemplate("Arizona RP", "Winslow", path, nick);
                        }
                    }
                    if (Yuma != null)
                    {
                        foreach (var nick in Yuma)
                        {
                            await SellTemplate("Arizona RP", "Yuma", path, nick);
                        }
                    }
                    if (ShowLow != null)
                    {
                        foreach (var nick in ShowLow)
                        {
                            await SellTemplate("Arizona RP", "Show Low", path, nick);
                        }
                    }
                }
                #endregion
                #region Advance RP
                if (await checkStatusSell("Advance RP"))
                {
                    var advanceBlueGoods = getAccounts.CheckNameAdvanceBlue(path);
                    var advanceRedRed = getAccounts.CheckNameAdvanceRed(path);
                    var advanceLimeGoods = getAccounts.CheckNameAdvanceLime(path);
                    var advanceLimeGreen = getAccounts.CheckNameAdvanceGreen(path);
                    if (advanceBlueGoods != null)
                    {
                        foreach (var nick in advanceBlueGoods)
                        {
                            await SellTemplate("Advance RP", "Blue", path, nick);
                        }
                    }
                    if (advanceRedRed != null)
                    {
                        foreach (var nick in advanceRedRed)
                        {
                            await SellTemplate("Advance RP", "Red", path, nick);
                        }
                    }
                    if (advanceLimeGoods != null)
                    {
                        foreach (var nick in advanceLimeGoods)
                        {
                            await SellTemplate("Advance RP", "Lime", path, nick);
                        }
                    }
                    if (advanceLimeGreen != null)
                    {
                        foreach (var nick in advanceLimeGreen)
                        {
                            await SellTemplate("Advance RP", "Green", path, nick);
                        }
                    }
                }
                #endregion
                #region Samp Rp
                if (await checkStatusSell("Samp RP"))
                {
                    var srpRevo = getAccounts.CheckNameSrpRevolution(path);
                    var srp02 = getAccounts.CheckNameSrpZerotwo(path);
                    var Legacy = getAccounts.CheckNameSrplegacytwo(path);
                    var Classic = getAccounts.CheckNameSrpClassic(path);
                    if (srpRevo != null)
                    {
                        foreach (var nick in srpRevo)
                        {
                            await SellTemplate("Samp RP", "Revolution", path, nick);
                        }
                    }
                    if (srp02 != null)
                    {
                        foreach (var nick in srp02)
                        {
                            await SellTemplate("Samp RP", "02", path, nick);
                        }
                    }
                    if (Legacy != null)
                    {
                        foreach (var nick in Legacy)
                        {
                            await SellTemplate("Samp RP", "Legacy", path, nick);
                        }
                    }
                    if (Classic != null)
                    {
                        foreach (var nick in Classic)
                        {
                            await SellTemplate("AdvaSampnce RP", "Classic", path, nick);
                        }
                    }
                }
                #endregion
                #region Radmir RP
                if (await checkStatusSell("Radmir RP"))
                {
                    var radmir1 = getAccounts.CheckNameRadmir1(path);
                    var radmir2 = getAccounts.CheckNameRadmir2(path);
                    var radmir3 = getAccounts.CheckNameRadmir3(path);
                    var radmir4 = getAccounts.CheckNameRadmir4(path);
                    var radmir5 = getAccounts.CheckNameRadmir5(path);
                    var radmir6 = getAccounts.CheckNameRadmir6(path);
                    var radmir7 = getAccounts.CheckNameRadmir7(path);
                    var radmir8 = getAccounts.CheckNameRadmir8(path);
                    var radmir9 = getAccounts.CheckNameRadmir9(path);
                    var radmir10 = getAccounts.CheckNameRadmir10(path);
                    var radmir11 = getAccounts.CheckNameRadmir11(path);
                    var radmir12 = getAccounts.CheckNameRadmir12(path);
                    var radmir13 = getAccounts.CheckNameRadmir13(path);
                    if (radmir1 != null)
                    {
                        foreach (var nick in radmir1)
                        {
                            await SellTemplate("Radmir RP", "Server 1", path, nick);
                        }
                    }
                    if (radmir2 != null)
                    {
                        foreach (var nick in radmir2)
                        {
                            await SellTemplate("Radmir RP", "Server 2", path, nick);
                        }
                    }
                    if (radmir3 != null)
                    {
                        foreach (var nick in radmir3)
                        {
                            await SellTemplate("Radmir RP", "Server 3", path, nick);
                        }
                    }
                    if (radmir4 != null)
                    {
                        foreach (var nick in radmir4)
                        {
                            await SellTemplate("Radmir RP", "Server 4", path, nick);
                        }
                    }
                    if (radmir5 != null)
                    {
                        foreach (var nick in radmir5)
                        {
                            await SellTemplate("Radmir RP", "Server 5", path, nick);
                        }
                    }
                    if (radmir6 != null)
                    {
                        foreach (var nick in radmir6)
                        {
                            await SellTemplate("Radmir RP", "Server 6", path, nick);
                        }
                    }
                    if (radmir7 != null)
                    {
                        foreach (var nick in radmir7)
                        {
                            await SellTemplate("Radmir RP", "Server 7", path, nick);
                        }
                    }
                    if (radmir8 != null)
                    {
                        foreach (var nick in radmir8)
                        {
                            await SellTemplate("Radmir RP", "Server 8", path, nick);
                        }
                    }
                    if (radmir9 != null)
                    {
                        foreach (var nick in radmir9)
                        {
                            await SellTemplate("Radmir RP", "Server 9", path, nick);
                        }
                    }
                    if (radmir10 != null)
                    {
                        foreach (var nick in radmir10)
                        {
                            await SellTemplate("Radmir RP", "Server 10", path, nick);
                        }
                    }
                    if (radmir11 != null)
                    {
                        foreach (var nick in radmir11)
                        {
                            await SellTemplate("Radmir RP", "Server 11", path, nick);
                        }
                    }
                    if (radmir12 != null)
                    {
                        foreach (var nick in radmir12)
                        {
                            await SellTemplate("Radmir RP", "Server 12", path, nick);
                        }
                    }
                    if (radmir13 != null)
                    {
                        foreach (var nick in radmir13)
                        {
                            await SellTemplate("Radmir RP", "Server 13", path, nick);
                        }
                    }
                }
                #endregion
                #region Diamond RP
                if (await checkStatusSell("Diamond RP"))
                {
                    var diamondEmerald = getAccounts.CheckNameDiamondEmerald(path);
                    var diamondTrilliant = getAccounts.CheckNameDiamondTrilliant(path);
                    var diamondRuby = getAccounts.CheckNameDiamondRuby(path);
                    if (diamondEmerald != null)
                    {
                        foreach (var nick in diamondEmerald)
                        {
                            await SellTemplate("Diamond RP", "Emerald", path, nick);
                        }
                    }
                    if (diamondTrilliant != null)
                    {
                        foreach (var nick in diamondTrilliant)
                        {
                            await SellTemplate("Diamond RP", "Trilliant", path, nick);
                        }
                    }
                    if (diamondRuby != null)
                    {
                        foreach (var nick in diamondRuby)
                        {
                            await SellTemplate("Diamond RP", "Ruby", path, nick);
                        }
                    }
                }


                #endregion
                #region Evolve RP
                if (await checkStatusSell("Evolve RP"))
                {
                    var Evolve01 = getAccounts.CheckNameEvolve1(path);
                    var Evolve02 = getAccounts.CheckNameEvolve2(path);
                    var Evolve03 = getAccounts.CheckNameEvolve3(path);
                    if (Evolve01 != null)
                    {
                        foreach (var nick in Evolve01)
                        {
                            await SellTemplate("Evolve RP", "01", path, nick);
                        }
                    }
                    if (Evolve02 != null)
                    {
                        foreach (var nick in Evolve02)
                        {
                            await SellTemplate("Evolve RP", "03", path, nick);
                        }
                    }
                    if (Evolve03 != null)
                    {
                        foreach (var nick in Evolve03)
                        {
                            await SellTemplate("Evolve RP", "03", path, nick);
                        }
                    }
                }

                #endregion
                #region RodinaRP
                if (await checkStatusSell("Rodina RP"))
                {
                    var RodinaVO = getAccounts.CheckNameRodinaVO(path);
                    var RodinaSO = getAccounts.CheckNameRodinaSO(path);
                    var RodinaCO = getAccounts.CheckNameRodinaCO(path);
                    var RodinaYO = getAccounts.CheckNameRodinaYO(path);
                    if (RodinaVO != null)
                    {
                        foreach (var nick in RodinaVO)
                        {
                            await SellTemplate("Rodina RP", "Восточный Округ", path, nick);
                        }
                    }
                    if (RodinaSO != null)
                    {
                        foreach (var nick in RodinaSO)
                        {
                            await SellTemplate("Rodina RP", "Северный Округ", path, nick);
                        }
                    }
                    if (RodinaCO != null)
                    {
                        foreach (var nick in RodinaCO)
                        {
                            await SellTemplate("Rodina RP", "Центральный Округ", path, nick);
                        }
                    }
                    if (RodinaYO != null)
                    {
                        foreach (var nick in RodinaYO)
                        {
                            await SellTemplate("Rodina RP", "Южный Округ", path, nick);
                        }
                    }
                }
                #endregion
                #region Trinity RP
                if (await checkStatusSell("Trinity RP"))
                {
                    var trinity1 = getAccounts.CheckNameTrinity1(path);
                    var trinity2 = getAccounts.CheckNameTrinity2(path);
                    if (trinity1 != null)
                    {
                        foreach (var nick in trinity1)
                        {
                            await SellTemplate("Trinity RP", "01", path, nick);
                        }
                    }
                    if (trinity2 != null)
                    {
                        foreach (var nick in trinity2)
                        {
                            await SellTemplate("Trinity RP", "02", path, nick);
                        }
                    }
                }

                #endregion
                #region GTA RP
                if (await checkStatusSell("GTA RP"))
                {
                    var gtarp1 = getAccounts.CheckNameGTARP1(path);
                    var gtarp2 = getAccounts.CheckNameGTARP2(path);
                    if (gtarp1 != null)
                    {
                        foreach (var nick in gtarp1)
                        {
                            await SellTemplate("GTA RP", "#1", path, nick);
                        }
                    }
                    if (gtarp2 != null)
                    {
                        foreach (var nick in gtarp2)
                        {
                            await SellTemplate("GTA RP", "#2", path, nick);
                        }
                    }
                }
                #endregion
                #region Amazing RP
                if (await checkStatusSell("Amazing RP"))
                {
                    var amazingred = getAccounts.CheckNameAmazingRed(path);
                    var amazingyellow = getAccounts.CheckNameAmazingYellow(path);
                    var amazinggreen = getAccounts.CheckNameAmazingGreen(path);
                    var amazingazure = getAccounts.CheckNameAmazingAzure(path);
                    var amazingsilver = getAccounts.CheckNameAmazingSilver(path);
                    if (amazingred != null)
                    {
                        foreach (var nick in amazingred)
                        {
                            await SellTemplate("Amazing RP", "Red", path, nick);
                        }
                    }
                    if (amazingyellow != null)
                    {
                        foreach (var nick in amazingyellow)
                        {
                            await SellTemplate("Amazing RP", "Yellow", path, nick);
                        }
                    }
                    if (amazinggreen != null)
                    {
                        foreach (var nick in amazinggreen)
                        {
                            await SellTemplate("Amazing RP", "Green", path, nick);
                        }
                    }
                    if (amazingazure != null)
                    {
                        foreach (var nick in amazingazure)
                        {
                            await SellTemplate("Amazing RP", "Azure", path, nick);
                        }
                    }
                    if (amazingsilver != null)
                    {
                        foreach (var nick in amazingsilver)
                        {
                            await SellTemplate("Amazing RP", "Silver", path, nick);
                        }
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
                MessageBox.Show($"Залито: \nArizona RP {successArizona}\nAdvance RP {successAdvance}\nSamp-RP {successSRP}\nRadmir-RP {successRadmir}\nDiamond-RP {successDiamond}\nEvolve-RP {successEvolve}\nRodina-RP {successRodina}\nTrinity-RP {successTrinity}\nGTA-RP {successGTARP}\nAmazing-RP {successAmazing}\nВсего - {allaccs}\nВиртов на аризоне: {MinifyLong(arizonasmoney)}", "Обработка завершена", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
    }
}
