using TaskManager.Domain.DTOs;

namespace TaskManager.Domain.Interfaces;

public interface IProjectRepository
{
    Task<ProjectDto> AddAsyncProject(ProjectDto dto);
}