using Cargohub.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cargohub.Services
{
    public class ItemService : IItemService
    {
        private readonly AppDbContext _dbContext;

        public ItemService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Item> GetAllItems()
        {
            return _dbContext.Items.ToList();
        }

        public Item GetItemByUid(string uid)
        {
            return _dbContext.Items.FirstOrDefault(item => item.Uid == uid);
        }

        public Item CreateItem(Item item)
        {
            // Automatically set createdAt and updatedAt
            item.CreatedAt = DateTime.UtcNow;
            item.UpdatedAt = DateTime.UtcNow;

            _dbContext.Items.Add(item);
            _dbContext.SaveChanges();
            return item;
        }


       public Item UpdateItem(string uid, Item updatedItem)
        {
            var existingItem = _dbContext.Items.SingleOrDefault(item => item.Uid == uid);
            if (existingItem == null) return null;

            // Update fields except createdAt
            existingItem.Code = updatedItem.Code;
            existingItem.Description = updatedItem.Description;
            existingItem.ShortDescription = updatedItem.ShortDescription;
            existingItem.UpcCode = updatedItem.UpcCode;
            existingItem.ModelNumber = updatedItem.ModelNumber;
            existingItem.CommodityCode = updatedItem.CommodityCode;
            existingItem.ItemLine = updatedItem.ItemLine;
            existingItem.ItemGroup = updatedItem.ItemGroup;
            existingItem.ItemType = updatedItem.ItemType;
            existingItem.UnitPurchaseQuantity = updatedItem.UnitPurchaseQuantity;
            existingItem.UnitOrderQuantity = updatedItem.UnitOrderQuantity;
            existingItem.PackOrderQuantity = updatedItem.PackOrderQuantity;
            existingItem.SupplierId = updatedItem.SupplierId;
            existingItem.SupplierCode = updatedItem.SupplierCode;
            existingItem.SupplierPartNumber = updatedItem.SupplierPartNumber;
            existingItem.UpdatedAt = DateTime.UtcNow; // Update updatedAt timestamp

            _dbContext.SaveChanges();
            return existingItem;
        }


        public void DeleteItem(string uid)
        {
            var item = GetItemByUid(uid);
            if (item != null)
            {
                _dbContext.Items.Remove(item);
                _dbContext.SaveChanges();
            }
        }
    }
}
