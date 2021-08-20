using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssLoader.SampStoreAPI
{
    class AddAccount
    {
        public string ToBase64(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        public async Task SendApi(string key, string server, int price, string reg, string login, string password,
            string code, string info, string title)
        {
            try
            {
                /*var streamReader = new StreamReader((await WebRequest.Create("https://samp-store.ru/ajax/api.php?" +
                                                                             $"method=add_account&version=15&key={key}&server={server}&price={price}&reg={reg}" + 
                                                                             $"&alogin={login}&password={password}" + 
                                                                             $"&code={code}&info={info}&tittle={title}".Split('|')).GetResponseAsync()).GetResponseStream()!);
                var result = await streamReader.ReadToEndAsync();
                using var webClient = new WebClient(); 
                streamReader.Close();*/
                var request = WebRequest.Create("https://samp-store.ru/ajax/api.php?" +
                    $"method=add_account&version=15&key={key}&server={server}&price={price}&reg={reg}" +
                    $"&alogin={login}&password={password}" +
                    $"&code={code}&info={info}&tittle={title}");
                using var response = await request.GetResponseAsync();
                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream);
                var result = await reader.ReadToEndAsync();
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка залива");
            }
        }
    }
}
