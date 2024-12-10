using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cargohub.Filters;

namespace Cargohub.Controllers{

    [ApiController]
    [Route("api/[controller]")]
    public class Item_linesController : ControllerBase{

        private readonly IitemlinesService _IitemlinesService;

        public Item_linesController(IitemlinesService IitemlinesService)
        {
            _IitemlinesService = IitemlinesService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var itemlines = _IitemlinesService.GetAllItem_lines();
            return Ok(itemlines);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var itemLine = _IitemlinesService.GetItem_linesById(id);
            if (itemLine == null) return NotFound("ItemLine not found");
            return Ok(itemLine);
        }

        [AdminFilter]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Item_lines updatedItemLine)
        {
            var updated = _IitemlinesService.UpdateItem_lines(id, updatedItemLine);
            if (updated == null) return NotFound("ItemLine not found");
            return Ok(updated);
        }

        [AdminFilter]
        [HttpDelete("{id}")]
        public IActionResult DeleteItem_lines(int id)
        {
            var deleted = _IitemlinesService.DeleteItem_lines(id);
            if (!deleted) return NotFound("ItemLine not found");
            return Ok("ItemLine deleted successfully");
        }
        }
}