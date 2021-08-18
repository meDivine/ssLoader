using Newtonsoft.Json;
using ssLoader.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ssLoader.SampStoreAPI;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ssLoader.Arizona
{
    public class AccountSender
    {
        public async Task ArizonaSender(string path)
        {
            try
            {
                var arizonaFormat = new ArizonaFormat();
                var getAccounts = new GetAccounts();
                var brainburgGoods = getAccounts.CheckNameArizonaBrainburg(path);
                var accountSender = new AddAccount();
                foreach (var ARZ in brainburgGoods)
                 {
                     var text = File.ReadAllText($@"{path}\Arizona RP\Brainburg\goods\{ARZ}.json");
                     var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                     MessageBox.Show(result.nick + result.password);
                 }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
