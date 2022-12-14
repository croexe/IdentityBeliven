using Microsoft.AspNetCore.Identity;

namespace TaskManager.Domain.Entities;

public sealed class Task : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    public int? PriorityId { get; set; }
    public int StateId { get; set; }
    public string? DeveloperId { get; set; }

    public IdentityUser Developer { get; set; }
    public Project Project { get; set; }
    public State State { get; set; }
    public Priority Priority { get; set; }
}