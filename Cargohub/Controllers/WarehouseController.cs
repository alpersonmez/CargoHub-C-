using Cargohub.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Cargohub.Services;

namespace Cargohub.Controllers;

[Route("api/v1/Warehouses")]
public class WarehouseController : Controller
{
    private readonly IWarehouseService warehouseService;

    public WarehouseController(IWarehouseService _warehouseService)
    {
        warehouseService = _warehouseService;
    }

    [HttpGet("{id}")]
    public ActionResult<Warehouse> GetWarehouse(int id)
    {
        Warehouse? warehouse = warehouseService.GetWarehouse(id);
        if (warehouse == null) return Ok($"there is no warehouse with id {id}, {warehouse}");
        return Ok(warehouse);
    }


    [HttpGet]
    public ActionResult<List<Warehouse>> GetAllWarehouses()
    {
        List<Warehouse> warehouseList = warehouseService.GetAllWarehouses();
        if (warehouseList == null) return Ok("there are no warehouses");
        return Ok(warehouseList);
    }

    [HttpPost("new")]
    public ActionResult<Warehouse> PostWarehouse([FromBody] object objWarehouse)
    {
        try
        {
            // Deserialize the object into the Warehouse class
            Warehouse warehouse = JsonSerializer.Deserialize<Warehouse>(objWarehouse.ToString());

            if (warehouse == null)
            {
                return BadRequest("Invalid warehouse data.");
            }

            // Now you have a Warehouse object that you can use
            Warehouse? postedWarehouse = warehouseService.AddWarehouse(warehouse);
            if (postedWarehouse == null)
            {
                return BadRequest("Warehouse already exists");
            }

            return Ok($"Successfully posted warehouse \n{JsonSerializer.Serialize(postedWarehouse, new JsonSerializerOptions { WriteIndented = true })}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }

    }
}