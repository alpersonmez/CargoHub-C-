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
        public IActionResult GetAllClients()
        {
            var clients = _clientService.GetAllClients();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            var client = _clientService.GetClientById(id);
            if (client == null)
                return NotFound($"Client with ID {id} not found.");
            return Ok(client);
        }

        [AdminFilter]
        [HttpPost]
        public IActionResult CreateClient([FromBody] Client client)
        {
            if (client == null)
                return BadRequest("Client is null.");

            var createdClient = _clientService.CreateClient(client);
            return CreatedAtAction(nameof(GetClientById), new { id = createdClient.id }, createdClient);
        }

        [AdminFilter]
        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody] Client client)
        {
            if (client == null || client.id != id)
                return BadRequest("Client ID mismatch.");

            var updatedClient = _clientService.UpdateClient(client);
            if (updatedClient == null)
                return NotFound($"Client with ID {id} not found.");

            return Ok(updatedClient);
        }

        [AdminFilter]
        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var deleted = _clientService.DeleteClient(id);
            if (!deleted)
                return NotFound($"Client with ID {id} not found.");
            return NoContent();
        }
    }
}
