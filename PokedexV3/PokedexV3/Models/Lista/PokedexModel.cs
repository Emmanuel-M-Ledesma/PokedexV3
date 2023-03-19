using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models.Lista
{
    public partial class PokedexModel
    {
        [JsonProperty("descriptions")]
        public Description[] Descriptions { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("is_main_series")]
        public bool IsMainSeries { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("names")]
        public Name[] Names { get; set; }

        [JsonProperty("pokemon_entries")]
        public PokemonEntry[] PokemonEntries { get; set; }

        [JsonProperty("region")]
        public object Region { get; set; }

        [JsonProperty("version_groups")]
        public object[] VersionGroups { get; set; }
    }
}
