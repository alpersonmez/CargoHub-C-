using System.Text;
using Microsoft.AspNetCore.Mvc;
using Cargohub.Services;
using Cargohub.Models;

namespace Cargohub.Controllers
{
    [Route("api/Orders")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            List<Order> orders = orderService.GetOrders();
            if (orders is null || !orders.Any()) return NotFound("empty");
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            Order order = orderService.GetOrder(id);
            if (order is null) return BadRequest("Order with the given id does not exist.");
            return Ok(order);
        }

        // [HttpGet("/{Id}/items")]
        // public IActionResult GetOrderItems(int id)
        // {
        //     List<Items> orders = orderService.GetOrderItems(id);
        //     if (orders is null) return BadRequest("Order with the given id does not exist.");
        //     return Ok(orders);
        // }

        [HttpPost]
        public IActionResult AddOrder([FromBody] Order order)
        {
            if (orderService.AddOrder(order)) return Ok($"Succesfully added {order}");
            if (order is null) return BadRequest("Order is empty.");
            return BadRequest("Order Already exists.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order order)
        {
            if (orderService.UpdateOrder(id, order) == false) return BadRequest("Failed to update order. Check if you have the correct id and a valid order.");
            return Ok($"Succesfully updated order with id: {id}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            if (orderService.DeleteOrder(id) == false) return BadRequest("Order with given id doesnt exist.");
            return Ok($"Succesfully deleted order with id: {id}");
        }



    }
}