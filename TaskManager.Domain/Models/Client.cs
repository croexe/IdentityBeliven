namespace TaskManager.Domain.Models;

public sealed class Client : Entity
{
    public string Name { get; set; }
    public string Sector { get; set; }
    public string Note { get; set; }

    public IEnumerable<Project> Projects { get; set; }
}