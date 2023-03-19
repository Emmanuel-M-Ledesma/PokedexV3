using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models.Ability
{
    public partial class Pokemon
    {
        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("pokemon")]
        public Generation PokemonPokemon { get; set; }

        [JsonProperty("slot")]
        public long Slot { get; set; }
    }
}
