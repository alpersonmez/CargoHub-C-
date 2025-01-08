using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
    public async Task<IActionResult> GetAll(int amount)
    {
        var item = await _itemService.GetAllItems(amount);
        return Ok(item);
    }

    [HttpGet("{uid}")]
    public async Task<IActionResult> Get(string uid)
    {
        Item item = await _itemService.GetItemByUid(uid);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [AdminFilter]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Item New)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Item CreatedItem = await _itemService.AddItem(New);
        return CreatedAtAction(nameof(Get), new { uid = CreatedItem.uid }, CreatedItem);
    }



    [AdminFilter]
    [HttpPut("{uid}")]
    public async Task<IActionResult> Update(string uid, [FromBody] Item item)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); //modelstate is om te kijken of de fields kloppen

        if (uid != item.uid)
        {
            return BadRequest($"Location Id {uid} does not match");
        }

        var updated = await _itemService.UpdateItem(item);

        if (!updated)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [AdminFilter]
    [HttpDelete("{uid}")]
    public async Task<IActionResult> Delete(string uid)
    {
        bool deleted = await _itemService.DeleteItem(uid);
        if (!deleted)
        {
            return NotFound();

        }
        return NoContent();
    }
}
