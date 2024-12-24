using Newtonsoft.Json;
using Cargohub.DataConverters;
using System;
using System.Collections.Generic;

namespace Cargohub.Models
{
    public class Shipment
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("order_id")]
        public int? order_id { get; set; }

        [JsonProperty("source_id")]
        public int? source_id { get; set; }

        [JsonProperty("order_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? order_date { get; set; }

        [JsonProperty("request_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? request_date { get; set; }

        [JsonProperty("shipment_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? shipment_date { get; set; }

        [JsonProperty("shipment_type")]
        public string? shipment_type { get; set; }

        [JsonProperty("shipment_status")]
        public string? shipment_status { get; set; }

        [JsonProperty("notes")]
        public string? notes { get; set; }

        [JsonProperty("carrier_code")]
        public string? carrier_code { get; set; }

        [JsonProperty("carrier_description")]
        public string? carrier_description { get; set; }

        [JsonProperty("service_code")]
        public string? service_code { get; set; }

        [JsonProperty("payment_type")]
        public string? payment_type { get; set; }

        [JsonProperty("transfer_mode")]
        public string? transfer_mode { get; set; }

        [JsonProperty("total_package_count")]
        public int? total_package_count { get; set; }

        [JsonProperty("total_package_weight")]
        public double? total_package_weight { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime created_at { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime updated_at { get; set; }

        // Change from single ItemShipment to List<ItemShipment>
        [JsonProperty("items")]
        public List<ItemShipment>? items { get; set; }

        [JsonProperty("isdeleted")]
        public bool? isdeleted { get; set; } = false;
    }

    public class ItemShipment
    {
        [JsonProperty("item_id")]
        public string? item_id { get; set; }

        [JsonProperty("amount")]
        public int? amount { get; set; }
    }
}
