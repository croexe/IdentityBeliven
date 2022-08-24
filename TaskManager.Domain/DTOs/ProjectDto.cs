namespace TaskManager.Domain.DTOs;

public sealed class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ClientId { get; set; }
}