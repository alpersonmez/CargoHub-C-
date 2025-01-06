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

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Order order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [AdminFilter]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order newOrder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Order CreatedOrder = await _orderService.AddOrder(newOrder);
            return CreatedAtAction(nameof(Get), new { id = CreatedOrder.id }, CreatedOrder);
        }

        [AdminFilter]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != order.id)
            {
                return BadRequest($"Shipment Id {id} does not match");
            }

            var updated = await _orderService.UpdateOrder(order);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
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
    }
}
