using Newtonsoft.Json;
using System;
using Cargohub.DataConverters;

namespace Cargohub.Models
{
    public class Location
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("warehouse_id")]
        public int? warehouse_id { get; set; }

        [JsonProperty("code")]
        public string? code { get; set; }

        [JsonProperty("name")]
        public string? name { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverters))]
        public DateTime created_at { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(DateTimeConverters))]
        public DateTime updated_at { get; set; }

        [JsonProperty("isdeleted")]
        public bool isdeleted { get; set; } = false;
    }
}
