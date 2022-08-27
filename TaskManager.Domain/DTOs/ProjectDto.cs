using TaskManager.Domain.Entities;

namespace TaskManager.Domain.DTOs;

public sealed class ProjectDto : Entity
{
    public string Name { get; set; }
    public int ClientId { get; set; }
    public string ProjectManagerId { get; set; }
}