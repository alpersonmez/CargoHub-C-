using Cargohub.Models;
using System.Collections.Generic;

namespace Cargohub.Services
{
    public interface IitemlinesService
    {
        Task<List<Item_lines>> GetAllItem_lines();
        Task<Item_lines> GetItem_linesById(int id);
        Task<bool> UpdateItem_lines(Item_lines item_lines);
        Task<bool> DeleteItem_lines(int id);
    }
}