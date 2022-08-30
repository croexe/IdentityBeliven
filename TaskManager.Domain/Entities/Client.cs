using System.Collections.ObjectModel;

namespace TaskManager.Domain.Entities;

public class Client : Entity
{
    public string Name { get; set; }
    public string? Sector { get; set; }
    public string? Note { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new Collection<Project>();
}