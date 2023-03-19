using Newtonsoft.Json;
using PokedexV3.Models.Detalle;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Color = PokedexV3.Models.Detalle.Color;

namespace PokedexV3.Models
{
    public partial class DetalleModel
    {
        [JsonProperty("color")]
        public Color Color { get; set; }

        [JsonProperty("egg_groups")]
        public Color[] EggGroups { get; set; }

        [JsonProperty("evolution_chain")]
        public EvolutionChain EvolutionChain { get; set; }

        [JsonProperty("evolves_from_species")]
        public object EvolvesFromSpecies { get; set; }

        [JsonProperty("flavor_text_entries")]
        public FlavorTextEntry[] FlavorTextEntries { get; set; }

        [JsonProperty("generation")]
        public Color Generation { get; set; }

        [JsonProperty("habitat")]
        public Color Habitat { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("is_baby")]
        public bool IsBaby { get; set; }

        [JsonProperty("is_legendary")]
        public bool IsLegendary { get; set; }

        [JsonProperty("is_mythical")]
        public bool IsMythical { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("shape")]
        public Color Shape { get; set; }
    }
}
