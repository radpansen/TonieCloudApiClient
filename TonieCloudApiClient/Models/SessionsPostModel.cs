using Newtonsoft.Json;

namespace TonieCloudApiClient.Models
{
    public class SessionsPostModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
