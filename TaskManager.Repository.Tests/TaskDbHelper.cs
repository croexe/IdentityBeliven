using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Database;

namespace TaskManager.Repository.Tests;


public static class TaskDbHelper
{

    public static DbContextOptions<TaskDbContext> TaskManagerDbContextOptionsSQLiteInMemory()
    {
        var connectionStringBuilder =
            new SqliteConnectionStringBuilder { DataSource = ":memory:" };
        var connection = new SqliteConnection(connectionStringBuilder.ToString());

        var options = new DbContextOptionsBuilder<TaskDbContext>()
            .UseSqlite(connection)
            .Options;

        return options;
    }

    public static async void CreateDataBaseSQLiteInMemory(DbContextOptions<TaskDbContext> options)
    {
        await using (var context = new TaskDbContext(options))
        {
            await context.Database.OpenConnectionAsync();
            await context.Database.EnsureCreatedAsync();
            CreateData(context);
        }
    }


    public static async System.Threading.Tasks.Task CreateData(TaskDbContext taskContext)
    {

        await taskContext.Users.AddAsync(new IdentityUser()
        {
            Id = "b2334e9a-a8d6-49b8-8a13-b77e1729d1b5",
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
            AccessFailedCount = 0,
        });

        await taskContext.Users.AddAsync(new IdentityUser()
        {
            Id = "90149c80-754f-44a6-9f09-6975492ae019",
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
            AccessFailedCount = 0,
        });

        await taskContext.Priorities.AddAsync(new Priority()
        {
            Id = 1,
            PriorityName = "Low"
        });
        await taskContext.States.AddAsync(new State()
        {
            Id = 1,
            StateName = "BackLog"
        });
        await taskContext.Clients.AddAsync(new Client()
        {
            Id = 1,
            Name = "Apple",
            Note = "Big client",
            Sector = "Tech"
        });
        await taskContext.Projects.AddAsync(new Project()
        {
            Id = 1,
            Name = "Refactor of Web Apis",
            ClientId = 2,
            ProjectManagerId = "b2334e9a-a8d6-49b8-8a13-b77e1729d1b5"
        });
        await taskContext.Tasks.AddAsync(new Domain.Entities.Task()
        {
            Id = 1,
            Title = "Service Layer",
            ProjectId = 3,
            StateId = 1,
            Description = "All bussines rules should be applied here and not in repositories",
            PriorityId = 2,
            DeveloperId = "90149c80-754f-44a6-9f09-6975492ae019"
        });
    }

    public static async System.Threading.Tasks.Task CleanDataBase(DbContextOptions<TaskDbContext> options)
    {
        await using (var taskContext = new TaskDbContext(options))
        {
            var priority = await taskContext.Priorities.FirstAsync();
            taskContext.Priorities.Remove(priority);

            var state = await taskContext.States.FirstAsync();
            taskContext.States.Remove(state);

            var client = await taskContext.Clients.FirstAsync();
            taskContext.Clients.Remove(client);

            var project = await taskContext.Projects.FirstAsync();
            taskContext.Projects.Remove(project);

            var task = await taskContext.Tasks.FirstAsync();
            taskContext.Tasks.Remove(task);

            foreach (var user in taskContext.Users)
                taskContext.Users.Remove(user);

            foreach (var role in taskContext.Roles)
                taskContext.Roles.Remove(role);

            await taskContext.SaveChangesAsync();
        }
    }
}
