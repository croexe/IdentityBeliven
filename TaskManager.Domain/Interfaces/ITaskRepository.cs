using TaskManager.Domain.DTOs;

namespace TaskManager.Domain.Interfaces;

public interface ITaskRepository
{
    Task<TaskDto> AddTask(TaskDto dto);
    Task AssignDeveloperToTask(int taskId, string developerId);
    Task UpdateStateOfTask(int taskId, int stateId);
}