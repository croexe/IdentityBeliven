using Microsoft.AspNetCore.Identity;

namespace TaskManager.Domain.Models;

public sealed class Project : Entity
{
    public string Name { get; set; }
    public int ClientId { get; set; }

    public Client Client { get; set; }
    public IdentityUser ProjectManager { get; set; }
    public IEnumerable<Task> Tasks { get; set; }
}