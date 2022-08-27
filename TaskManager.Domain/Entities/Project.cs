using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Domain.Entities;

public class Project : Entity
{
    public string Name { get; set; }
    public int ClientId { get; set; }
    [NotMapped]
    public string UserId { get; set; }

    public Client Client { get; set; }
    public IdentityUser ProjectManager { get; set; }
    public virtual ICollection<Task> Tasks { get; set; } = new Collection<Task>();
}