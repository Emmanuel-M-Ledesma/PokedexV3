using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models.Detalle
{
    public partial class FlavorTextEntry
    {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        [JsonProperty("language")]
        public Color Language { get; set; }

        [JsonProperty("version")]
        public Color Version { get; set; }
    }
}
