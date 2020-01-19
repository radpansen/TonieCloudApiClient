namespace TonieCloudApiClient.GraphQLQueries
{
    public static class Queries
    {
        public static string NotificationsQuery => @"
            { notifications {
                id
                read
                timestamp
                ntype
                invitationToken
                householdId
                membershipId
                tonieId
                tonieReason
                text
                }
            }";

        public static string HouseholdsQuery = @"
            { households {
                access
                canLeave
                foreignCreativeTonieContent
                id
                image
                name
                ownerName
                tonieboxes {
                    id
                    name
                    imageUrl
                }
                creativeTonies {
                    id
                    name
                    live
                    private
                    imageUrl
                    secondsRemaining
                }
            }
            }";
    }
}
