using System.Collections.Generic;

namespace TonieCloudApiClient.Models
{
    public class ConfigModel
    {
        public List<string> Locales { get; set; }
        public int MaxChapters { get; set; }
        public int MaxSeconds { get; set; }
        public int MaxBytes { get; set; }
        public List<string> Accepts { get; set; }
        public bool StageWarning { get; set; }
    }
}
