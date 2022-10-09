using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models.Detalle
{
    public partial class EvolutionChain
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
