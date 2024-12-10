using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cargohub.Filters;


[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var locations = await _locationService.GetAllLocations();
        return Ok(locations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Location location = await _locationService.GetLocationById(id);
        if (location == null)
        {
            return NotFound();
        }
        return Ok(location);
    }

    [AdminFilter]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Location New)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Location createdLocation = await _locationService.AddLocation(New);
        return CreatedAtAction(nameof(Get), new { id = createdLocation.Id }, createdLocation);
    }

    [AdminFilter]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Location location)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); //modelstate is om te kijken of de fields kloppen
        
        if (id != location.Id)
        {
            return BadRequest($"Location Id {id} does not match");
        }
        
        var updated = await _locationService.UpdateLocation(location);

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
        bool deleted = await _locationService.DeleteLocation(id);
        if (!deleted)
        {
            return NotFound();

        } 
        return NoContent();
    }
}
