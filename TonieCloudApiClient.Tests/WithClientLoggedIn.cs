namespace TonieCloudApiClient.Tests
{
    public class WithClientLoggedIn
    {
        public WithClientLoggedIn()
        {
            TonieClient.Initialize(Credentials.UserName, Credentials.Password.ToCharArray()).Wait();
        }
    }
}