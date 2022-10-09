using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models.Info
{
    public partial class TypeElement
    {
        [JsonProperty("slot")]
        public long Slot { get; set; }

        [JsonProperty("type")]
        public Species Type { get; set; }
    }
}
