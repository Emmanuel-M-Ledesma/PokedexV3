using Newtonsoft.Json;
using PokedexV3.Models.Lista;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models
{
    public partial class GenerationModel
    {
        
        [JsonProperty("id")]
        public long Id { get; set; }   
        
        [JsonProperty("pokemon_species")]
        public Language[] PokemonSpecies { get; set; }

        [JsonProperty("types")]
        public Language[] Types { get; set; }

        
    }
}
