using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TonieCloudApiClient
{
    public static class SessionsClient
    {
        public static RestClient Client { get; private set; }

        static SessionsClient()
        {
            Client = new RestClient($"{Urls.BaseUrl}/{Urls.Sessions}");
            Client.AddDefaultHeader("Content-Type", "application/json");
        }

        public static async Task<BearerTokenModel> PostAsync(string username, string password)
        {
            var request = new RestRequest(Method.POST);
            request.AddParameter("application/json", $"{{\"email\":\"{username}\",\"password\":\"{password}\"}}", ParameterType.RequestBody);

            var response = await Client.PostAsync<BearerTokenModel>(request);

            return response;
        }
    }

    public static class Urls
    {
        public static string BaseUrl => "https://api.tonie.cloud/v2";
        public static string Sessions => "sessions";
    }
}
