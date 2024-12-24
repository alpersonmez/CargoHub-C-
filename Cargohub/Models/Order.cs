using Newtonsoft.Json;
using Cargohub.DataConverters;
using System;
using System.Collections.Generic;

namespace Cargohub.Models
{
    public class Order
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("source_id")]
        public int? source_id { get; set; }

        [JsonProperty("order_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime order_date { get; set; }

        [JsonProperty("request_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime request_date { get; set; }

        [JsonProperty("reference")]
        public string? reference { get; set; }

        [JsonProperty("reference_extra")]
        public string? reference_extra { get; set; }

        [JsonProperty("order_status")]
        public string? order_status { get; set; }

        [JsonProperty("notes")]
        public string? notes { get; set; }

        [JsonProperty("shipping_notes")]
        public string? shipping_notes { get; set; }

        [JsonProperty("picking_notes")]
        public string? picking_notes { get; set; }

        [JsonProperty("warehouse_id")]
        public int? warehouse_id { get; set; }

        [JsonProperty("ship_to")]
        public string? ship_to { get; set; }

        [JsonProperty("bill_to")]
        public string? bill_to { get; set; }

        [JsonProperty("shipment_id")]
        public int? shipment_id { get; set; }

        [JsonProperty("total_amount")]
        public double? total_amount { get; set; }

        [JsonProperty("total_discount")]
        public double? total_discount { get; set; }

        [JsonProperty("total_tax")]
        public double? total_tax { get; set; }

        [JsonProperty("total_surcharge")]
        public double? total_surcharge { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime created_at { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime updated_at { get; set; }

        [JsonProperty("items")]
        public List<orderItem>? items { get; set; }

        [JsonProperty("isdeleted")]
        public bool? isdeleted { get; set; } = false;
    }

    public class orderItem
    {
        [JsonProperty("item_id")]
        public string? item_id { get; set; }

        [JsonProperty("amount")]
        public int? amount { get; set; }
    }
}
