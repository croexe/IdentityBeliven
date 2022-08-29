using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Helpers;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _repository;

    public TaskController(ITaskRepository repository)
    {
        _repository = repository;
    }

    [Authorize(Roles = UserRoles.Manager)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("create")]
    public async Task<IActionResult> Add([FromBody] TaskDto dto)
    {
        if (!ModelState.IsValid) return BadRequest();
        var task = await _repository.AddTaskAsync(dto);
        return Ok(task);
    }

    [Authorize(Roles = UserRoles.Manager)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("assign")]
    public async Task<IActionResult> AssignTask([FromBody] DeveloperToTaskDto dto)
    {
        if (!ModelState.IsValid) return BadRequest();
        var task = await _repository.AssignDeveloperToTaskAsync(dto);
        return Ok(task);
    }

    [Authorize(Roles = UserRoles.Developer)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("change/state")]
    public async Task<IActionResult> ChangeTaskState([FromBody] TaskStateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest();
        var task = await _repository.UpdateStateOfTaskAsync(dto);
        return Ok(task);
    }
}