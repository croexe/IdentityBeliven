using TaskManager.Domain.DTOs;

namespace TaskManager.Domain.Interfaces;

public interface ITaskRepository
{
    Task<TaskDto> AddTask(TaskDto dto);
    Task<DeveloperToTaskDto> AssignDeveloperToTask(DeveloperToTaskDto developerToTaskDto);
    Task<TaskStateDto> UpdateStateOfTask(TaskStateDto taskStateDto);
}