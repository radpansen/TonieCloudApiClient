using System.Net.Http;
using System.Net.Http.Headers;
using System;
using TonieCloudApiClient.Models;
using System.Threading.Tasks;
using TonieCloudApiClient.Config;
using System.Linq;
using TonieCloudApiClient.Extensions;

namespace TonieCloudApiClient
{
    public static class TonieClient
    {
        private static HttpClient HttpClient;
        private static string UserName;
        private static char[] Password;
        private static string BearerToken = null;

        public static async Task Initialize(string username, char[] password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (password == null || !password.Any())
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (HttpClient != null)
            {
                return;
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri($"{Urls.BaseUrl}/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            await SetupAuthenticationAsync(client, username, password);

            HttpClient = client;
        }

        private static async Task SetupAuthenticationAsync(HttpClient client, string username, char[] password)
        {
            if (string.IsNullOrEmpty(BearerToken))
            {
                UserName = username;
                Password = password;
                BearerToken = await GetBearerTokenAsync(client);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
            }
        }

        private static async Task<string> GetBearerTokenAsync(HttpClient client)
        {
            var model = new SessionsPostModel { Email = UserName, Password = string.Concat(Password) };
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
            public static async Task<MeModel> GetAsync()
            {
                using var response = (await HttpClient.GetAsync(Urls.Me)).ThrowIfNotSuccessful();

                return await response.Content.ReadAsAsync<MeModel>();
            }
        }

        public static class Config
        {
            public static async Task<ConfigModel> GetAsync()
            {
                using var response = (await HttpClient.GetAsync(Urls.Config)).ThrowIfNotSuccessful();
                return await response.Content.ReadAsAsync<ConfigModel>();
            }
        }
    }
}
