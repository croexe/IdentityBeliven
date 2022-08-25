namespace TaskManager.Domain.Entities;

public sealed class Priority : Entity
{
    public string PriorityName { get; set; }
    public Task Task { get; set; }
}