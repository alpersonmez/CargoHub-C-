using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cargohub.Controllers
{

    [ApiController]
    [Route("api/v1/inventory")]
    public class InventoryController : ControllerBase
    {

        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService InventoryService)
        {
            _inventoryService = InventoryService;
        }

        [HttpGet]
        public ActionResult<List<Item>> GetAllInventories()
        {
            List<Inventory>? allInventories = _inventoryService.GetAllInventories();
            return Ok(allInventories.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Inventory inventory = _inventoryService.GetInventoryById(Id);
            if (inventory == null)
            {
                return NotFound();
            }
            return Ok(inventory);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Inventory inventory)
        {
            if (id != inventory.id)
            {
                return BadRequest($"Location Id {id} does not match");
            }
            bool updated = _inventoryService.UpdateInventory(inventory);
            if (!updated)
            {
                return NotFound();
            }
            return Ok(updated.ToString());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = _inventoryService.DeleteInventory(id);
            if (!deleted)
            {
                return NotFound();

            }
            return Ok(deleted.ToString());
        }

    }
}