using Newtonsoft.Json;

namespace Cargohub.Models
{
    public class OrderShipment
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("order_id")]
        public int order_id { get; set; }
        public Order Order { get; set; }

        [JsonProperty("shipment_id")]
        public int shipment_id { get; set; }
        public Shipment Shipment { get; set; }

        [JsonProperty("quantity")]
        public int quantity { get; set; } // Optional: Quantity of the order in this shipment
    }
}
