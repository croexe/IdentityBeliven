using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Controllers
{
    [Authorize("Project Manager")]
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
        public Task<ClientDto> Add(ClientDto dto)
        {
            var kol = repository.AddClient(dto);
            return kol;
        }
    }
}
