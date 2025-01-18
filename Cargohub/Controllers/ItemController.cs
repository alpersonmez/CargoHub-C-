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

    [HttpGet("amount/{amount}")]
    public async Task<IActionResult> GetAll(int amount)
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
            return NotFound();
        }
        return Ok(item);
    }

    [HttpGet("item-line/{itemLineId}")]
    public async Task<IActionResult> GetByItemLine(int itemLineId)
    {
        var item = await _itemService.GetItemsByItemLineAsync(itemLineId);
        if (item == null)
        {
            return NotFound(new { Message = "Item line not found" });
        }
        return Ok(item);
    }

    [HttpGet("item-group/{itemGroupId}")]
    public async Task<IActionResult> GetByItemGroup(int itemGroupId)
    {
        var item = await _itemService.GetItemsByItemGroupAsync(itemGroupId);
        if (item == null)
        {
            return NotFound(new { Message = "Item group not found" });
        }
        return Ok(item);
    }

    [HttpGet("item-type/{itemTypeId}")]
    public async Task<IActionResult> GetByItemType(int itemTypeId)
    {
        var item = await _itemService.GetItemsByItemTypeAsync(itemTypeId);
        if (item == null)
        {
            return NotFound(new { Message = "Item type not found" });
        }
        return Ok(item);
    }

    [HttpGet("supplier/{supplierId}")]
    public async Task<IActionResult> GetBySupplier(int supplierId)
    {
        var item = await _itemService.GetItemsBySupplierAsync(supplierId);
        if (item == null)
        {
            return NotFound(new { Message = "Supplier not found" });
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
        bool deleted = await _itemService.RemoveItemAsync(uid);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
