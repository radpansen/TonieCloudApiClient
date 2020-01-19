namespace TonieCloudApiClient.GraphQL
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
    }
}
