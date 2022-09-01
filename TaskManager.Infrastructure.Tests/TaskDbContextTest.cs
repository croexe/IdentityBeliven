using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Helpers;
using TaskManager.Domain.Models;
using TaskManager.Infrastructure.Database;

namespace TaskManager.Infrastructure.Tests;

public class TaskDbContextTest : TaskDbContext
{
    public DbSet<Client> ClientsMockDbSet { get; set; }
    public DbSet<Project> ProjectsMockDbSet { get; set; }
    public DbSet<Domain.Entities.Task> TasksMockDbSet { get; set; }
    public DbSet<State> StatesMockDbSet { get; set; }
    public DbSet<Priority> PrioritiesMockDbSet { get; set; }

    public List<Priority> PrioritiesMockData { get; set; }
    public List<State> StatesMockData { get; set; }
    public List<Client> ClientsMockData { get; set; }
    public List<Project> ProjectsMockData { get; set; }
    public List<Domain.Entities.Task> TasksMockData { get; set; }

    public TaskDbContextTest(DbContextOptions<TaskDbContext> options) : base(options)
    {
        SeedDatabase();
    }



    public void SeedDatabase()
    {
        ClientsMockData = ClientData();
        ClientsMockDbSet.AddRangeAsync(ClientsMockData);

        ProjectsMockData = ProjectData();
        ProjectsMockDbSet.AddRangeAsync(ProjectsMockData);

        TasksMockData = TaskData();
        TasksMockDbSet.AddRangeAsync(TasksMockData);

        base.SaveChangesAsync();
    }

    private List<State> StateData()
    {
        List<State> statesMockData = new List<State>()
        {
            new State()
            {
                StateName = "BackLog"
            },
            new State()
            {
                StateName = "To Do"
            },
            new State()
            {
                StateName = "In Progress"
            },
            new State()
            {
                StateName = "Done"
            }
        };
        return statesMockData;
    }

    private List<Priority> PriorityData()
    {
        List<Priority> priorityMockData = new List<Priority>()
        {
            new Priority()
            {
                PriorityName = "Low"
            },
            new Priority()
            {
                PriorityName = "Medium"
            },
            new Priority()
            {
                PriorityName = "High"
            }
        };
        return priorityMockData;
    }

    private List<Client> ClientData()
    {
        List<Client> clientsMockData = new List<Client>()
        {
            new Client()
            {
                Name = "Apple",
                Note = "Big client",
                Sector = "Tech"
            },
            new Client()
            {
                Name = "Intesa San Paolo"
            }
        };
        return clientsMockData;
    }

    private List<Project> ProjectData()
    {
        List<Project> projectMockData = new List<Project>()
        {
            new Project()
            {
                Name = "Refactor of Web Apis",
                ClientId = 2,
                ProjectManagerId = "b2334e9a-a8d6-49b8-8a13-b77e1729d1b5"
            },
            new Project()
            {
                Name = "New Front End",
                ClientId = 3,
                ProjectManagerId = "b2334e9a-a8d6-49b8-8a13-b77e1729d1b5"
            }
        };
        return projectMockData;
    }

    private List<Domain.Entities.Task> TaskData()
    {
        List<Domain.Entities.Task> taskMockData = new List<Domain.Entities.Task>()
        {
            new Domain.Entities.Task()
            {
                Title = "Service Layer",
                ProjectId = 3,
                StateId = 1,
                Description = "All bussines rules should be applied here and not in repositories",
                PriorityId = 2,
                DeveloperId = "90149c80-754f-44a6-9f09-6975492ae019"
            },
            new Domain.Entities.Task()
            {
                Title = "Repositories Layer",
                ProjectId = 3,
                StateId = 2,
                Description = "All bussines rules should be applied here and not in repositories",
                PriorityId = 3
            }
        };
        return taskMockData;
    }
}