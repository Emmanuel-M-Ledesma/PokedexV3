using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexV3.Models.Info
{
    public partial class Move
    {
        [JsonProperty("move")]
        public Species MoveMove { get; set; }       
    }
}
