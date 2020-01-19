using System;

namespace TonieCloudApiClient.Models
{
    public class CreativeTonieListModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool Live { get; set; }
        public bool Private { get; set; }
    }
}