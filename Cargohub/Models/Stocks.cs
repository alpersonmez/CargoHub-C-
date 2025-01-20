using Newtonsoft.Json;

namespace Cargohub.Models
{
    public class Stock
    {
        public int Id { get; set; }

        [JsonProperty("item_id")]
        public string? ItemId { get; set; }
        [JsonProperty("amount")]
        public int amount { get; set; }

    }

    public class OrderStock : Stock
    {   
        [JsonIgnore]
        public Order? Order { get; set; }
        public int OrderId { get; set; }

    }

    public class ShipmentStock : Stock
    {
        public Shipment? Shipment { get; set; }
        public int ShipmentId { get; set; }

    }

    public class TransferStock : Stock
    {
        public Transfer? Transfer { get; set; }
        public int TransferId { get; set; }

    }
}
