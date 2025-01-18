using Microsoft.AspNetCore.Mvc;
using Cargohub.Models;
using Cargohub.Services;
using Cargohub.Filters;

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

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAllClients();
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

        [AdminFilter]
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Client createdClient = await _clientService.AddClient(client);
            return CreatedAtAction(nameof(GetClientById), new { id = createdClient.id }, createdClient);
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
            
            var updatedClient = await _clientService.UpdateClient(client);
            
            if (!updatedClient)
            {
                return NotFound();
            }

            return Ok(updatedClient);
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
