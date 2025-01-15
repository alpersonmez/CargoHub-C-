// using Newtonsoft.Json;
// using Cargohub.DataConverters;
// using System;
// using System.Collections.Generic;

// namespace Cargohub.Models
// {
//     public class Transfer
//     {
//         [JsonProperty("id")]
//         public int id { get; set; }

//         [JsonProperty("reference")]
//         public string? reference { get; set; }

//         [JsonProperty("transfer_from")]
//         public int? transfer_from { get; set; }

//         [JsonProperty("transfer_to")]
//         public int? transfer_to { get; set; }

//         [JsonProperty("transfer_status")]
//         public string? transfer_status { get; set; }

//         [JsonProperty("created_at")]
//         [JsonConverter(typeof(DateTimeConverter))]
//         public DateTime created_at { get; set; }

//         [JsonProperty("updated_at")]
//         [JsonConverter(typeof(DateTimeConverter))]
//         public DateTime updated_at { get; set; }

//         [JsonProperty("items")]
//         public List<ItemTransfers>? items { get; set; }

//         [JsonProperty("isdeleted")]
//         public bool? isdeleted { get; set; } = false;
//     }
//     public class ItemTransfers
//     {
//         [JsonProperty("item_id")]
//         public int item_id { get; set; }

//         [JsonProperty("amount")]
//         public int? amount { get; set; }

//         // Foreign key to order
//         public int? TransferId { get; set; }
//         public Transfer? Transfer { get; set; }
//     }
// }

using Newtonsoft.Json;
using Cargohub.DataConverters;
using System;
using System.Collections.Generic;

namespace Cargohub.Models
{
public class Transfer
{
    [JsonProperty("id")]
    public int id { get; set; }

    [JsonProperty("reference")]
    public string? reference { get; set; }

    [JsonProperty("transfer_from")]
    public int? transfer_from { get; set; }

    [JsonProperty("transfer_to")]
    public int? transfer_to { get; set; }

    [JsonProperty("transfer_status")]
    public string? transfer_status { get; set; }

    [JsonProperty("created_at")]
    [JsonConverter(typeof(DateTimeConverters))]
    public DateTime created_at { get; set; }

    [JsonProperty("updated_at")]
    [JsonConverter(typeof(DateTimeConverters))]
    public DateTime updated_at { get; set; }

    // [JsonProperty("items")]
    // public List<ItemTransfer>? items { get; set; }  // This will be serialized as part of Transfer

    [JsonProperty("isdeleted")]
    public bool isdeleted { get; set; } = false;
}

// public class ItemTransfer
// {
//     [JsonProperty("item_id")]
//     public string item_id { get; set; }  // Assuming item_id is a string

//     [JsonProperty("amount")]
//     public int? amount { get; set; }

//     // No foreign key needed here since it's embedded as part of Transfer
// }
}