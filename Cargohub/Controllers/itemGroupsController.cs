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

        private readonly IitemGroupsService _IitemGroupsService;

        public Item_GroupsController(IitemGroupsService IitemGroupsService)
        {
            _IitemGroupsService = IitemGroupsService;
        }

        [HttpGet]
        public ActionResult<List<Item>> GetAllItem_groups()
        {
            return null;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            ItemGroup item_lines = await _IitemGroupsService.GetItem_GroupsById(Id);
            if (item_lines == null)
            {
                return NotFound();
            }
            return Ok(item_lines);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemGroup item_Lines)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //modelstate is om te kijken of de fields kloppen

            if (id != item_Lines.id)
            {
                return BadRequest($"Location Id {id} does not match");
            }

            var updated = await _IitemGroupsService.UpdateItem_Groups(item_Lines);

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