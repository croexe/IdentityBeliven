namespace TaskManager.Domain.DTOs;

public sealed class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    public int PriorityId { get; set; }
    public int StateId { get; set; }
}