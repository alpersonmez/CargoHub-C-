using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Cargohub.Models;
using Cargohub.DataConverters;

namespace Cargohub.DatetimeConverter
{
    public class DataLoader
    {
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
                Converters = new List<JsonConverter> { new DateTimeConverters() },
                NullValueHandling = NullValueHandling.Ignore,
                DateParseHandling = DateParseHandling.None
            };

            using (StreamReader reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json, settings);
            }
        }

        public static void ImportData(AppDbContext context)
        {
            try
            {
                // Check if data has already been imported
                if (context.ImportStatuses.Any(s => s.DataImported))
                {
                    Console.WriteLine("Data has already been imported. Skipping import.");
                    return;
                }

                Console.WriteLine("Starting data import...");

                // Import suppliers
                var suppliers = LoadDataFromFile<Supplier>("data/suppliers.json");
                foreach (var supplier in suppliers)
                {
                    supplier.created_at = ToUtc(supplier.created_at);
                    supplier.updated_at = ToUtc(supplier.updated_at);
                    supplier.id = 0;
                }
                context.Supplier.AddRange(suppliers);
                context.SaveChanges();
                Console.WriteLine($"Imported {suppliers.Count} suppliers.");

                // Import Clients
                var clients = LoadDataFromFile<Client>("data/clients.json");
                foreach (var client in clients)
                {
                    client.created_at = ToUtc(client.created_at);
                    client.updated_at = ToUtc(client.updated_at);
                    client.id = 0;
                }
                context.Clients.AddRange(clients);
                context.SaveChanges();
                Console.WriteLine($"Imported {clients.Count} clients.");

                // Import Warehouses
                var warehouses = LoadDataFromFile<Warehouse>("data/warehouses.json");
                foreach (var warehouse in warehouses)
                {
                    warehouse.created_at = ToUtc(warehouse.created_at);
                    warehouse.updated_at = ToUtc(warehouse.updated_at);
                    warehouse.id = 0;
                }
                context.Warehouses.AddRange(warehouses);
                context.SaveChanges();
                Console.WriteLine($"Imported {warehouses.Count} warehouses.");

                // Import Orders
                var orders = LoadDataFromFile<Order>("data/orders.json");
                foreach (var order in orders)
                {
                    if (order.order_date == default)
                    {
                        Console.WriteLine($"Skipping order with invalid date: {JsonConvert.SerializeObject(order)}");
                        continue;
                    }

                    order.created_at = ToUtc(order.created_at);
                    order.updated_at = ToUtc(order.updated_at);
                    order.id = 0;
                }
                context.Orders.AddRange(orders);
                context.SaveChanges();
                Console.WriteLine($"Imported {orders.Count} orders.");

                // Import Inventories
                var inventories = LoadDataFromFile<Inventory>("data/inventories.json");
                foreach (var inventory in inventories)
                {
                    inventory.created_at = ToUtc(inventory.created_at);
                    inventory.updated_at = ToUtc(inventory.updated_at);
                    inventory.id = 0;
                }
                context.Inventories.AddRange(inventories);
                context.SaveChanges();
                Console.WriteLine($"Imported {inventories.Count} inventories.");

                // Import Item Groups
                var itemGroups = LoadDataFromFile<ItemGroup>("data/item_groups.json");
                foreach (var itemGroup in itemGroups)
                {
                    itemGroup.created_at = ToUtc(itemGroup.created_at);
                    itemGroup.updated_at = ToUtc(itemGroup.updated_at);
                    itemGroup.id = 0;
                }
                context.ItemGroups.AddRange(itemGroups);
                context.SaveChanges();
                Console.WriteLine($"Imported {itemGroups.Count} item groups.");

                // Import Item Lines
                var itemLines = LoadDataFromFile<ItemLines>("data/item_lines.json");
                foreach (var itemLine in itemLines)
                {
                    itemLine.created_at = ToUtc(itemLine.created_at);
                    itemLine.updated_at = ToUtc(itemLine.updated_at);
                    itemLine.id = 0;
                }
                context.Item_lines.AddRange(itemLines);
                context.SaveChanges();
                Console.WriteLine($"Imported {itemLines.Count} item lines.");

                // Import Item Types
                var itemTypes = LoadDataFromFile<ItemType>("data/item_types.json");
                foreach (var itemType in itemTypes)
                {
                    itemType.created_at = ToUtc(itemType.created_at);
                    itemType.updated_at = ToUtc(itemType.updated_at);
                    itemType.id = 0;
                }
                context.ItemTypes.AddRange(itemTypes);
                context.SaveChanges();
                Console.WriteLine($"Imported {itemTypes.Count} item types.");

                // Import Items
                var items = LoadDataFromFile<Item>("data/items.json");
                foreach (var item in items)
                {
                    item.created_at = ToUtc(item.created_at);
                    item.updated_at = ToUtc(item.updated_at);
                    item.id = 0;
                }
                context.Items.AddRange(items);
                context.SaveChanges();
                Console.WriteLine($"Imported {items.Count} items.");

                // Import Shipments
                var shipments = LoadDataFromFile<Shipment>("data/shipments.json");
                foreach (var shipment in shipments)
                {
                    shipment.created_at = ToUtc(shipment.created_at);
                    shipment.updated_at = ToUtc(shipment.updated_at);
                    shipment.id = 0;
                }
                context.Shipments.AddRange(shipments);
                context.SaveChanges();
                Console.WriteLine($"Imported {shipments.Count} shipments.");

                // Import Transfers
                var transfers = LoadDataFromFile<Transfer>("data/transfers.json");
                foreach (var transfer in transfers)
                {
                    transfer.created_at = ToUtc(transfer.created_at);
                    transfer.updated_at = ToUtc(transfer.updated_at);
                    transfer.id = 0;
                }
                context.Transfers.AddRange(transfers);
                context.SaveChanges();
                Console.WriteLine($"Imported {transfers.Count} transfers.");

                // Import Locations
                var locations = LoadDataFromFile<Location>("data/locations.json");
                foreach (var location in locations)
                {
                    location.created_at = ToUtc(location.created_at);
                    location.updated_at = ToUtc(location.updated_at);
                    location.id = 0;
                }
                context.Locations.AddRange(locations);
                context.SaveChanges();
                Console.WriteLine($"Imported {locations.Count} locations.");

                // Mark data as imported
                context.ImportStatuses.Add(new ImportStatus { DataImported = true });
                context.SaveChanges();

                Console.WriteLine("Data import completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during data import: {ex.Message}");
            }
        }
    }
}
