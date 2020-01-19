using System.Net.Http;
using System.Net.Http.Headers;
using System;
using TonieCloudApiClient.Models;
using System.Threading.Tasks;
using TonieCloudApiClient.Config;
using System.Linq;
using TonieCloudApiClient.Extensions;
using System.Collections.Generic;
using GraphQL.Client;
using GraphQL.Common.Request;
using TonieCloudApiClient.GraphQL;

namespace TonieCloudApiClient
{
    public static class TonieClient
    {
        private static HttpClient HttpClient;
        private static GraphQLClient GraphQLClient;
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
            SetupGraphQLClient(client);
        }

        private static void SetupGraphQLClient(HttpClient client)
        {
            var graphQLClient = new GraphQLClient(client.BaseAddress);
            
            graphQLClient.EndPoint = new Uri($"{Urls.BaseUrl}/{Urls.GraphQl}");
            
            graphQLClient.DefaultRequestHeaders.Clear();
            foreach (var header in client.DefaultRequestHeaders)
            {
                graphQLClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            graphQLClient.DefaultRequestHeaders.Authorization = client.DefaultRequestHeaders.Authorization;

            GraphQLClient = graphQLClient;
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

        public static class GraphQl
        {
            public static async Task<List<NotificationModel>> GetNotificationsAsync()
            {
                var graphQlResponse = (await GraphQLClient.PostQueryAsync(Queries.NotificationsQuery)).ThrowIfNotSuccessful();
                return graphQlResponse.GetDataFieldAs<List<NotificationModel>>("notifications");
            }
        }
    }
}
