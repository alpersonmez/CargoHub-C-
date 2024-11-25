using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cargohub.Controllers{

    [ApiController]
    [Route("api/v1/item_lines")]
    public class Item_linesController : ControllerBase{

        private readonly IitemlinesService _IitemlinesService;

        public Item_linesController(IitemlinesService IitemlinesService)
        {
            _IitemlinesService = IitemlinesService;
        }

        [HttpGet]
        public ActionResult<List<Item>> GetAllItem_lines()
        {
            return Ok(_IitemlinesService.GetAllItem_lines());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            Item_lines item_lines = await _IitemlinesService.GetItem_linesById(Id);
            if (item_lines == null)
            {
                return NotFound();
            }
            return Ok(item_lines);
        }
    
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Item_lines item_Lines)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //modelstate is om te kijken of de fields kloppen
            
            if (id != item_Lines.id)
            {
                return BadRequest($"Location Id {id} does not match");
            }
            
            var updated = await _IitemlinesService.UpdateItem_lines(item_Lines);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _IitemlinesService.DeleteItem_lines(id);
        if (!deleted)
        {
            return NotFound();

        } 
        return NoContent();
    }



    }
}