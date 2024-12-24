using Newtonsoft.Json;
using Cargohub.DataConverters;
using System.ComponentModel.DataAnnotations;

namespace Cargohub.Models
{
    public class Item
    {
        [JsonProperty("uid")]
        public string uid { get; set; }

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

        // Foreign key for ItemLine (optional)
        [JsonProperty("item_line")]
        public int? ItemLineId { get; set; }
        public ItemLines? ItemLine { get; set; } // Navigation property

        // Foreign key for ItemGroup (optional)
        [JsonProperty("item_group")]
        public int? ItemGroupId { get; set; }
        public ItemGroup? ItemGroup { get; set; } // Navigation property

        // Foreign key for ItemType (optional)
        [JsonProperty("item_type")]
        public int? ItemTypeId { get; set; }
        public ItemType? ItemType { get; set; } // Navigation property

        [JsonProperty("unit_purchase_quantity")]
        public int? unit_purchase_quantity { get; set; }

        [JsonProperty("unit_order_quantity")]
        public int? unit_order_quantity { get; set; }

        [JsonProperty("pack_order_quantity")]
        public int? pack_order_quantity { get; set; }

        [JsonProperty("supplier_id")]
        public int? supplier_id { get; set; }

        [JsonProperty("supplier_code")]
        public string? supplier_code { get; set; }

        [JsonProperty("supplier_part_number")]
        public string? supplier_part_number { get; set; }

        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }

        [JsonProperty("updated_at")]
        public DateTime updated_at { get; set; }

        [JsonProperty("isdeleted")]
        public bool? isdeleted { get; set; } = false;
    }
}
