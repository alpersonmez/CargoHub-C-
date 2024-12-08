using System.Text;
using Microsoft.AspNetCore.Mvc;
using Cargohub.Services;
using Cargohub.Models;
using Cargohub.Filters;

namespace Cargohub.Controllers
{
    [Route("api/[controller]")]
    public class ShipmentController : Controller
    {
        private readonly IShipmentService shipmentService;

        public ShipmentController(IShipmentService _shipmentService)
        {
            shipmentService = _shipmentService;
        }

        [HttpGet]
        public IActionResult GetShipments()
        {
            List<Shipment> shipments = shipmentService.GetShipments();
            if (shipments is null || !shipments.Any()) return NotFound("empty");
            return Ok(shipments);
        }

        [HttpGet("{id}")]
        public IActionResult GetShipment(int id)
        {
            Shipment shipment = shipmentService.GetShipment(id);
            if (shipment is null) return BadRequest("shipment with the given id does not exist.");
            return Ok(shipment);
        }

        // [HttpGet("/{Id}/items")]
        // public IActionResult GetShipmentItems(int id)
        // {
        //     List<Item> shipments = shipmentService.GetItems(id);
        //     if (shipments is null) return BadRequest("Shipment with the given id does not exist.");
        //     return Ok(shipments);
        // }
        [AdminFilter]
        [HttpPost]
        public IActionResult AddShipment([FromBody] Shipment shipment)
        {
            if (shipmentService.AddShipment(shipment)) return Ok($"Succesfully added {shipment}");
            if (shipment is null) return BadRequest("Shipment is empty.");
            return BadRequest("Shipment Already exists.");
        }
        [AdminFilter]
        [HttpPut("{id}")]
        public IActionResult UpdateShipment(int id, [FromBody] Shipment shipment)
        {
            if (shipmentService.UpdateShipment(id, shipment) == false) return BadRequest("Failed to update shipment. Check if you have the correct id and a valid shipment.");
            return Ok($"Succesfully updated shipment with id: {id}");
        }
        [AdminFilter]
        [HttpDelete("{id}")]
        public IActionResult DeleteShipment(int id)
        {
            if (shipmentService.DeleteShipment(id) == false) return BadRequest("shipment with given id doesnt exist.");
            return Ok($"Succesfully deleted shipment with id: {id}");
        }



    }
}