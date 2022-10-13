using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models.Lista
{
    public partial class Description
    {
        [JsonProperty("description")]
        public string DescriptionDescription { get; set; }

        [JsonProperty("language")]
        public Language Language { get; set; }
    }
}
