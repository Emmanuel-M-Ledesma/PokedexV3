using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models.Info
{
    public class InfoPoke
    {
        [JsonProperty("abilities")]
        public Ability[] Abilities { get; set; }

        [JsonProperty("base_experience")]
        public long BaseExperience { get; set; }

        [JsonProperty("forms")]
        public Species[] Forms { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }

        [JsonProperty("location_area_encounters")]
        public Uri LocationAreaEncounters { get; set; }

        [JsonProperty("moves")]
        public Move[] Moves { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("past_types")]
        public object[] PastTypes { get; set; }

        [JsonProperty("species")]
        public Species Species { get; set; }

        [JsonProperty("types")]
        public TypeElement[] Types { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }
    }
}
