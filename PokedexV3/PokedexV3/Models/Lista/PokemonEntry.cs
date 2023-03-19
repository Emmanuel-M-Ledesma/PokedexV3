using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models.Lista
{
    public partial class PokemonEntry
    {
        [JsonProperty("entry_number")]
        public long EntryNumber { get; set; }

        [JsonProperty("pokemon_species")]
        public Language PokemonSpecies { get; set; }
    }
}
