using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cargohub.Controllers
{

    [ApiController]
    [Route("api/v1/item_groups")]
    public class Item_GroupsController : ControllerBase
    {

        private readonly IItemGroupsService _IitemGroupsService;

        public Item_GroupsController(IItemGroupsService IitemGroupsService)
        {
            _IitemGroupsService = IitemGroupsService;
        }

        [HttpGet]
        public ActionResult<List<Item>> GetAllItem_groups()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            ItemGroup itemGroup = await _IitemGroupsService.GetItem_GroupById(Id);
            if (itemGroup == null)
            {
                return NotFound();
            }
            return Ok(itemGroup);
        }

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