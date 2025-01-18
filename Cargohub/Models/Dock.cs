using Newtonsoft.Json;
using Cargohub.DataConverters;
using System;
using Microsoft.EntityFrameworkCore;

namespace Cargohub.Models
{
    public class Dock
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("warehouse_id")]
        public int warehouse_id { get; set; } // Foreign key to Warehouse

        [JsonProperty("code")]
        public string? code { get; set; } // Unique identifier for the dock

        [JsonProperty("status")]
        public string status { get; set; } = "free"; // Default status (e.g., free, occupied)

        [JsonProperty("description")]
        public string? description { get; set; } // Optional description of the dock

        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverters))]
        public DateTime created_at { get; set; } = DateTime.Now;

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(DateTimeConverters))]
        public DateTime updated_at { get; set; } = DateTime.Now;

        [JsonProperty("isdeleted")]
        public bool is_deleted { get; set; } = false;

        [JsonIgnore]
        public Warehouse? warehouse { get; set; }
    }
}
