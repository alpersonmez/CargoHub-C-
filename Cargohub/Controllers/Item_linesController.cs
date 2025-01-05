using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Cargohub.Filters;

namespace Cargohub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemLinesController : ControllerBase
    {
        private readonly IItemLinesService _itemLinesService;

        public ItemLinesController(IItemLinesService itemLinesService)
        {
            _itemLinesService = itemLinesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var itemLines = await _itemLinesService.GetAllItemLines();
            return Ok(itemLines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var itemLine = await _itemLinesService.GetItemLineById(id);
            if (itemLine == null)
            {
                return NotFound("ItemLine not found");
            }
            return Ok(itemLine);
        }

        [AdminFilter]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemLines updatedItemLine)
        {
            if (id != updatedItemLine.id)
            {
                return BadRequest("ItemLine ID mismatch");
            }

            var updated = await _itemLinesService.UpdateItemLine(id, updatedItemLine);
            if (updated == null)
            {
                return NotFound("ItemLine not found");
            }

            return Ok(updated);
        }

        [AdminFilter]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _itemLinesService.DeleteItemLine(id);
            if (!deleted)
            {
                return NotFound("ItemLine not found");
            }
            return NoContent();
        }
    }
}
