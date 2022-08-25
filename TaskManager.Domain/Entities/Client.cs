namespace TaskManager.Domain.Entities;

public sealed class Client : Entity
{
    public string Name { get; set; }
    public string Sector { get; set; }
    public string Note { get; set; }

    public IEnumerable<Project> Projects { get; set; }
}