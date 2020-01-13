using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.Net;
using Should;
using RestSharp;

namespace TonieCloudApiClient.Tests
{
    public class SessionsTests
    {
        private const string UserName = "batzi.01+tonies@mailbox.org";
        private const string Password = "IBGpM4eN7F9uWAFFK6c2";

        [Test]
        public async Task CanPostSessions()
        {
            var result = await SessionsClient.PostAsync(UserName, Password);

            result.Jwt.ShouldNotBeNull();

            //var meClient = new RestClient($"{Urls.BaseUrl}/me");
            //meClient.AddDefaultHeader("Authorization", $"Bearer {result.Jwt}");

            //var getRequest = new RestRequest();

            //var meResult = await meClient.GetAsync<object>(getRequest);
        }
    }
}
