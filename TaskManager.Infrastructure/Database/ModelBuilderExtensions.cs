using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Database;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<State>()
            .HasData(
                new State() { Id = 1, StateName = "Backlog" },
                new State() { Id = 2, StateName = "ToDo" },
                new State() { Id = 3, StateName = "InProgress" },
                new State() { Id = 4, StateName = "Done" }

            );

        modelBuilder.Entity<Priority>()
            .HasData(
                new Priority() { Id = 1, PriorityName = "Low" },
                new Priority() { Id = 2, PriorityName = "Medium" },
                new Priority() { Id = 3, PriorityName = "High" }
            );
    }
}