using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using Cargohub.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Cargohub.ApplyDefaultValues;

namespace Cargohub.DatetimeConverter
{

    public class MultiFormatDateConverter : IsoDateTimeConverter
    {
        public MultiFormatDateConverter()
        {
            DateTimeStyles = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal;
        }

        public override bool CanConvert(Type objectType) => objectType == typeof(DateTime) || objectType == typeof(DateTime?);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            var dateStr = reader.Value.ToString();
            if (DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.AssumeUniversal, out var dt) ||
                DateTime.TryParseExact(dateStr, "yyyy-MM-ddTHH:mm:ssZ", null, DateTimeStyles.AssumeUniversal, out dt))
            {
                return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            }

            throw new JsonSerializationException($"Unable to parse '{dateStr}' as a date.");
        }
    }

    public class DataLoader
    {
        // public class DataImportService
        // {
        //     private readonly AppDbContext context;

        //     public DataImportService(AppDbContext _context)
        //     {
        //         context = _context; 
        //     }

        // }

        public static DateTime ToUtc(DateTime dateTime)
        {
            return dateTime.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)
                : dateTime.ToUniversalTime();
        }
        public static List<T> LoadDataFromFile<T>(string filePath)
        {
            var settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new MultiFormatDateConverter() },
                NullValueHandling = NullValueHandling.Ignore // Skip null values
            };

            using (StreamReader reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json, settings);
            }
        }
        public static void ImportData(AppDbContext context)
        {
            // Import Clients
            // var clients = LoadDataFromFile<Client>("data/clients.json");
            // foreach (var client in clients)
            // {
            //     client.ApplyDefaultValuesClient();
            //     client.created_at = ToUtc(client.created_at);
            //     client.updated_at = ToUtc(client.updated_at);
            //     client.id = 0; // Resetting the Id to 0
            // }
            // context.Clients.AddRange(clients);
            // context.SaveChanges();

            // // Import Inventories
            // var inventories = LoadDataFromFile<Inventory>("data/inventories.json");
            // foreach (var inventory in inventories)
            // {
            //     inventory.ApplyDefaultValuesInventory();
            //     inventory.created_at = ToUtc(inventory.created_at);
            //     inventory.updated_at = ToUtc(inventory.updated_at);
            //     inventory.id = 0; // Resetting the Id to 0
            // }
            // context.Inventories.AddRange(inventories);
            // context.SaveChanges();

            // // Import Suppliers
            // var suppliers = LoadDataFromFile<Supplier>("data/suppliers.json");
            // foreach (var supplier in suppliers)
            // {
            //     supplier.ApplyDefaultValuesSupplier();
            //     supplier.created_at = ToUtc(supplier.created_at);
            //     supplier.updated_at = ToUtc(supplier.updated_at);
            //     supplier.id = 0; // Resetting the Id to 0
            // }
            // context.Supplier.AddRange(suppliers);
            // context.SaveChanges();

            // Now Import Items
            // var items = LoadDataFromFile<Item>("data/items.json");
            // foreach (var item in items)
            // {
            //     item.ApplyDefaultValuesItems();
            //     item.created_at = ToUtc(item.created_at);
            //     item.updated_at = ToUtc(item.updated_at);
            //     item.uid = "0"; // Resetting the Id to 0
            // }
            // context.Items.AddRange(items);
            // context.SaveChanges();

            // // Import Item Groups before Items
            // var itemGroups = LoadDataFromFile<ItemGroup>("data/item_groups.json");
            // foreach (var itemGroup in itemGroups)
            // {
            //     itemGroup.ApplyDefaultValuesItemGroups();
            //     itemGroup.created_at = ToUtc(itemGroup.created_at);
            //     itemGroup.updated_at = ToUtc(itemGroup.updated_at);
            //     itemGroup.id = 0; // Resetting the Id to 0
            // }
            // context.ItemGroups.AddRange(itemGroups);
            // context.SaveChanges(); // Ensure Item Groups are saved first

            // // Import Item Lines before Items
            // var itemLines = LoadDataFromFile<ItemLines>("data/item_lines.json");
            // foreach (var itemLine in itemLines)
            // {
            //     itemLine.ApplyDefaultValuesItemLines();
            //     itemLine.created_at = ToUtc(itemLine.created_at);
            //     itemLine.updated_at = ToUtc(itemLine.updated_at);

            //     itemLine.id = 0; // Resetting the Id to 0
            // }
            // context.Item_lines.AddRange(itemLines);
            // context.SaveChanges(); // Ensure Item Lines are saved first

            // // Import Item Types before Items
            // var itemTypes = LoadDataFromFile<ItemType>("data/item_types.json");
            // foreach (var itemType in itemTypes)
            // {
            //     itemType.ApplyDefaultValuesITemTypes();
            //     itemType.created_at = ToUtc(itemType.created_at);
            //     itemType.updated_at = ToUtc(itemType.updated_at);
            //     itemType.id = 0; // Resetting the Id to 0
            // }
            // context.ItemTypes.AddRange(itemTypes);
            // context.SaveChanges(); 
            // Ensure Item Types are saved first

            // // Now Import Items
            // var items = LoadDataFromFile<Item>("data/items.json");
            // foreach (var item in items)
            // {
            //     item.ApplyDefaultValuesItems();
            //     item.created_at = ToUtc(item.created_at);
            //     item.updated_at = ToUtc(item.updated_at);
            //     item.uid = "0"; // Resetting the Id to 0
            // }
            // context.Items.AddRange(items);
            // context.SaveChanges();

            // Import Warehouses
            // var warehouses = LoadDataFromFile<Warehouse>("data/warehouses.json");
            // foreach (var warehouse in warehouses)
            // {
            //     warehouse.ApplyDefaultValuesWareHouse();
            //     warehouse.created_at = ToUtc(warehouse.created_at);
            //     warehouse.updated_at = ToUtc(warehouse.updated_at);
            //     warehouse.id = 0; // Resetting the Id to 0
            // }
            // context.Warehouses.AddRange(warehouses);
            // context.SaveChanges();

            // Import Orders
            var orders = LoadDataFromFile<Order>("data/orders.json");
            foreach (var order in orders)
            {
                order.ApplyDefaultValuesOrders();
                order.created_at = ToUtc(order.created_at);
                order.updated_at = ToUtc(order.updated_at);
                order.request_date = ToUtc(order.request_date);
                order.order_date = ToUtc(order.order_date);
                order.id = 0; // Resetting the Id to 0
            }
            context.Orders.AddRange(orders);
            context.SaveChanges();

            // //Load Shipments
            // var shipments = LoadDataFromFile<Shipment>("data/shipments.json");
            // foreach (var shipment in shipments)
            // {
            //     shipment.ApplyDefaultValuesShipment();
            //     shipment.created_at = ToUtc(shipment.created_at);
            //     shipment.updated_at = ToUtc(shipment.updated_at);
            //     shipment.id = 0; // Resetting the Id to 0
            // }
            // context.Shipments.AddRange(shipments);
            // context.SaveChanges();

            // //Load Transfers
            // var transfers = LoadDataFromFile<Transfer>("data/transfers.json");
            // foreach (var transfer in transfers)
            // {
            //     transfer.ApplyDefaultValuesTransfers();
            //     transfer.created_at = ToUtc(transfer.created_at);
            //     transfer.updated_at = ToUtc(transfer.updated_at);
            //     transfer.id = 0; // Resetting the Id to 0
            // }
            // context.Transfers.AddRange(transfers);
            // context.SaveChanges();

            // var locations = LoadDataFromFile<Location>("data/locations.json");
            // foreach (var location in locations)
            // {
            //     location.ApplyDefaultValuesLocations();
            //     location.created_at = ToUtc(location.created_at);
            //     location.updated_at = ToUtc(location.updated_at);
            //     location.id = 0; // Resetting the Id to 0
            // }
            // context.Locations.AddRange(locations);
            // context.SaveChanges();
        }
    }
}
