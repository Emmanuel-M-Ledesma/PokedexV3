using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models.Info
{
    public partial class Ability
    {
        [JsonProperty("ability")]
        public Species AbilityAbility { get; set; }

        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("slot")]
        public long Slot { get; set; }
    }
}
