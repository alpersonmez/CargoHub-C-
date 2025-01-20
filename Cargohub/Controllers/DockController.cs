using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cargohub.Filters;

[ApiController]
[Route("api/[controller]")]
public class DockController : ControllerBase
{
    private readonly IDockService _dockService;

    public DockController(IDockService dockService)
    {
        _dockService = dockService;
    }

    // Get all docks
    [HttpGet("amount/{amount}")]
    public async Task<IActionResult> GetAll(int amount)
    {
        var docks = await _dockService.GetAllDocks(amount);
        var dockDTOs = docks.Select(d => new
        {
            d.id,
            d.code,
            d.status,
            d.description
        });
        return Ok(dockDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var dock = await _dockService.GetDockById(id);
        if (dock == null)
        {
            return NotFound(new { Message = "Dock not found" });
        }

        var dockDTO = new
        {
            dock.id,
            dock.code,
            dock.status,
            dock.description
        };

        return Ok(dockDTO);
    }

    [HttpGet("warehouse/{warehouseId}")]
    public async Task<IActionResult> GetByWarehouse(int warehouseId)
    {
        var docks = await _dockService.GetDocksByWarehouseId(warehouseId);
        if (!docks.Any())
        {
            return NotFound(new { Message = "No docks found for the specified warehouse" });
        }

        var dockDTOs = docks.Select(d => new
        {
            d.id,
            d.code,
            d.status,
            d.description
        });

        return Ok(dockDTOs);
    }


    [AdminFilter]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Dock newDock)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Validation: Prevent user from passing the "code" field
        if (!string.IsNullOrEmpty(newDock.code))
        {
            return BadRequest(new { Message = "Code cannot be provided. It will be auto-generated." });
        }

        // Generate the dock (the service will auto-generate the code)
        var createdDock = await _dockService.AddDockAsync(newDock);
        return CreatedAtAction(nameof(Get), new { id = createdDock.id }, createdDock);
    }


   [AdminFilter]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Dock updatedDock)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Retrieve the existing dock from the database
        var existingDock = await _dockService.GetDockById(id);
        if (existingDock == null)
        {
            return NotFound(new { Message = "Dock not found" });
        }

        // Validation: Check if the user is trying to modify the code
        if (!string.IsNullOrEmpty(updatedDock.code) && updatedDock.code != existingDock.code)
        {
            return BadRequest(new { Message = "Code cannot be modified" });
        }

        // Retain the existing code value
        updatedDock.code = existingDock.code;

        // Ensure the ID matches
        if (id != updatedDock.id)
        {
            return BadRequest(new { Message = $"Provided ID ({id}) does not match dock ID" });
        }

        // Update the dock using the service
        var success = await _dockService.UpdateDockAsync(id, updatedDock);

        if (!success)
        {
            return NotFound(new { Message = "Dock could not be updated" });
        }

        return Ok(updatedDock);
    }




    // Delete a dock
    [AdminFilter]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _dockService.RemoveDockAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
