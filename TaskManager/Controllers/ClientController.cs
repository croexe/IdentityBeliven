using Microsoft.AspNetCore.Authorization;
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
            var client = repository.AddClient(dto);
            return client;
        }
    }
}