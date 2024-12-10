using Cargohub.Models;
using System.Collections.Generic;

namespace Cargohub.Services
{
    public interface IItemGroupService
    {
        Task<List<ItemGroup>>? GetAllItem_Groups();
        Task<ItemGroup>? GetItem_GroupById(int id);
        Task<bool> UpdateItem_Groups(ItemGroup item_groups);
        Task<bool> DeleteItem_Groups(int id);
        Task<bool> PostItemGroup(ItemGroup itemGroup);
    }
}