using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models.Info
{
    public partial class Chain
    {
        [JsonProperty("evolves_to")]
        public Chain[] EvolvesTo { get; set; }

        [JsonProperty("is_baby")]
        public bool IsBaby { get; set; }

        [JsonProperty("species")]
        public Species Species { get; set; }
    }
}
