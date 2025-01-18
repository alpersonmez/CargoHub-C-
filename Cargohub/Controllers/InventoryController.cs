using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cargohub.Filters;

namespace Cargohub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {

        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int amount)
        {
            var inventory = await _inventoryService.GetAllInventories(amount);
            return Ok(inventory);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)

        {
            Inventory inventory = await _inventoryService.GetInventoryById(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return Ok(inventory);
        }


        [AdminFilter]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Inventory New)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Inventory createdinventory = await _inventoryService.AddInventory(New);
            return CreatedAtAction(nameof(Get), new { id = createdinventory.id }, createdinventory);
        }

        [AdminFilter]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Inventory inventory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //modelstate is om te kijken of de fields kloppen
            
            if (id != inventory.id)
            {
                return BadRequest($"supplier Id {id} does not match");
            }
            
            var updated = await _inventoryService.UpdateInventory(inventory);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [AdminFilter]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _inventoryService.DeleteInventory(id);
            if (!deleted)
            {
                return NotFound();

            } 
            return NoContent();
        }

    }
}