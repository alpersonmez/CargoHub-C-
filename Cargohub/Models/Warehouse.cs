using Newtonsoft.Json;
using Cargohub.DataConverters;
using System;

namespace Cargohub.Models
{
    public class Warehouse
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("code")]
        public string? code { get; set; }

        [JsonProperty("name")]
        public string? name { get; set; }

        [JsonProperty("address")]
        public string? address { get; set; }

        [JsonProperty("zip")]
        public string? zip { get; set; }

        [JsonProperty("city")]
        public string? city { get; set; }

        [JsonProperty("province")]
        public string? province { get; set; }

        [JsonProperty("country")]
        public string? country { get; set; }

        [JsonProperty("contact")]
        public Contact contact { get; set; } = new Contact(); // Ensure initialization to avoid null references

        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverters))]
        public DateTime created_at { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(DateTimeConverters))]
        public DateTime updated_at { get; set; }

        [JsonProperty("isdeleted")]
        public bool? isdeleted { get; set; } = false;

        // Inner Contact class remains nested within Warehouse
        public class Contact
        {
            [JsonProperty("name")]
            public string? name { get; set; }

            [JsonProperty("phone")]
            public string? phone { get; set; }

            [JsonProperty("email")]
            public string? email { get; set; }
        }
    }
}
