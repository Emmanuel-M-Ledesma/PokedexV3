using Newtonsoft.Json;
using PokedexV3.Models.Ability;
using PokedexV3.Models.Detalle;
using PokedexV3.Models.Lista;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models
{
    public partial class AbilityModel
    {
        [JsonProperty("effect_changes")]
        public object[] EffectChanges { get; set; }

        [JsonProperty("effect_entries")]
        public EffectEntry[] EffectEntries { get; set; }

        [JsonProperty("flavor_text_entries")]
        public FlavorTextEntry[] FlavorTextEntries { get; set; }

        [JsonProperty("generation")]
        public Generation Generation { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("is_main_series")]
        public bool IsMainSeries { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("names")]
        public Name[] Names { get; set; }

        [JsonProperty("pokemon")]
        public Pokemon[] Pokemon { get; set; }
    }
}
