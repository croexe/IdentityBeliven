using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Helpers;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Controllers
{
    [Authorize(Roles = UserRoles.Manager)]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository repository;
        
        public ClientController(IClientRepository _repository)
        {
            repository = _repository;
        }
        [HttpPost]
        [Route("create")]
        public Task<ClientDto> Add([FromBody]ClientDto dto)
        {
            var kol = repository.AddClient(dto);
            return kol;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAll()
        {
            var b = 100;
            return Ok(b);
        }
    }
}
