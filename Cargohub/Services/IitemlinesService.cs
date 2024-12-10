using Cargohub.Models;
using System.Collections.Generic;

namespace Cargohub.Services
{
    public interface IitemlinesService
    {
        List<Item_lines> GetAllItem_lines();
        Item_lines GetItem_linesById(int id);
        Item_lines UpdateItem_lines(int id, Item_lines Updateditem_lines);
        bool DeleteItem_lines(int id);
    }
}