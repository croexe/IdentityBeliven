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
    [Route("create")]
    public async Task<ProjectDto> Add([FromBody] ProjectDto dto)
    {
        var project = await _repository.AddProject(dto);
        return project;
    }
}