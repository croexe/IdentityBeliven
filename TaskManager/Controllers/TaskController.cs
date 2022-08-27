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
    [Route("create")]
    public async Task<TaskDto> Add([FromBody] TaskDto dto)
    {
        var task = await _repository.AddTask(dto);
        return task;
    }

    [Authorize(Roles = UserRoles.Manager)]
    [HttpPost]
    [Route("assign")]
    public async Task<DeveloperToTaskDto> AssignTask([FromBody] DeveloperToTaskDto dto)
    {
        var task = await _repository.AssignDeveloperToTask(dto);
        return task;
    }

    [Authorize(Roles = UserRoles.Developer)]
    [HttpPost]
    [Route("change/state")]
    public async Task<TaskStateDto> ChangeTaskState([FromBody] TaskStateDto dto)
    {
        var task = await _repository.UpdateStateOfTask(dto);
        return task;
    }
}