using Cargohub.Models;
using Cargohub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cargohub.Filters;

namespace Cargohub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderShipmentService _orderShipmentService;

        public OrderController(IOrderService orderService, IOrderShipmentService orderShipmentService)
        {
            _orderService = orderService;
            _orderShipmentService = orderShipmentService;
        }

        [HttpGet("amount/{amount}")]
        public async Task<IActionResult> GetAll(int amount)
        {
            var orders = await _orderService.GetAllOrders(amount);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }


        [AdminFilter]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDto newOrder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Use the service to add the new order
            OrderDto createdOrder = await _orderService.AddOrder(newOrder);

            // Return a 201 Created response with the newly created order
            return CreatedAtAction(nameof(GetById), new { id = createdOrder.id }, createdOrder);
        }


        [AdminFilter]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != order.id)
            {
                return BadRequest($"Order ID {id} does not match.");
            }

            var updated = await _orderService.UpdateOrder(order);

            if (!updated)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [AdminFilter]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _orderService.DeleteOrder(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Link multiple shipments to an order
        [HttpPost("{orderId}/link-shipments")]
        public async Task<IActionResult> LinkShipmentsToOrder(int orderId, [FromBody] LinkShipmentsToOrderDto dto)
        {
            try
            {
                var result = await _orderShipmentService.LinkShipmentsToOrder(orderId, dto.ShipmentIds);
                if (result)
                {
                    return Ok(new { message = "Shipments successfully linked to order." });
                }

                return BadRequest(new { error = "Failed to link shipments to order." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Get all shipments for an order
        [HttpGet("{orderId}/shipments")]
        public async Task<IActionResult> GetShipmentsForOrder(int orderId)
        {
            var shipments = await _orderShipmentService.GetShipmentsForOrder(orderId);
            return Ok(shipments);
        }

        [HttpPost("{orderId}/disconnect-shipments")]
        public async Task<IActionResult> DisconnectShipmentsFromOrder(int orderId, [FromBody] DisconnectShipmentsDto dto)
        {
            try
            {
                var result = await _orderService.DisconnectShipmentsFromOrder(orderId, dto.ShipmentIds);
                if (result)
                {
                    return Ok(new { message = "Shipments successfully disconnected from order." });
                }

                return BadRequest(new { error = "Failed to disconnect shipments from order." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
