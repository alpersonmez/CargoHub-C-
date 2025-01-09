using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Cargohub.Filters;

namespace Cargohub.Controllers
{

    [ApiController]
    [Route("api/inventory")]
    public class InventoryController : ControllerBase
    {

        private readonly IinventoryService _inventoryService;

        public InventoryController(IinventoryService InventoryService)
        {
            _inventoryService = InventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAllInventories()
        {
            List<Inventory>? allInventories = await _inventoryService.GetAllInventories();
            return Ok(allInventories.ToList());
        }

        [HttpGet("total/{id}")]
        public async Task<IActionResult> GetInventoryTotalById(int id)
        {
            Inventory inventory = await _inventoryService.GetInventoryById(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                inventoryId = inventory.id,
                itemReference = inventory.item_reference,
                totalOnHand = inventory.total_on_hand,
                totalExpected = inventory.total_expected,
                totalOrdered = inventory.total_ordered,
                totalAllocated = inventory.total_allocated,
                totalAvailable = inventory.total_available
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            Inventory? inventory = await _inventoryService.GetInventoryById(Id);
            if (inventory == null)
            {
                return NotFound($"Inventory with id {Id} doesn't exist");
            }
            else if (inventory.isdeleted == true) return NotFound("Inventory has been deleted");
            return Ok(inventory);
        }

        [AdminFilter]
        [HttpPost("new")]
        public async Task<IActionResult> Post([FromBody] Inventory inventory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int postCode = await _inventoryService.PostInventory(inventory);
            switch (postCode)
            {
                case 1: //code voor niet alle velden zijn gevuld
                    return BadRequest("Error: not all nessecery info has been sent");
                case 2: //code voor deze inventory bestaat al
                    return BadRequest("Error: Inventory already exists");
                case 3: //Too many lines changed
                    return Problem("Error: Too many lines got affected");
                case 4: //Succes
                    return CreatedAtAction(nameof(GetById), new { id = inventory.id }, inventory);
                //return Created($"api/inventory/{inventory.id}", $"Inventory posted succesfully\n{inventory}");
                default:
                    return Problem($"Error: An unexpected error occurred when adding the inventory to the database.");
            }
        }

        [AdminFilter]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Inventory inventory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int updateCode = await _inventoryService.UpdateInventory(id, inventory);
            switch (updateCode)
            {
                case 1: //code voor geen inventory om aan te passen
                    return NotFound($"no inventory found with id {id}");
                case 2: //code voor niet alle velden zijn gevuld
                    return BadRequest("not all required fields are given");
                case 3: //Succes
                    return Ok(new
                    {
                        message = "Succesfully updated inventory",
                        updated_inventory = await _inventoryService.GetInventoryById(id)
                    });
                default:
                    return Problem("unexpected error occurred");
            }
        }

        [AdminFilter]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Inventory? inventoryToDelete = await _inventoryService.GetInventoryById(id);
            bool deleted = await _inventoryService.DeleteInventory(id);
            if (!deleted || inventoryToDelete == null)
            {
                return NotFound($"no inventory found with id {id}");
            }
            return Ok(new { deleted_inventory = inventoryToDelete });
        }
    }
}