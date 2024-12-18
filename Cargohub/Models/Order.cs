using Microsoft.AspNetCore.Http.Features;

namespace Cargohub.Models
{
    public class Order
    {
        public int id { get; set; }
        public int source_id { get; set; }
        public DateTime order_date { get; set; }
        public DateTime request_date { get; set; }
        public string reference { get; set; }
        public string reference_extra { get; set; }
        public string order_status { get; set; }
        public string notes { get; set; }
        public string shipping_notes { get; set; }
        public string picking_notes { get; set; }
        public int warehouse_id { get; set; }
        public string ship_to { get; set; }
        public string bill_to { get; set; }
        public int shipment_id { get; set; }
        public double total_amount { get; set; }
        public double total_discount { get; set; }
        public double total_tax { get; set; }
        public double total_surcharge { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        // dit moet er nog bij
    //     items": [
    //   {
    //     "item_id": "P007435",
    //     "amount": 23
    //   }

    }
}
