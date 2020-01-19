using System;
using System.Collections.Generic;
using System.Text;

namespace TonieCloudApiClient.Models
{
    public class HouseholdModel
    {
        public Guid Id { get; set; }
        public string Access { get; set; } // TODO enum?
        public bool CanLeave { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public bool ForeignCreativeTonieContent { get; set; }

        public List<TonieBoxModel> TonieBoxes { get; set; }
        public List<CreativeTonieModel> CreativeTonies { get; set; }
    }
}
