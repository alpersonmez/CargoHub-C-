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

        [HttpGet("amount/{amount}")]
        public async Task<IActionResult> GetTransfers(int amount)
        {
            var transfers = await transferService.GetTransfers(amount);
            return Ok(transfers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransfer(int id)
        {
            Transfer transfer = await transferService.GetTransfer(id);
            if (transfer is null) return NotFound("transfer with the given id does not exist.");
            return Ok(transfer);
        }

        // [HttpGet("/{Id}/items")]
        // public IActionResult GetTransferItems(int id)
        // {
        //     List<Item> transfers = transferService.GetItems(id);
        //     if (transfers is null) return BadRequest("Transfer with the given id does not exist.");
        //     return Ok(transfers);
        // }

        [AdminFilter]
        [HttpPost]
        public async Task<IActionResult> AddTransfer([FromBody] Transfer transfer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Transfer CreatedTransfer = await transferService.AddTransfer(transfer);
            return CreatedAtAction(nameof(GetTransfer), new { id = CreatedTransfer.id }, CreatedTransfer);
        }
        [AdminFilter]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransfer(int id, [FromBody] Transfer transfer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != transfer.id)
            {
                return BadRequest($"given Id {id} does not match");
            }

            var updated = await transferService.UpdateTransfer(transfer);

            if (!updated)
            {
                return NotFound();
            }

            return Ok(transfer);
        }
        [AdminFilter]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransfer(int id)
        {
            bool deleted = await transferService.DeleteTransfer(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }



    }
}
