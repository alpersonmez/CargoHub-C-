using Cargohub.Models;


namespace Cargohub.Services
{
    public interface IItemLinesService
    {
        Task<List<ItemLines>> GetAllItemLines(int amount = 100);
        Task<ItemLines> GetItemLineById(int id);
        Task<bool> UpdateItemLine(ItemLines itemline);
        Task<bool> DeleteItemLine(int id);
    }
}
