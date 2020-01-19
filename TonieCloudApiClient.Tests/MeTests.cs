using NUnit.Framework;
using System.Threading.Tasks;
using Should;
using System.Linq;

namespace TonieCloudApiClient.Tests
{
    public class MeTests : WithClientLoggedIn
    {
        [Test]
        public async Task CanGetMe()
        {
            var result = await TonieClient.Me.GetAsync();

            result.ShouldNotBeNull();
            result.Sex.ShouldEqual("m");
            result.FirstName.ShouldEqual("Marcus");
        }
    }
}
