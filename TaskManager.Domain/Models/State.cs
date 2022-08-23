namespace TaskManager.Domain.Models;

public sealed class State : Entity
{
    public string StateName { get; set; }

    public Task Task { get; set; }
}