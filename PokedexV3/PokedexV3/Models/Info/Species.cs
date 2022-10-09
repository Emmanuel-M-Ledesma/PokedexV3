using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models.Info
{
    public partial class Species
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
