using NUnit.Framework;
using System.Threading.Tasks;
using Should;
using System.Linq;

namespace TonieCloudApiClient.Tests
{
    public class ConfigTests : WithClientLoggedIn
    {
        [Test]
        public async Task CanGetConfig()
        {
            var result = await TonieClient.Config.GetAsync();

            result.ShouldNotBeNull();
            result.Accepts.Any().ShouldBeTrue();
            result.Accepts.ShouldContain("mp3");
            result.Locales.ShouldContain("de");
            result.Locales.ShouldContain("en");
            result.MaxBytes.ShouldEqual(1*1024*1024*1024);
            result.MaxSeconds.ShouldEqual(5400);
            result.MaxChapters.ShouldEqual(99);
            result.StageWarning.ShouldBeFalse();
        }
    }
}
