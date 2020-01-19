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

        [Test]
        public async Task CanGetHouseholds()
        {
            var result = await TonieClient.GraphQl.GetHouseholdsAsync();

            result.Any().ShouldBeTrue();

            var household = result.First(x => x.TonieBoxes.Any());

            household.Access.ShouldEqual("owner");
            household.CanLeave.ShouldBeTrue();
            household.CreativeTonies.Any().ShouldBeTrue();
            household.ForeignCreativeTonieContent.ShouldBeTrue();
            household.Id.ShouldNotEqual(Guid.Empty);
            household.Image
                .ShouldNotBeNull()
                .ShouldNotEqual("");
            household.Name
                .ShouldNotBeNull()
                .ShouldNotEqual("");
            household.OwnerName
                .ShouldNotBeNull()
                .ShouldNotEqual("");
            household.TonieBoxes.Any().ShouldBeTrue();

            var creativeTonie = household.CreativeTonies.First(x => x.Name.Contains("Papa"));
            creativeTonie.Id
                .ShouldNotBeNull()
                .ShouldNotEqual("");
            creativeTonie.ImageUrl
                .ShouldNotBeNull()
                .ShouldNotEqual("");
            creativeTonie.Live.ShouldBeFalse();
            creativeTonie.Name.ShouldEqual("Papa-Tonie");
            creativeTonie.Private.ShouldBeFalse();
            creativeTonie.SecondsRemaining.ShouldNotEqual(0);
            
            var tonieBox = household.TonieBoxes.First(x => x.Name.Contains("Tim"));
            tonieBox.Id
                .ShouldNotBeNull()
                .ShouldNotEqual("");
            tonieBox.ImageUrl
                .ShouldNotBeNull()
                .ShouldNotEqual("");
            tonieBox.Name.ShouldEqual("Tims rote Toniebox");
        }
    }
}
