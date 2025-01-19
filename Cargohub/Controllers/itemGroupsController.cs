using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cargohub.Filters;


namespace Cargohub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Item_GroupsController : ControllerBase
    {

        private readonly IItemGroupService _IitemGroupsService;

        public Item_GroupsController(IItemGroupService IitemGroupsService)
        {
            _IitemGroupsService = IitemGroupsService;
        }

        [HttpGet("amount/{amount}")]
        public async Task<IActionResult> GetAll(int amount)
        {
            var supplier = await _IitemGroupsService.GetAllItemGroups(amount);
            return Ok(supplier);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ItemGroup itemGroup = await _IitemGroupsService.GetItemGroupById(id);
            if (itemGroup == null)
            {
                return NotFound();
            }
            return Ok(itemGroup);
        }

        [AdminFilter]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemGroup itemGroup)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != itemGroup.id)
            {
                return BadRequest($"Location Id {id} does not match");
            }

            var updated = await _IitemGroupsService.UpdateItem_Groups(itemGroup);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _IitemGroupsService.DeleteItem_Groups(id);
            if (!deleted)
            {
                return NotFound();

            }
            return NoContent();
        }

    }
}