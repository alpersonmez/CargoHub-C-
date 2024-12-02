using Cargohub.Models;
using System.Collections.Generic;

namespace Cargohub.Services
{
    public interface IitemGroupsService
    {
        Task<List<ItemGroup>> GetAllItem_Groups();
        Task<ItemGroup> GetItem_GroupsById(int id);
        Task<bool> UpdateItem_Groups(ItemGroup item_groups);
        Task<bool> DeleteItem_Groups(int id);
    }
}