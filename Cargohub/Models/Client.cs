using Newtonsoft.Json;
using Cargohub.DataConverters;
namespace Cargohub.Models
{
    public class Client
    {
        [JsonProperty("id")]
        public int id { get; set; }
        
        [JsonProperty("name")]
        public string? name { get; set; }

        [JsonProperty("address")]
        public string? address { get; set; }

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

        [JsonProperty("contact_phone")]
        public string? contact_phone { get; set; }

        [JsonProperty("contact_email")]
        public string? contact_email { get; set; }

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
