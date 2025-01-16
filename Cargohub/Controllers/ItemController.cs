using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cargohub.Filters;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int amount)
    {
        var items = await _itemService.GetAllItems(amount);
        return Ok(items);
    }

    [HttpGet("{uid}")]
    public async Task<IActionResult> Get(string uid)
    {
        var item = await _itemService.GetItemByUid(uid);
        if (item == null)
        {
            return NotFound(new { Message = "Item not found" });
        }
        return Ok(item);
    }

    [AdminFilter]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Item newItem)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdItem = await _itemService.AddItemAsync(newItem);
        return CreatedAtAction(nameof(Get), new { uid = createdItem.uid }, createdItem);
    }

    [AdminFilter]
    [HttpPut("{uid}")]
    public async Task<IActionResult> Update(string uid, [FromBody] Item updatedItem)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (uid != updatedItem.uid)
        {
            return BadRequest(new { Message = $"Provided UID ({uid}) does not match item UID" });
        }

        var success = await _itemService.UpdateItemAsync(uid, updatedItem);

        if (!success)
        {
            return NotFound(new { Message = "Item not found or could not be updated" });
        }

        return Ok(updatedItem);
    }

    [AdminFilter]
    [HttpDelete("{uid}")]
    public async Task<IActionResult> Delete(string uid)
    {
        var success = await _itemService.RemoveItemAsync(uid);
        if (!success)
        {
            return NotFound(new { Message = "Item not found or could not be deleted" });
        }

        return NoContent();
    }
}
