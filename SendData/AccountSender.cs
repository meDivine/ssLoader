using Serilog;
using ssLoader.Json;
using ssLoader.Json.CarPrice;
using ssLoader.SampStoreAPI;
using ssLoader.SendData;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ssLoader.Arizona
{
    public class AccountSender
    {
        private string getCurrDir = Directory.GetCurrentDirectory();
        private int successArizona = 0;
        private int successAdvance = 0;
        private int successSRP = 0;
        private int successRadmir = 0;
        private int successDiamond = 0;
        private int successEvolve = 0;
        private int successRodina = 0;
        private int successTrinity = 0;
        private int successGTARP = 0;
        private int successAmazing = 0;
        private int arizonasmoney = 0;
        private int amazingsmoney = 0;
        private int evolvesmoney = 0;
        private int diamondsmoney = 0;
        private int radmirmoney = 0;
        private int samprpsmoney = 0;
        private int rodinamoney = 0;
        private int trinitysmoney = 0;
        private int gtarpsmoney = 0;
        private int advancemoney = 0;
        private int varizonasmoney = 0;
        private int vamazingsmoney = 0;
        private int vevolvesmoney = 0;
        private int vdiamondsmoney = 0;
        private int vradmirmoney = 0;
        private int vsamprpsmoney = 0;
        private int vrodinamoney = 0;
        private int vtrinitysmoney = 0;
        private int vgtarpsmoney = 0;
        private int vadvancemoney = 0;
       
        private async Task LogError()
        {
            var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            Log.Logger = new LoggerConfiguration()
                   .WriteTo.File($@"{getCurrDir}\logs\log-{CreateMD5(timestamp.ToString())}.txt", rollingInterval: RollingInterval.Infinite).CreateLogger();
        }
        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
        private async Task AddStat(string Project, int money, int virts)
        {
            switch (Project)
            {
                case "Samp RP":
                    successTrinity++;
                    vtrinitysmoney += money;
                    trinitysmoney += virts;
                    break;

                case "Trinity RP":
                    successTrinity++;
                    vtrinitysmoney += money;
                    trinitysmoney += virts;
                    break;
                case "Rodina RP":
                    successRodina++;
                    rodinamoney += virts;
                    vrodinamoney += money;
                    break;
                case "Radmir RP":
                    successRadmir++;
                    vradmirmoney += money;
                    radmirmoney += virts;
                    break;
                case "GTA RP":
                    successGTARP++;
                    vgtarpsmoney += money;
                    gtarpsmoney += virts;
                    break;
                case "Evolve RP":
                    successEvolve++;
                    vevolvesmoney += money;
                    evolvesmoney += virts;
                    break;
                case "Diamond RP":
                    successDiamond++;
                    vdiamondsmoney += money;
                    diamondsmoney += virts;
                    break;
                case "Advance RP":
                    successAdvance++;
                    vadvancemoney += money;
                    advancemoney += virts;
                    break;
                case "Amazing RP":
                    successAmazing++;
                    amazingsmoney += virts;
                    vamazingsmoney += money;
                    break;
                case "Arizona RP":
                    arizonasmoney += virts;
                    varizonasmoney += money;
                    successArizona++;
                    break;
            }

        }

        private async Task SellTemplate(string Project, string server, string path, string nick)
        {
            var getAccounts = new GetAccounts();
            var accountSender = new AddAccount();
            var descGenerator = new GenerateDescription();
            await using FileStream api_key = File.OpenRead(@$"{getCurrDir}\Config\Config.json");
            var jsonConfig = await JsonSerializer.DeserializeAsync<Configurate>(api_key);

            await using FileStream openStream = File.OpenRead(@$"{getCurrDir}\Config\Money.json");
            var moneyPrice = await JsonSerializer.DeserializeAsync<Money>(openStream);

            await using FileStream levelPriceJson = File.OpenRead(@$"{getCurrDir}\Config\Levels.json");
            var levelPrice = await JsonSerializer.DeserializeAsync<Level>(levelPriceJson);

            await using FileStream MoneyStartJson = File.OpenRead(@$"{getCurrDir}\Config\StartPrice.json");
            var MoneyStart = await JsonSerializer.DeserializeAsync<MoneyStart>(MoneyStartJson);

            await using FileStream carPriceJson = File.OpenRead(@$"{getCurrDir}\Config\Cars\Coefficient.json");
            var carPrice = await JsonSerializer.DeserializeAsync<Prices>(carPriceJson);

            if (File.Exists($@"{path}\{Project}\{server}\goods\{nick}.json"))
            {
               
                var text = await File.ReadAllTextAsync($@"{path}\{Project}\{server}\goods\{nick}.json");
                var result = JsonSerializer.Deserialize<AccountFormat>(text.Replace("\r\n", ""));
                int price = 0;
                int carpr = 0;
                int summMoney = 0;
                
                switch (Project)
                {
                    case "Arizona RP":
                        price = descGenerator.CountPrice(result.money, result.lvl, MoneyStart.ArizonaRP, moneyPrice.ArizonaRP, levelPrice.ArizonaRP);
                        carpr = descGenerator.CarPrice(result.cars, "ARZ", moneyPrice.ArizonaRP, carPrice.ArizonaRP);
                        summMoney = price + carpr;
                        break;
                    case "Amazing RP":
                        price = descGenerator.CountPrice(result.money, result.lvl, MoneyStart.AmazingRP, moneyPrice.AmazingRP, levelPrice.AmazingRP);
                        carpr = descGenerator.CarPrice(result.cars, "Amazing", moneyPrice.AmazingRP, carPrice.AmazingRP);
                        summMoney = price + carpr;
                        break;
                    case "Advance RP":
                        price = descGenerator.CountPrice(result.money, result.lvl, MoneyStart.AdvanceRP, moneyPrice.AdvanceRP, levelPrice.AdvanceRP);
                        carpr = descGenerator.CarPrice(result.cars, "Advance", moneyPrice.AdvanceRP, carPrice.AdvanceRP);
                        summMoney = price + carpr;
                        break;
                    case "Diamond RP":
                        price = descGenerator.CountPrice(result.money, result.lvl, MoneyStart.DiamondRP, moneyPrice.DiamondRP, levelPrice.DiamondRP);
                        carpr = descGenerator.CarPrice(result.cars, "Diamond", moneyPrice.DiamondRP, carPrice.DiamondRP);
                        summMoney = price + carpr;
                        break;
                    case "Evolve RP":
                        price = descGenerator.CountPrice(result.money, result.lvl, MoneyStart.EvolveRP, moneyPrice.EvolveRP, levelPrice.EvolveRP);
                        carpr = descGenerator.CarPrice(result.cars, "Evolve", moneyPrice.EvolveRP, carPrice.EvolveRP);
                        summMoney = price + carpr;
                        break;
                    case "GTA RP":
                        price = descGenerator.CountPrice(result.money, result.lvl, MoneyStart.GTARP, moneyPrice.GTARP, levelPrice.GTARP);
                        carpr = descGenerator.CarPrice(result.cars, "GTARP", moneyPrice.GTARP, carPrice.GTARP);
                        summMoney = price + carpr;
                        break;
                    case "Radmir RP":
                        price = descGenerator.CountPrice(result.money, result.lvl, MoneyStart.RadmirRP, moneyPrice.RadmirRP, levelPrice.RadmirRP);
                        carpr = descGenerator.CarPrice(result.cars, "Radmir", moneyPrice.RadmirRP, carPrice.RadmirRP);
                        summMoney = price + carpr;
                        break;
                    case "Rodina RP":
                        price = descGenerator.CountPrice(result.money, result.lvl, MoneyStart.RodinaRP, moneyPrice.RodinaRP, levelPrice.RodinaRP);
                        carpr = descGenerator.CarPrice(result.cars, "Rodina", moneyPrice.RodinaRP, carPrice.RodinaRP);
                        summMoney = price + carpr;
                        break;
                    case "Trinity RP":
                        price = descGenerator.CountPrice(result.money, result.lvl, MoneyStart.TrinityRP, moneyPrice.TrinityRP, levelPrice.TrinityRP);
                        carpr = descGenerator.CarPrice(result.cars, "Trinity", moneyPrice.TrinityRP, carPrice.TrinityRP);
                        summMoney = price + carpr;
                        break;
                    case "Samp RP":
                        price = descGenerator.CountPrice(result.money, result.lvl, MoneyStart.SampRP, moneyPrice.SampRP, levelPrice.SampRP);
                        summMoney = price;
                        break;
                }



                if (await descGenerator.accFilterAsync(Project, result.money, summMoney, result.lvl))
                {
                    if (await descGenerator.checkStatusMail(Project, result.mail))
                    {
                        string title = await descGenerator.GenerateTitleAsync(result.lvl, result.money, result.cars);
                        // MessageBox.Show($"{Project} | Сервер {server} Ник {result.nick} Цена: {summMoney} денег: {MinifyLong(result.money)} уровень {result.lvl}");
                        var resultat = await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, summMoney, null, result.nick, result.password, "", jsonConfig.seller_message, title));
                        MessageBox.Show(title);
                        if (resultat.Contains("Вы уже добавляли этот аккаунт"))
                        {
                            Log.Error($"[{Project} - {server}] Ошибка добавления аккаунта {result.nick} | Аккаунт уже выставлен на продажу");
                        }
                        else if (resultat.Contains("OK"))
                        {
                            string[] words = resultat.Split(new char[] { '|' });
                            string idacc = words[1];
                            Log.Information($"[{Project} - {server}] {result.nick} - выставлен на продажу за {summMoney} рублей | https://samp-store.ru/account/?id=" + idacc);
                            await AddStat(Project, summMoney, result.money);
                        }
                        else if (resultat.Contains("You are being rate limited"))
                        {
                            MessageBox.Show("Бан от cloudflare за большое кол-во запросов", "Бан от cloudflare");
                        }
                    }
                }

            }
            int timing = jsonConfig.timing;
        }

        public async Task SendToSS(string path)
        {
            try
            {
                await LogError();
                var getAccounts = new GetAccounts();
                var accountSender = new AddAccount();
                var descGenerator = new GenerateDescription();
                #region ArizonaRP
                if (await descGenerator.checkStatusSell("Arizona RP"))
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
                if (await descGenerator.checkStatusSell("Advance RP"))
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
                if (await descGenerator.checkStatusSell("Samp RP"))
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
                if (await descGenerator.checkStatusSell("Radmir RP"))
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
                if (await descGenerator.checkStatusSell("Diamond RP"))
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
                if (await descGenerator.checkStatusSell("Evolve RP"))
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
                if (await descGenerator.checkStatusSell("Rodina RP"))
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
                if (await descGenerator.checkStatusSell("Trinity RP"))
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
                if (await descGenerator.checkStatusSell("GTA RP"))
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
                if (await descGenerator.checkStatusSell("Amazing RP"))
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
                var descGenerator = new GenerateDescription(); // оч хуево делаю торопят обнову выпустить вот и говнокожу
                var allaccs = successArizona + successAdvance + successAmazing + successDiamond + successEvolve + successGTARP + successRadmir + successRodina + successSRP + successTrinity;
                var sumrub = varizonasmoney + vadvancemoney + vamazingsmoney + vdiamondsmoney + vevolvesmoney + vgtarpsmoney + vradmirmoney + vrodinamoney + vsamprpsmoney + vtrinitysmoney; 
                MessageBox.Show($"Залито: \nArizona RP {successArizona} | Вирты: {descGenerator.MinifyLong(arizonasmoney)} | Кэш {varizonasmoney} р." +
                    $"\nAdvance RP {successAdvance} | Вирты: {descGenerator.MinifyLong(advancemoney)} | Кэш {vadvancemoney} р.\n" +
                    $"Samp-RP {successSRP} | Вирты: {descGenerator.MinifyLong(samprpsmoney)} | Кэш {vsamprpsmoney} р.\n" +
                    $"Radmir-RP {successRadmir} | Вирты: {descGenerator.MinifyLong(radmirmoney)} | Кэш {vradmirmoney} р.\n" +
                    $"Diamond-RP {successDiamond} | Вирты: {descGenerator.MinifyLong(diamondsmoney)} | Кэш {vdiamondsmoney} р.\n" +
                    $"Evolve-RP {successEvolve} | Вирты: {descGenerator.MinifyLong(evolvesmoney)} | Кэш {vevolvesmoney} р.\n" +
                    $"Rodina-RP {successRodina} | Вирты: {descGenerator.MinifyLong(rodinamoney)} | Кэш {vrodinamoney} р.\n" +
                    $"Trinity-RP {successTrinity} | Вирты: {descGenerator.MinifyLong(trinitysmoney)} | Кэш {vtrinitysmoney} р.\n" +
                    $"GTA-RP {successGTARP} | Вирты: {descGenerator.MinifyLong(gtarpsmoney)} | Кэш {vgtarpsmoney} р.\n" +
                    $"Amazing-RP {successAmazing} | Вирты: {descGenerator.MinifyLong(amazingsmoney)} | Кэш {vamazingsmoney} р.\n" +
                    $"Всего - {allaccs} аккаунтов\n" +
                    $"Кэш: {sumrub} р.", "Обработка завершена", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
    }
}
