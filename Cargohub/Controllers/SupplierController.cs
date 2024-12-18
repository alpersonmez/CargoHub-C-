using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cargohub.Filters;

[ApiController]
[Route("api/[controller]")]
public class SupplierController : ControllerBase
{
    private readonly ISupplierService _SupplierService;

    public SupplierController(ISupplierService SupplierService)
    {
        _SupplierService = SupplierService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var supplier = await _SupplierService.GetAllSupplier();
        return Ok(supplier);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Supplier supplier = await _SupplierService.GetSupplierById(id);
        if (supplier == null)
        {
            return NotFound();
        }
        return Ok(supplier);
    }

    [AdminFilter]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Supplier New)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Supplier createdsupplier = await _SupplierService.AddSupplier(New);
        return CreatedAtAction(nameof(Get), new { id = createdsupplier.id }, createdsupplier);
    }

    [AdminFilter]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Supplier supplier)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); //modelstate is om te kijken of de fields kloppen
        
        if (id != supplier.id)
        {
            return BadRequest($"Location Id {id} does not match");
        }
        
        var updated = await _SupplierService.UpdateSupplier(supplier);

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
        bool deleted = await _SupplierService.DeleteSupplier(id);
        if (!deleted)
        {
            return NotFound();

        } 
        return NoContent();
    }
}
