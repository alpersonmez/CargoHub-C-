// using Cargohub.Models;
// // nog kijken hoe we het met datetime gaan doen maar ik vraag jurn wel
// namespace Cargohub.ApplyDefaultValues{
//     public static class ApplyDefaultValues{
//         public static void ApplyDefaultValuesClient(this Client client)
//         {
//             if (string.IsNullOrEmpty(client.name))
//             {
//                 client.name = "Unknown";
//             }

//             if (string.IsNullOrEmpty(client.address))
//             {
//                 client.address = "Unknown Address";
//             }

//             if (string.IsNullOrEmpty(client.city))
//             {
//                 client.city = "Unknown City";
//             }

//             if (string.IsNullOrEmpty(client.zip_code))
//             {
//                 client.zip_code = "Unknown";
//             }

//             if (string.IsNullOrEmpty(client.province))
//             {
//                 client.province = "Unknown Province";
//             }

//             if (string.IsNullOrEmpty(client.country))
//             {
//                 client.country = "Unknown Country";
//             }

//             if (string.IsNullOrEmpty(client.contact_name))
//             {
//                 client.contact_name = "Unknown Contact";
//             }

//             if (string.IsNullOrEmpty(client.contact_phone))
//             {
//                 client.contact_phone = "Unknown";
//             }

//             if (string.IsNullOrEmpty(client.contact_email))
//             {
//                 client.contact_email = "unknown";
//             }

//             // Apply defaults for DateTime fields 
//             if (client.created_at == default)
//             {
//                 client.created_at = DateTime.UtcNow;
//             }

//             if (client.updated_at == default)
//             {
//                 client.updated_at = DateTime.UtcNow;
//             }

//             // Set the isdeleted flag to false if it's not set
//             if (client.isdeleted == null)
//             {
//                 client.isdeleted = false;
//             }
//         }
    
//     public static void ApplyDefaultValuesInventory(this Inventory inventory)
//         {
//             if (string.IsNullOrEmpty(inventory.item_id))
//             {
//                 inventory.item_id = "Unknown";
//             }

//             if (string.IsNullOrEmpty(inventory.description))
//             {
//                 inventory.description = "Unknown description";
//             }

//             if (string.IsNullOrEmpty(inventory.item_reference))
//             {
//                 inventory.item_reference = "Unknown item reference";
//             }

//             if (inventory.locations == null || !inventory.locations.Any())
//             {
//                 inventory.locations = new List<int> { -1 };
//             }

//             if (inventory.total_on_hand < 0)
//             {
//                 inventory.total_on_hand = -1;
//             }

//             if (inventory.total_expected < 0)
//             {
//                 inventory.total_expected = -1;
//             }

//             if (inventory.total_ordered < 0)
//             {
//                 inventory.total_ordered = -1;
//             }

//             if (inventory.total_allocated < 0)
//             {
//                 inventory.total_allocated = -1;
//             }

//             if (inventory.total_available < 0)
//             {
//                 inventory.total_available = -1;
//             }

//             // Apply defaults for DateTime fields 
//             if (inventory.created_at == default)
//             {
//                 inventory.created_at = DateTime.UtcNow;
//             }

//             if (inventory.updated_at == default)
//             {
//                 inventory.updated_at = DateTime.UtcNow;
//             }

//             // Set the isdeleted flag to false if it's not set
//             if (inventory.isdeleted == null)
//             {
//                 inventory.isdeleted = false;
//             }
//         }

//         public static void ApplyDefaultValuesItems(this Item item)
//         {
//             if (string.IsNullOrEmpty(item.uid))
//             {
//                 item.uid = "Unknown";
//             }

//             if (string.IsNullOrEmpty(item.code))
//             {
//                 item.code = "Unknown code";
//             }

//             if (string.IsNullOrEmpty(item.description))
//             {
//                 item.description = "Unknown description";
//             }

//             if (string.IsNullOrEmpty(item.short_description))
//             {
//                 item.short_description = "Unknown short description";
//             }

//             if (string.IsNullOrEmpty(item.upc_code))
//             {
//                 item.upc_code = "Unknown UPC code";
//             }

//             if (string.IsNullOrEmpty(item.model_number))
//             {
//                 item.model_number = "Unknown model number";
//             }

//             if (string.IsNullOrEmpty(item.commodity_code))
//             {
//                 item.commodity_code = "Unknown commodity code";
//             }

//             if (item.item_line < 0)
//             {
//                 item.item_line = -1;
//             }

//             if (item.item_group < 0)
//             {
//                 item.item_group = -1;
//             }

//             if (item.item_type < 0)
//             {
//                 item.item_type = -1;
//             }

//             if (item.unit_purchase_quantity < 0)
//             {
//                 item.unit_purchase_quantity = -1;
//             }

//             if (item.unit_order_quantity < 0)
//             {
//                 item.unit_order_quantity = -1;
//             }

//             if (item.pack_order_quantity < 0)
//             {
//                 item.pack_order_quantity = -1;
//             }

//             if (item.supplier_id < 0)
//             {
//                 item.supplier_id = -1;
//             }

//             if (string.IsNullOrEmpty(item.supplier_code))
//             {
//                 item.supplier_code = "Unknown supplier code";
//             }

//             if (string.IsNullOrEmpty(item.supplier_part_number))
//             {
//                 item.supplier_part_number = "Unknown supplier part number";
//             }

//             if (item.created_at == default)
//             {
//                 item.created_at = DateTime.UtcNow;
//             }

//             if (item.updated_at == default)
//             {
//                 item.updated_at = DateTime.UtcNow;
//             }

//             if (item.isdeleted == null)
//             {
//                 item.isdeleted = false;
//             }
//         }

//         public static void ApplyDefaultValuesItemGroups(this ItemGroup itemGroup)
//         {
//             if (string.IsNullOrEmpty(itemGroup.name))
//             {
//                 itemGroup.name = "Unknown Item Group";
//             }

//             if (string.IsNullOrEmpty(itemGroup.description))
//             {
//                 itemGroup.description = "Unknown description";
//             }

//             if (itemGroup.created_at == default)
//             {
//                 itemGroup.created_at = DateTime.UtcNow;
//             }

//             if (itemGroup.updated_at == default)
//             {
//                 itemGroup.updated_at = DateTime.UtcNow;
//             }

//             if (itemGroup.isdeleted == null)
//             {
//                 itemGroup.isdeleted = false;
//             }
//         }

//         public static void ApplyDefaultValuesItemLines(this ItemLines itemLines)
//         {
//             if (string.IsNullOrEmpty(itemLines.name))
//             {
//                 itemLines.name = "Unknown Item Line";
//             }

//             if (string.IsNullOrEmpty(itemLines.description))
//             {
//                 itemLines.description = "Unknown description";
//             }

//             if (itemLines.created_at == default)
//             {
//                 itemLines.created_at = DateTime.UtcNow;
//             }

//             if (itemLines.updated_at == default)
//             {
//                 itemLines.updated_at = DateTime.UtcNow;
//             }

//             if (itemLines.isdeleted == null)
//             {
//                 itemLines.isdeleted = false;
//             }
//         }

//         public static void ApplyDefaultValuesITemTypes(this ItemType itemType)
//         {
//             if (string.IsNullOrEmpty(itemType.name))
//             {
//                 itemType.name = "Unknown Item Type";
//             }

//             if (string.IsNullOrEmpty(itemType.description))
//             {
//                 itemType.description = "Unknown description";
//             }

//             if (itemType.created_at == default)
//             {
//                 itemType.created_at = DateTime.UtcNow;
//             }

//             if (itemType.updated_at == default)
//             {
//                 itemType.updated_at = DateTime.UtcNow;
//             }

//             if (itemType.isdeleted == null)
//             {
//                 itemType.isdeleted = false;
//             }
//         }

//         public static void ApplyDefaultValuesLocations(this Location location)
//         {
//             if (string.IsNullOrEmpty(location.code))
//             {
//                 location.code = "Unknown Location";
//             }

//             if (string.IsNullOrEmpty(location.name))
//             {
//                 location.name = "Unknown location name";
//             }

//             if (location.created_at == default)
//             {
//                 location.created_at = DateTime.UtcNow;
//             }

//             if (location.updated_at == default)
//             {
//                 location.updated_at = DateTime.UtcNow;
//             }

//             if (location.isdeleted == null)
//             {
//                 location.isdeleted = false;
//             }
//         }

//         public static void ApplyDefaultValuesOrders(this Order order)
//         {
//             if (order.source_id < 0)
//             {
//                 order.source_id = -1;
//             }
            
//             if (string.IsNullOrEmpty(order.reference))
//             {
//                 order.reference = "Unknown order reference";
//             }

//             if (string.IsNullOrEmpty(order.reference_extra))
//             {
//                 order.reference_extra = "Unknown extra reference";
//             }

//             if (string.IsNullOrEmpty(order.order_status))
//             {
//                 order.order_status = "Unknown order status";
//             }

//             if (string.IsNullOrEmpty(order.notes))
//             {
//                 order.notes = "Unknown notes";
//             }

//             if (string.IsNullOrEmpty(order.shipping_notes))
//             {
//                 order.shipping_notes = "Unknown shipping notes";
//             }

//             if (string.IsNullOrEmpty(order.picking_notes))
//             {
//                 order.picking_notes = "Unknown picking notes";
//             }

//             if (order.warehouse_id < 0)
//             {
//                 order.warehouse_id = -1;
//             }

//             if (string.IsNullOrEmpty(order.ship_to))
//             {
//                 order.ship_to = "Unknown shipping destination";
//             }

//             if (string.IsNullOrEmpty(order.bill_to))
//             {
//                 order.bill_to = "Unknown bill to";
//             }

//             if (order.shipment_id < 0)
//             {
//                 order.shipment_id = -1;
//             }

//             if (order.total_amount < 0.0)
//             {
//                 order.total_amount = -1.0;
//             }

//             if (order.total_discount < 0.0)
//             {
//                 order.total_discount = -1.0;
//             }

//             if (order.total_tax < 0.0)
//             {
//                 order.total_tax = -1.0;
//             }

//             if (order.total_surcharge < 0.0)
//             {
//                 order.total_surcharge = -1.0;
//             }

//             if (order.order_date == default)
//             {
//                 order.order_date = DateTime.UtcNow;
//             }

//             if (order.request_date == default)
//             {
//                 order.request_date = DateTime.UtcNow;
//             }

//             if (order.created_at == default)
//             {
//                 order.created_at = DateTime.UtcNow;
//             }

//             if (order.updated_at == default)
//             {
//                 order.updated_at = DateTime.UtcNow;
//             }

//             if (order.isdeleted == null)
//             {
//                 order.isdeleted = false;
//             }
//         }

//         public static void ApplyDefaultValuesShipment(this Shipment shipment)
//         {
//             if (shipment.order_id < 0)
//             {
//                 shipment.order_id = -1;
//             }

//             if (shipment.source_id < 0)
//             {
//                 shipment.source_id = -1;
//             }

//             if (string.IsNullOrEmpty(shipment.shipment_type))
//             {
//                 shipment.shipment_type = "Unknown shipment type";
//             }

//             if (string.IsNullOrEmpty(shipment.shipment_status))
//             {
//                 shipment.shipment_status = "Unknown shipment status";
//             } 

//             if (string.IsNullOrEmpty(shipment.notes))
//             {
//                 shipment.notes = "Unknown notes";
//             }

//             if (string.IsNullOrEmpty(shipment.carrier_code))
//             {
//                 shipment.carrier_code = "Unknown carrier code";
//             }

//             if (string.IsNullOrEmpty(shipment.carrier_description))
//             {
//                 shipment.carrier_description = "Unknown carrier description";
//             }

//             if (string.IsNullOrEmpty(shipment.service_code))
//             {
//                 shipment.service_code = "Unknown service code";
//             }

//             if (string.IsNullOrEmpty(shipment.payment_type))
//             {
//                 shipment.payment_type = "Unknown payment type";
//             }

//             if (string.IsNullOrEmpty(shipment.transfer_mode))
//             {
//                 shipment.transfer_mode = "Unknown transfer mode";
//             }

//             if (shipment.total_package_count < 0)
//             {
//                 shipment.total_package_count = -1;
//             }

//             if (shipment.total_package_weight < 0.0)
//             {
//                 shipment.total_package_weight = -1.0;
//             }                            
            
//             if (shipment.order_date == default)
//             {
//                 shipment.order_date = DateTime.UtcNow;
//             }

//             if (shipment.order_date == default)
//             {
//                 shipment.order_date = DateTime.UtcNow;
//             }

//             if (shipment.request_date == default)
//             {
//                 shipment.request_date = DateTime.UtcNow;
//             }

//             if (shipment.shipment_date == default)
//             {
//                 shipment.shipment_date = DateTime.UtcNow;
//             }

//             if (shipment.created_at == default)
//             {
//                 shipment.created_at = DateTime.UtcNow;
//             }

//             if (shipment.updated_at == default)
//             {
//                 shipment.updated_at = DateTime.UtcNow;
//             }

//             if (shipment.isdeleted == null)
//             {
//                 shipment.isdeleted = false;
//             }
//         }

//         public static void ApplyDefaultValuesSupplier(this Supplier supplier)
//         {
//             if (string.IsNullOrEmpty(supplier.code))
//             {
//                 supplier.code = "Unknown supplier code";
//             }

//             if (string.IsNullOrEmpty(supplier.name))
//             {
//                 supplier.name = "Unknown supplier";
//             }

//             if (string.IsNullOrEmpty(supplier.address))
//             {
//                 supplier.address = "Unknown address";
//             }

//             if (string.IsNullOrEmpty(supplier.address_extra))
//             {
//                 supplier.address_extra = "Unknown address extra";
//             }

//             if (string.IsNullOrEmpty(supplier.city))
//             {
//                 supplier.city = "Unknown city";
//             }

//             if (string.IsNullOrEmpty(supplier.zip_code))
//             {
//                 supplier.zip_code = "Unknown zip code";
//             }

//             if (string.IsNullOrEmpty(supplier.province))
//             {
//                 supplier.province = "Unknown province";
//             }

//             if (string.IsNullOrEmpty(supplier.country))
//             {
//                 supplier.country = "Unknown country";
//             }

//             if (string.IsNullOrEmpty(supplier.contact_name))
//             {
//                 supplier.contact_name = "Unknown contact name";
//             }

//             if (string.IsNullOrEmpty(supplier.phone_number))
//             {
//                 supplier.phone_number = "Unknown phone number";
//             }

//             if (string.IsNullOrEmpty(supplier.reference))
//             {
//                 supplier.reference = "Unknown reference";
//             }

//             if (supplier.created_at == default)
//             {
//                 supplier.created_at = DateTime.UtcNow;
//             }

//             if (supplier.updated_at == default)
//             {
//                 supplier.updated_at = DateTime.UtcNow;
//             }

//             if (supplier.isdeleted == null)
//             {
//                 supplier.isdeleted = false;
//             }
//         }

//         public static void ApplyDefaultValuesTransfers(this Transfer transfer)
//         {
//             if (string.IsNullOrEmpty(transfer.reference))
//             {
//                 transfer.reference = "Unknown transfer reference";
//             }

//             if (transfer.transfer_from < 0)
//             {
//                 transfer.transfer_from = -1;
//             }

//             if (transfer.transfer_to < 0)
//             {
//                 transfer.transfer_to = -1;
//             }

//             if (string.IsNullOrEmpty(transfer.transfer_status))
//             {
//                 transfer.transfer_status = "Unknown transfer status";
//             }

//             if (transfer.created_at == default)
//             {
//                 transfer.created_at = DateTime.UtcNow;
//             }

//             if (transfer.updated_at == default)
//             {
//                 transfer.updated_at = DateTime.UtcNow;
//             }


//             if (transfer.isdeleted == null)
//             {
//                 transfer.isdeleted = false;
//             }

//         }

//         public static void ApplyDefaultValuesWareHouse(this Warehouse warehouse)
//         {
//             if (string.IsNullOrEmpty(warehouse.code))
//             {
//                 warehouse.code = "Unknown warehouse code";
//             }

//             if (string.IsNullOrEmpty(warehouse.name))
//             {
//                 warehouse.name = "Unknown warehouse name";
//             }

//             if (string.IsNullOrEmpty(warehouse.address))
//             {
//                 warehouse.address = "Unknown warehouse address";
//             }

//             if (string.IsNullOrEmpty(warehouse.zip))
//             {
//                 warehouse.zip = "Unknown zip code";
//             }

//             if (string.IsNullOrEmpty(warehouse.city))
//             {
//                 warehouse.city = "Unknown city";
//             }

//             if (string.IsNullOrEmpty(warehouse.province))
//             {
//                 warehouse.province = "Unknown province";
//             }

//             if (string.IsNullOrEmpty(warehouse.country))
//             {
//                 warehouse.country = "Unknown country";
//             }

//             if (warehouse.contact == null)
//             {
//                 warehouse.contact = new Warehouse.Contact();
//             }

//             if (warehouse.created_at == default)
//             {
//                 warehouse.created_at = DateTime.UtcNow;
//             }

//             if (warehouse.updated_at == default)
//             {
//                 warehouse.updated_at = DateTime.UtcNow;
//             }

//             if (warehouse.isdeleted == null)
//             {
//                 warehouse.isdeleted = false;
//             }

//             // Default values for Contact
//             if (warehouse.contact != null)
//             {
//                 if (string.IsNullOrEmpty(warehouse.contact.name))
//                 {
//                     warehouse.contact.name = "Unknown contact name";
//                 }

//                 if (string.IsNullOrEmpty(warehouse.contact.phone))
//                 {
//                     warehouse.contact.phone = "Unknown contact phone";
//                 }

//                 if (string.IsNullOrEmpty(warehouse.contact.email))
//                 {
//                     warehouse.contact.email = "Unknown contact email";
//                 }
//             }
//         }
//     }
// }