using System.Collections.Generic;
using System.Linq;
using Cargohub.Models;

namespace Cargohub.Services
{
    public class ItemTypeService : IItemTypeService
    {
        private readonly AppDbContext _dbContext;

        public ItemTypeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ItemType> GetAllItemTypes()
        {
            return _dbContext.ItemTypes.ToList();
        }

        public ItemType GetItemTypeById(int id)
        {
            return _dbContext.ItemTypes.FirstOrDefault(itemType => itemType.Id == id);
        }

        public ItemType CreateItemType(ItemType itemType)
        {
            itemType.CreatedAt = DateTime.UtcNow;
            itemType.UpdatedAt = DateTime.UtcNow;

            _dbContext.ItemTypes.Add(itemType);
            _dbContext.SaveChanges();
            return itemType;
        }

        public ItemType UpdateItemType(int id, ItemType updatedItemType)
        {
            var existingItemType = _dbContext.ItemTypes.SingleOrDefault(itemType => itemType.Id == id);
            if (existingItemType == null) return null;

            existingItemType.Name = updatedItemType.Name;
            existingItemType.Description = updatedItemType.Description;
            existingItemType.UpdatedAt = DateTime.UtcNow;

            _dbContext.SaveChanges();
            return existingItemType;
        }

        public bool DeleteItemType(int id)
        {
            var itemType = GetItemTypeById(id);
            if (itemType == null) return false;

            _dbContext.ItemTypes.Remove(itemType);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
