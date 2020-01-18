using System.Net.Http;
using System.Net.Http.Headers;
using System;
using TonieCloudApiClient.Models;
using System.Threading.Tasks;
using TonieCloudApiClient.Config;

namespace TonieCloudApiClient
{
    public static class TonieClient
    {
        private static HttpClient _httpClient;
        private static  string _userName;
        private static  char[] _password;
        private static string _bearerToken = null;

        // HttpClient wird als Singleton verwendet
        //private TonieClient() { }

        public static async Task<HttpClient> GetClientAsync(string username = null, char[] password = null)
        {
            if (_httpClient != null)
            {
                return _httpClient;
            }

            if (string.IsNullOrEmpty(username) || password == null)
            {
                throw new ArgumentException("you have to provide username and password");
            }

            _userName = username;
            _password = password;

            var client = new HttpClient();
            client.BaseAddress = new Uri($"{Urls.BaseUrl}/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            await SetupAuthenticationAsync(client);
            _httpClient = client;
            return client;
        }

        private static async Task SetupAuthenticationAsync(HttpClient client)
        {
            if (string.IsNullOrEmpty(_bearerToken))
            {
                _bearerToken = await GetBearerTokenAsync(client);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
            }
        }

        private static async Task<string> GetBearerTokenAsync(HttpClient client)
        {
            var model = new SessionsPostModel { Email = _userName, Password = string.Concat(_password) };
            using var response = await client.PostAsJsonAsync(Urls.Sessions, model);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }
            var bearerToken = await response.Content.ReadAsAsync<BearerTokenModel>();
            return bearerToken.Jwt;
        }

        public static class Me
        {
            //private static HttpClient Client;

            public static async Task<MeModel> GetAsync()
            {
                //if (Client == null)
                //{
                //    Client = await TonieClient.GetClientAsync();
                //}

                using var response = await _httpClient.GetAsync(Urls.Me);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                return await response.Content.ReadAsAsync<MeModel>();
            }
        }
    }
}
