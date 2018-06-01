using System;
using System.Net.Http;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace BL.Network
{
    public class AppHttpClient
    {
        public static HttpClient Client = new HttpClient();

        private static bool CheckConnection() => CrossConnectivity.Current.IsConnected;

        public async Task<T> Send<T>(Func<HttpClient, Task<T>> func)
        {
            return await func(Client);
        }
    }
}