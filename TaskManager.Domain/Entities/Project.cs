using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace TaskManager.Domain.Entities;

public class Project : Entity
{
    public string Name { get; set; }
    public int ClientId { get; set; }
    public string ProjectManagerId { get; set; }

    public Client Client { get; set; }
    public IdentityUser ProjectManager { get; set; }
    public virtual ICollection<Task> Tasks { get; set; }
}