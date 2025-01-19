using Cargohub.Models;
using System.Collections.Generic;

namespace Cargohub.Services
{
    public interface IItemGroupService
    {
        Task<List<ItemGroup>>? GetAllItemGroups(int amount);
        Task<ItemGroup>? GetItemGroupById(int id);
        Task<bool> UpdateItem_Groups(ItemGroup item_groups);
        Task<bool> DeleteItem_Groups(int id);
    }
}