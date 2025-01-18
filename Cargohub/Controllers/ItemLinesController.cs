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
        public async Task<IActionResult> Update(int id, [FromBody] ItemLines itemline)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != itemline.id)
            {
                return BadRequest($"ItemLine Id {id} does not match");
            }
            
            var updatedItemLine = await _itemLinesService.UpdateItemLine(itemline);
            
            if (!updatedItemLine)
            {
                return NotFound();
            }

            return Ok(updatedItemLine);
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
