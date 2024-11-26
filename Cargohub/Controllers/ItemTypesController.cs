using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cargohub.Models;
using Cargohub.Services;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            var itemTypes = _itemTypeService.GetAllItemTypes();
            return Ok(itemTypes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var itemType = _itemTypeService.GetItemTypeById(id);
            if (itemType == null) return NotFound("ItemType not found");
            return Ok(itemType);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ItemType itemType)
        {
            if (itemType == null) return BadRequest("Invalid data");
            var createdItemType = _itemTypeService.CreateItemType(itemType);
            return CreatedAtAction(nameof(GetById), new { id = createdItemType.Id }, createdItemType);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ItemType updatedItemType)
        {
            var updated = _itemTypeService.UpdateItemType(id, updatedItemType);
            if (updated == null) return NotFound("ItemType not found");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _itemTypeService.DeleteItemType(id);
            if (!deleted) return NotFound("ItemType not found");
            return Ok("ItemType deleted successfully");
        }
    }
}
