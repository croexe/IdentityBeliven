using TaskManager.Domain.Entities;

namespace TaskManager.Domain.DTOs;

public sealed class TaskDto : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    public int PriorityId { get; set; }
    public int StateId { get; set; }
    public string DeveloperId { get; set; }
}