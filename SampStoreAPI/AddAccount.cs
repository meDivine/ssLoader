using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Leaf.xNet; // dll
using HttpStatusCode = Leaf.xNet.HttpStatusCode; // debug request
using Leaf.xNet.Services.Cloudflare;
using System.Text.Json;
using ssLoader.Json;

namespace ssLoader.SampStoreAPI
{
    class AddAccount
    {
        public string ToBase64(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        string currDir = Directory.GetCurrentDirectory();
        private string RandomProxy()
        {
            string[] s = File.ReadAllLines($@"{currDir}\proxy.txt", Encoding.Default);
            return s[new Random().Next(s.Length)];
        }

        private async Task<bool> GetProxyStatusAsync()
        {
            await using FileStream sets = File.OpenRead(@$"{currDir}\Config\Config.json");
            var jsonConfig = await JsonSerializer.DeserializeAsync<Configurate>(sets);
            return jsonConfig.proxy;
        }
        [Obsolete]
        public async Task<string> SendApi(string key, string server, int price, string reg, string login, string password,
            string code, string info, string title)
        {

            /*var streamReader = new StreamReader((await WebRequest.Create("https://samp-store.ru/ajax/api.php?" +
                                                                         $"method=add_account&version=15&key={key}&server={server}&price={price}&reg={reg}" + 
                                                                         $"&alogin={login}&password={password}" + 
                                                                         $"&code={code}&info={info}&tittle={title}".Split('|')).GetResponseAsync()).GetResponseStream()!);
            var result = await streamReader.ReadToEndAsync();
            using var webClient = new WebClient(); 
            streamReader.Close();*/

            /*var request = WebRequest.Create("https://samp-store.ru/ajax/api.php?" +
                $"method=add_account&version=15&key={key}&server={server}&price={price}&reg={reg}" +
                $"&alogin={login}&password={password}" +
                $"&code={code}&info={info}&tittle={title}");*/
            /* request.Proxy = new WebProxy(RandomProxy(), true);
             request.Method = "GET";
             request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 YaBrowser/21.8.2.381 Yowser/2.5 Safari/537.36");
             using var response = await request.GetResponseAsync();
             await using var stream = response.GetResponseStream();
             using var reader = new StreamReader(stream);
             var result = await reader.ReadToEndAsync();
             reader.Close();
             return result;*/
            // Либо объявить var и работать с ним.
            using var request = new HttpRequest();
            var req = request.Get("https://samp-store.ru/ajax/api.php?" +
                           $"method=add_account&version=15&key={key}&server={server}&price={price}&reg={reg}" +
                           $"&alogin={login}&password={password}" +
                           $"&code={code}&info={info}&tittle={title}");
            try
            {

               if (await GetProxyStatusAsync()) // тож плохо)
                {
                    request.Proxy = ProxyClient.Parse(RandomProxy());
                }
                
                request.UserAgent = Http.RandomUserAgent();
               
            }
            catch (HttpException ex)
            {
                MessageBox.Show(String.Format("HttpException: {0}", ex.Message));

                switch (ex.Status)
                {
                    case HttpExceptionStatus.Other:
                        MessageBox.Show("Unknown error");
                        break;

                    case HttpExceptionStatus.ProtocolError:
                        MessageBox.Show(String.Format("Status code: {0}", (int)ex.HttpStatusCode));
                        break;

                    case HttpExceptionStatus.ConnectFailure:
                        MessageBox.Show($"Failed to connect to the HTTP-server. Type: {request.Proxy.Type} Proxy: {request.Proxy.Host}:{request.Proxy.Port}");
                        break;

                    case HttpExceptionStatus.SendFailure:
                        MessageBox.Show($"Failed to send request to HTTP-server. Type: {request.Proxy.Type} Proxy: {request.Proxy.Host}:{request.Proxy.Port}");
                        break;

                    case HttpExceptionStatus.ReceiveFailure:
                        MessageBox.Show($"Failed to load response from HTTP-server. Type: {request.Proxy.Type} Proxy: {request.Proxy.Host}:{request.Proxy.Port}");
                        break;
                        
                }
            }
            return req.ToString();
        }
    }
}
