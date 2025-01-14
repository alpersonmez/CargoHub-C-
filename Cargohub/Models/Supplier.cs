using Newtonsoft.Json;
using Cargohub.DataConverters;
using System;

namespace Cargohub.Models
{
    public class Supplier
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("code")]
        public string? code { get; set; }

        [JsonProperty("name")]
        public string? name { get; set; }

        [JsonProperty("address")]
        public string? address { get; set; }

        [JsonProperty("address_extra")]
        public string? address_extra { get; set; }

        [JsonProperty("city")]
        public string? city { get; set; }

        [JsonProperty("zip_code")]
        public string? zip_code { get; set; }

        [JsonProperty("province")]
        public string? province { get; set; }

        [JsonProperty("country")]
        public string? country { get; set; }

        [JsonProperty("contact_name")]
        public string? contact_name { get; set; }

        [JsonProperty("phone_number")]
        public string? phone_number { get; set; }

        [JsonProperty("reference")]
        public string? reference { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverters))]
        public DateTime created_at { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(DateTimeConverters))]
        public DateTime updated_at { get; set; }

        [JsonProperty("isdeleted")]
        public bool? isdeleted { get; set; } = false;
    }
}
