using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssLoader.SampStoreAPI
{
    class AddAccount
    {
        public string ToBase64(string simpleText)
        {
            return simpleText != null ? Convert.ToBase64String(Encoding.UTF8.GetBytes(simpleText)) : "";
        }
        public async Task SendAPI (string key, string server, string price, string reg, string login, string password,
            string code, string info, string title)
        {
            try
            {
                var streamReader = new StreamReader((await WebRequest.Create("https://samp-store.ru/ajax/api.php?" +
                                                                             $"method=add_account&version=15&key={key}&server={server}&price={price}&reg={reg}" + 
                                                                             $"&alogin={login}&password={password}" + 
                                                                             $"&code={code}&info={info}&tittle={title}").GetResponseAsync()).GetResponseStream()!);
                var result = await streamReader.ReadToEndAsync();
                using var webClient = new WebClient(); 
                streamReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка залива");
            }
        }
	}
}
