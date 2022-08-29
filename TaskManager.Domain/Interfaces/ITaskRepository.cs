using TaskManager.Domain.DTOs;

namespace TaskManager.Domain.Interfaces;

public interface ITaskRepository
{
    Task<TaskDto> AddTaskAsync(TaskDto dto);
    Task<TaskDto> AssignDeveloperToTaskAsync(DeveloperToTaskDto developerToTaskDto);
    Task<TaskDto> UpdateStateOfTaskAsync(TaskStateDto taskStateDto);
}