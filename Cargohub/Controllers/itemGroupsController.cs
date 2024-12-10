using Cargohub.Models;
using Cargohub.Services;
using Cargohub.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cargohub.Controllers
{

    [ApiController]
    [Route("api/v1/item_groups")]
    public class Item_GroupsController : ControllerBase
    {

        private readonly IItemGroupService _itemGroupsService;

        public Item_GroupsController(IItemGroupService IitemGroupsService)
        {
            _itemGroupsService = IitemGroupsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAllItem_groups()
        {
            List<ItemGroup>? allItemsGroups = await _itemGroupsService.GetAllItem_Groups();
            if (allItemsGroups == null || !allItemsGroups.Any())
                return NotFound("There are no Item Groups available.");
            return Ok(allItemsGroups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            ItemGroup itemGroup = await _itemGroupsService.GetItem_GroupById(Id);
            if (itemGroup == null)
            {
                return NotFound();
            }
            return Ok(itemGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemGroup itemGroup)
        {
            if (id != itemGroup.id)
            {
                return BadRequest($"Item Group Id {id} does not match");
            }

            bool updated = await _itemGroupsService.UpdateItem_Groups(itemGroup);

            if (!updated)
            {
                return NotFound();
            }

            return Ok(itemGroup);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _itemGroupsService.DeleteItem_Groups(id);
            if (!deleted)
            {
                return NotFound();

            }
            return NoContent();
        }

        [AdminFilter]
        [HttpPost("new")]
        public async Task<IActionResult> Post([FromBody] ItemGroup newItemGroup)
        {
            bool posted = await _itemGroupsService.PostItemGroup(newItemGroup);
            if (!posted) return NotFound("this itemgroup already exists or it can not be posted");
            return Ok(newItemGroup);
        }
    }
}