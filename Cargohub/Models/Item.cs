using System.ComponentModel;
using Cargohub.Models;
using Cargohub.DataConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cargohub.Models
{
    public class Item
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("uid")]
        public string? uid { get; set; }

        [JsonProperty("code")]
        public string? code { get; set; }

        [JsonProperty("description")]
        public string? description { get; set; }

        [JsonProperty("short_description")]
        public string? short_description { get; set; }

        [JsonProperty("upc_code")]
        public string? upc_code { get; set; }

        [JsonProperty("model_number")]
        public string? model_number { get; set; }

        [JsonProperty("commodity_code")]
        public string? commodity_code { get; set; }

        public ItemLines? ItemLine { get; set; }

        [JsonProperty("item_line")]
        public int? item_line { get; set; } // Foreign key for ItemLine

        public ItemGroup? ItemGroup { get; set; }

        [JsonProperty("item_group")]
        public int? item_group { get; set; } // Foreign key for ItemGroup

        public ItemType? ItemType { get; set; }

        [JsonProperty("item_type")]
        public int? item_type { get; set; } // Foreign key for ItemType

        [JsonProperty("unit_purchase_quantity")]
        public int unit_purchase_quantity { get; set; }

        [JsonProperty("unit_order_quantity")]
        public int unit_order_quantity { get; set; }

        [JsonProperty("pack_order_quantity")]
        public int pack_order_quantity { get; set; }

        [JsonProperty("supplier")]
        public Supplier? supplier { get; set; }

        [JsonProperty("supplier_id")]
        public int supplier_id { get; set; }

        [JsonProperty("supplier_code")]
        public string? supplier_code { get; set; }

        [JsonProperty("supplier_part_number")]
        public string? supplier_part_number { get; set; }

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
