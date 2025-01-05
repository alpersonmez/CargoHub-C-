using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cargohub.Filters;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var warehouse = await _warehouseService.GetAllWareHouses();
        return Ok(warehouse);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Warehouse warehouse = await _warehouseService.GetWareHouseById(id);
        if (warehouse == null)
        {
            return NotFound();
        }
        return Ok(warehouse);
    }

    [AdminFilter]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Warehouse New)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Warehouse createdwarehouse = await _warehouseService.AddWarehouse(New);
        return CreatedAtAction(nameof(Get), new { id = createdwarehouse.id }, createdwarehouse);
    }
    
    //PUT moet ik nog ff checken hoe en wat
    
    [AdminFilter]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _warehouseService.DeleteWarehouse(id);
        if (!deleted)
        {
            return NotFound();

        } 
        return NoContent();
    }
    
}