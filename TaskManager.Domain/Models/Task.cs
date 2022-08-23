using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskManager.Domain.Models;

public sealed class Task : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    public int PriorityId { get; set; }
    public int StateId { get; set; }
    public string AspNetUserId { get; set; }

    public Project Project { get; set; }
    public State State { get; set; }
    public Priority Priority { get; set; }
}