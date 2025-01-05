using Newtonsoft.Json;
using System;
using Cargohub.DataConverters;

namespace Cargohub.Models
{
    public class ItemGroup
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string? name { get; set; }

        [JsonProperty("description")]
        public string? description { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime created_at { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime updated_at { get; set; }

        [JsonProperty("isdeleted")]
        public bool? isdeleted { get; set; } = false;
    }
}
