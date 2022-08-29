using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Helpers;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Controllers;

[Authorize(Roles = UserRoles.Manager)]
[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectRepository _repository;

    public ProjectController(IProjectRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("create")]
    public async Task<IActionResult> Add([FromBody] ProjectDto dto)
    {
        if (!ModelState.IsValid) return BadRequest();
        var project = await _repository.AddAsyncProject(dto);
        return Ok(project);
    }
}