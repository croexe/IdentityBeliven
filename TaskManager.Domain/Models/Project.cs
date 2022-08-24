namespace TaskManager.Domain.Models;

public sealed class Project : Entity
{
    public string Name { get; set; }
    public int ClientId { get; set; }
    public string UserId { get; set; }

    public Client Client { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public IEnumerable<Task> Tasks { get; set; }
}