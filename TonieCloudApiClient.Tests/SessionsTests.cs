using NUnit.Framework;
using System.Threading.Tasks;
using Should;

namespace TonieCloudApiClient.Tests
{
    public class SessionsTests
    {
        [Test]
        public async Task CanPostSessions()
        {
            var client = await TonieClient.GetClientAsync(Credentials.UserName, Credentials.Password.ToCharArray());

            client.ShouldNotBeNull();
        }
    }

    public class MeTests
    {
        [Test]
        public async Task CanGetMe()
        {
            // Vorher anmelden
            await TonieClient.GetClientAsync(Credentials.UserName, Credentials.Password.ToCharArray());
            var result = await TonieClient.Me.GetAsync();

            result.ShouldNotBeNull();
            result.Sex.ShouldEqual("m");
        }
    }
}
