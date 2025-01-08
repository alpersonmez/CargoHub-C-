using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cargohub.Filters;
namespace Cargohub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var shipments = await _shipmentService.GetAllShipments();
            return Ok(shipments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Shipment shipment = await _shipmentService.GetShipmentById(id);
            if (shipment == null)
            {
                return NotFound();
            }
            return Ok(shipment);
        }

        [AdminFilter]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Shipment newShipment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Shipment createdShipment = await _shipmentService.AddShipment(newShipment);
            return CreatedAtAction(nameof(Get), new { id = createdShipment.id }, createdShipment);
        }

        [AdminFilter]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Shipment shipment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != shipment.id)
            {
                return BadRequest($"Shipment Id {id} does not match");
            }

            var updated = await _shipmentService.UpdateShipment(shipment);

            if (!updated)
            {
                return NotFound();
            }

            return Ok(shipment);
        }

        [AdminFilter]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _shipmentService.DeleteShipment(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
