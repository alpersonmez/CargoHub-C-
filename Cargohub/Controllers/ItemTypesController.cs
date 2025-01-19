using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cargohub.Filters;

namespace Cargohub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemTypesController : ControllerBase
    {
        private readonly IItemTypeService _itemTypeService;

        public ItemTypesController(IItemTypeService itemTypeService)
        {
            _itemTypeService = itemTypeService;
        }

        [HttpGet("amount/{amount}")]
        public async Task<IActionResult> GetAll(int amount)
        {
            var itemType = await _itemTypeService.GetAllItemTypes(amount);
            return Ok(itemType);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ItemType itemType = await _itemTypeService.GetItemTypeById(id);
            if (itemType == null)
            {
                return NotFound();
            }
            return Ok(itemType);
        }

        [AdminFilter]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemType itemType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //modelstate is om te kijken of de fields kloppen

            if (id != itemType.id)
            {
                return BadRequest($"itemType Id {id} does not match");
            }

            var updated = await _itemTypeService.UpdateItemType(itemType);

            if (!updated)
            {
                return NotFound();
            }

            return Ok(itemType);
        }

        [AdminFilter]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _itemTypeService.DeleteItemType(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
