using NUnit.Framework;
using System.Threading.Tasks;
using Should;
using System.Linq;
using System;

namespace TonieCloudApiClient.Tests
{
    public class GraphQlTests : WithClientLoggedIn
    {
        [Test]
        public async Task CanGetNotifications()
        {
            var result = await TonieClient.GraphQl.GetNotificationsAsync();

            result.ShouldNotBeNull();
            var notification = result.FirstOrDefault();

            if (notification != null)
            {
                // hard to test, because... notifications, duh
                notification.Id.ShouldNotEqual(Guid.Empty);
                notification.MembershipId.ShouldNotEqual(Guid.Empty);
                notification.Timestamp.ShouldBeInRange(new DateTime(2000,1,1), new DateTime(2100,1,1));
            }
        }
    }
}
