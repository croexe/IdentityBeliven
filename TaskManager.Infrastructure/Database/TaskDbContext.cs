using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Database;

public class TaskDbContext : IdentityDbContext<IdentityUser>
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Domain.Entities.Task> Tasks { get; set; }
    public virtual DbSet<State> States { get; set; }
    public virtual DbSet<Priority> Priorities { get; set; }
}