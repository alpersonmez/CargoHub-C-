using Cargohub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cargohub.Services
{
    public interface IItemLinesService
    {
        Task<List<ItemLines>> GetAllItemLines();
        Task<ItemLines> GetItemLineById(int id);
        Task<ItemLines> UpdateItemLine(int id, ItemLines updatedItemLine);
        Task<bool> DeleteItemLine(int id);
    }
}
