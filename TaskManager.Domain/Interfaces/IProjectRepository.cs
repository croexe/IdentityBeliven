using TaskManager.Domain.DTOs;

namespace TaskManager.Domain.Interfaces;

public interface IProjectRepository
{
    Task<ProjectDto> AddProject(ProjectDto dto);
}