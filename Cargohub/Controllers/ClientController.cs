using Microsoft.AspNetCore.Mvc;
using Cargohub.Models;
using Cargohub.Services;
using Cargohub.Filters;
using System;

namespace Cargohub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("amount/{amount}")]
        public async Task<IActionResult> GetAllClients(int amount)
        {
            var clients = await _clientService.GetAllClients(amount);
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetClientById(id);
            if (client == null)
                return NotFound($"Client with ID {id} not found.");
            return Ok(client);
        }

        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetClientOrders(int id)
        {
            var orders = await _clientService.GetClientOrders(id);
            if (orders == null)
                return NotFound($"Client with ID {id} has no orders.");
            return Ok(orders);
        }

        [AdminFilter]
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(m => m.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new
                {
                    message = "Validation failed for the client request.",
                    errors
                });
            }

            try
            {
                Client createdClient = await _clientService.AddClient(client);
                return CreatedAtAction(nameof(GetClientById), new { id = createdClient.id }, createdClient);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }


        [AdminFilter]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != client.id)
            {
                return BadRequest($"Client Id {id} does not match");
            }

            try
            {
                var updatedClient = await _clientService.UpdateClient(client);

                if (!updatedClient)
                {
                    return NotFound();
                }

                return Ok(updatedClient);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [AdminFilter]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var deleted = await _clientService.DeleteClient(id);
            if (!deleted)
                return NotFound($"Client with ID {id} not found.");
            return NoContent();
        }
    }
}
