using System.Text;
using Microsoft.AspNetCore.Mvc;
using Cargohub.Services;
using Cargohub.Models;
using Cargohub.Filters;

namespace Cargohub.Controllers
{
    [Route("api/[controller]")]
    public class TransferController : Controller
    {
        private readonly ITransferService transferService;

        public TransferController(ITransferService _transferService)
        {
            transferService = _transferService;
        }

        [HttpGet]
        public IActionResult GetTransfers()
        {
            List<Transfer> transfers = transferService.GetTransfers();
            if (transfers is null || !transfers.Any()) return NotFound("empty");
            return Ok(transfers);
        }

        [HttpGet("{id}")]
        public IActionResult GetTransfer(int id)
        {
            Transfer transfer = transferService.GetTransfer(id);
            if (transfer is null) return BadRequest("transfer with the given id does not exist.");
            return Ok(transfer);
        }

        [HttpGet("/{Id}/items")]
        public IActionResult GetTransferItems(int id)
        {
            List<Item> transfers = transferService.GetItems(id);
            if (transfers is null) return BadRequest("Transfer with the given id does not exist.");
            return Ok(transfers);
        }

        [AdminFilter]
        [HttpPost]
        public IActionResult AddTransfer([FromBody] Transfer transfer)
        {
            if (transferService.AddTransfer(transfer)) return Ok($"Succesfully added {transfer}");
            if (transfer is null) return BadRequest("Transfer is empty.");
            return BadRequest("Transfer Already exists.");
        }
        [AdminFilter]
        [HttpPut("{id}")]
        public IActionResult UpdateTransfer(int id, [FromBody] Transfer transfer)
        {
            if (transferService.UpdateTransfer(id, transfer) == false) return BadRequest("Failed to update transfer. Check if you have the correct id and a valid transfer.");
            return Ok($"Succesfully updated transfer with id: {id}");
        }
        [AdminFilter]
        [HttpDelete("{id}")]
        public IActionResult DeleteTransfer(int id)
        {
            if (transferService.DeleteTransfer(id) == false) return BadRequest("Transfer with given id doesnt exist.");
            return Ok($"Succesfully deleted transfer with id: {id}");
        }



    }
}