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
        
        [HttpPost("{shipmentId}/orders")]
        public async Task<IActionResult> AddOrdersToShipment(int shipmentId, [FromBody] List<int> orderIds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _shipmentService.AddOrdersToShipment(shipmentId, orderIds);
                if (result)
                {
                    return Ok(new { message = "Orders successfully added to shipment." });
                }

                return BadRequest(new { error = "Failed to add orders to shipment." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("release-order/{orderId}")]
        public async Task<IActionResult> ReleaseOrderFromShipment(int orderId)
        {
            try
            {
                var result = await _shipmentService.ReleaseOrderFromShipment(orderId);
                if (result)
                {
                    return Ok(new { message = $"Order {orderId} has been successfully released from the shipment." });
                }

                return BadRequest(new { error = "Failed to release the order from the shipment." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
