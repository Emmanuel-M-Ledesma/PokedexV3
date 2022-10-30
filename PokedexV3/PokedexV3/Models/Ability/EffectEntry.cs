using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models.Ability
{
    public partial class EffectEntry
    {
        [JsonProperty("effect")]
        public string Effect { get; set; }

        [JsonProperty("language")]
        public Generation Language { get; set; }

        [JsonProperty("short_effect")]
        public string ShortEffect { get; set; }
    }
}
