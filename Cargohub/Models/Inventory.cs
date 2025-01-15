using Newtonsoft.Json;
using Cargohub.DataConverters;

namespace Cargohub.Models
{
    public class Inventory
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("item_id")]
        public string? item_id { get; set; }

        [JsonProperty("description")]
        public string? description { get; set; }

        [JsonProperty("item_reference")]
        public string? item_reference { get; set; }

        [JsonProperty("locations")]
        public List<int>? locations { get; set; }

        [JsonProperty("total_on_hand")]
        public int? total_on_hand { get; set; }

        [JsonProperty("total_expected")]
        public int? total_expected { get; set; }

        [JsonProperty("total_ordered")]
        public int? total_ordered { get; set; }

        [JsonProperty("total_allocated")]
        public int? total_allocated { get; set; }

        [JsonProperty("total_available")]
        public int? total_available { get; set; }

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
