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
        private readonly IOrderShipmentService _orderShipmentService;

        public ShipmentController(IShipmentService shipmentService, IOrderShipmentService orderShipmentService)
        {
            _shipmentService = shipmentService;
            _orderShipmentService = orderShipmentService;
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
                return BadRequest($"Shipment ID {id} does not match.");
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

        // Link multiple orders to a shipment
        [HttpPost("{shipmentId}/link-orders")]
        public async Task<IActionResult> LinkOrdersToShipment(int shipmentId, [FromBody] LinkOrdersToShipmentDto dto)
        {
            try
            {
                var result = await _orderShipmentService.LinkOrdersToShipment(shipmentId, dto.OrderIds);
                if (result)
                {
                    return Ok(new { message = "Orders successfully linked to shipment." });
                }

                return BadRequest(new { error = "Failed to link orders to shipment." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Get all orders for a shipment
        [HttpGet("{shipmentId}/orders")]
        public async Task<IActionResult> GetOrdersForShipment(int shipmentId)
        {
            var orders = await _orderShipmentService.GetOrdersForShipment(shipmentId);
            return Ok(orders);
        }

        [HttpPost("{shipmentId}/disconnect-orders")]
        public async Task<IActionResult> DisconnectOrdersFromShipment(int shipmentId, [FromBody] DisconnectOrdersDto dto)
        {
            try
            {
                var result = await _shipmentService.DisconnectOrdersFromShipment(shipmentId, dto.OrderIds);
                if (result)
                {
                    return Ok(new { message = "Orders successfully disconnected from shipment." });
                }

                return BadRequest(new { error = "Failed to disconnect orders from shipment." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
