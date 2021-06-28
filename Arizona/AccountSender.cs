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
                var brainburgGoods = getAccounts.CheckNameArizona(path);
                var accountSender = new AddAccount();
                 foreach (var test in brainburgGoods)
                 {
                     var text = File.ReadAllText($@"{path}\Arizona RP\Brainburg\goods\{test}.json");
                     var result = JsonSerializer.Deserialize<ArizonaFormat>(text.Replace("\r\n", ""));
                     MessageBox.Show(result.nick);
                     

                 }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
