using Leaf.xNet;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HttpStatusCode = Leaf.xNet.HttpStatusCode; // debug reques

namespace ssLoader.Proxy
{
    public class ProxyCheck
    {
        private string getCurrDir = Directory.GetCurrentDirectory();

            
        public async void Check(string proxy)
        {
            Log.Logger = new LoggerConfiguration()
                   .WriteTo.File($@"{getCurrDir}\bad_proxies.txt", rollingInterval: RollingInterval.Infinite).CreateLogger();
            try
            {
                using var request = new HttpRequest();
                request.Proxy = ProxyClient.Parse(proxy);
                request.UserAgent = Http.RandomUserAgent();
                var req = request.Get("https://samp-store.ru/ajax/api.php");
                StreamWriter writer = new StreamWriter(@$"{getCurrDir}\proxy.txt", true);

                if (req.IsOK)
                {
                    await writer.WriteLineAsync(proxy);
                    writer.Close();
                }
                else {
                    Log.Error($"Ошибка подключения к прокси: {proxy} - удалённый сервер не отвечает");
                }
               
            }
            
           /* catch (HttpException ex)
            {
                Log.Error(String.Format("HttpException: {0}", ex.Message));

                switch (ex.Status)
                {
                    case HttpExceptionStatus.Other:
                        Log.Error("Unknown error");
                        break;

                    case HttpExceptionStatus.ProtocolError:
                        Log.Error(String.Format("Status code: {0}", (int)ex.HttpStatusCode));
                        break;

                    case HttpExceptionStatus.ConnectFailure:
                        Log.Error($"Failed to connect to the HTTP-server. Proxy: {proxy}");
                        break;

                    case HttpExceptionStatus.SendFailure:
                        Log.Error($"Failed to send request to HTTP-server. Proxy: {proxy}");
                        break;

                    case HttpExceptionStatus.ReceiveFailure:
                        Log.Error($"Failed to load response from HTTP-server. Proxy: {proxy}");
                        break;

                }
            }*/
            catch (Exception ex)
            {
                Log.Error($"[{proxy}] - " +ex.Message);
            }

        }
    }
}
