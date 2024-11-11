using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cargohub.Controllers
{
    [ApiController]
    [Route("api/v1/items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public ActionResult<List<Item>> GetAllItems()
        {
            return Ok(_itemService.GetAllItems());
        }

        [HttpGet("{uid}")]
        public ActionResult<Item> GetItemByUid(string uid)
        {
            var item = _itemService.GetItemByUid(uid);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<Item> CreateItem([FromBody] Item item)
        {
            if (item.CreatedAt != default || item.UpdatedAt != default)
            {
                return BadRequest("The fields 'createdAt' and 'updatedAt' cannot be specified.");
            }

            var createdItem = _itemService.CreateItem(item);
            return CreatedAtAction(nameof(GetItemByUid), new { uid = createdItem.Uid }, createdItem);
        }



        [HttpPut("{uid}")]
        public ActionResult<Item> UpdateItem(string uid, [FromBody] Item item)
        {
            var updatedItem = _itemService.UpdateItem(uid, item);
            if (updatedItem == null) return NotFound();
            return Ok(updatedItem);
        }

        [HttpDelete("{uid}")]
        public IActionResult DeleteItem(string uid)
        {
            _itemService.DeleteItem(uid);
            return NoContent();
        }
    }
}
