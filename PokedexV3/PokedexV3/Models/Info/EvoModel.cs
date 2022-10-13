using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PokedexV3.Models.Info
{
    public partial class EvoModel
    {
        [JsonProperty("baby_trigger_item")]
        public object BabyTriggerItem { get; set; }

        [JsonProperty("chain")]
        public Chain Chain { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
