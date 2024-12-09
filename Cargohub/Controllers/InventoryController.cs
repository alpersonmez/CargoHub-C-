using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cargohub.Controllers
{

    [ApiController]
    [Route("api/v1/item_groups")]
    public class InventoryController : ControllerBase
    {

        private readonly InventoryService _InventoryService;

        public InventoryController(InventoryService InventoryService)
        {
            InventoryService = InventoryService;
        }

        [HttpGet]
        public ActionResult<List<Item>> GetAllInventories()
        {
            return null;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            Inventory inventory = await _InventoryService.GetInventoryById(Id);
            if (inventory == null)
            {
                return NotFound();
            }
            return Ok(inventory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Inventory inventory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //modelstate is om te kijken of de fields kloppen

            if (id != inventory.id)
            {
                return BadRequest($"Location Id {id} does not match");
            }

            var updated = await _InventoryService.UpdateInventory(inventory);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _InventoryService.DeleteInventory(id);
            if (!deleted)
            {
                return NotFound();

            }
            return NoContent();
        }



    }
}