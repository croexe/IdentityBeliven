using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.DTOs;
using TaskManager.Infrastructure.Database;
using TaskManager.Infrastructure.Repositories;
using Xunit;

namespace TaskManager.Repository.Tests;

public class RepositoriesTest
{
    private DbContextOptions<TaskDbContext> _options;
    private IMapper _mapper;
    public RepositoriesTest()
    {
        _options = TaskDbHelper.TaskManagerDbContextOptionsSQLiteInMemory();
        TaskDbHelper.CreateDataBaseSQLiteInMemory(_options);
        _mapper = AutoMapperFactory.BuildAutoMapper();
    }

    public void Dispose()
    {
        TaskDbHelper.CleanDataBase(_options);
    }

    [Fact]
    public async Task ShouldAddClientToDatabaseSuccessfuly()
    {
        using(var context = new TaskDbContext(_options))
        {
            var clientRepository = new ClientRepository(context, _mapper);
            var result = await clientRepository.AddAsyncClient(new ClientDto()
            {
                Id = 2,
                Name = "Apple",
                Note = "Big client",
                Sector = "Tech"
            });

           Assert.NotNull(result);
        }
    }

    [Fact]
    public async Task ShouldAddClientToDatabaseFailure()
    {
        using (var context = new TaskDbContext(_options))
        {
            var clientRepository = new ClientRepository(context, _mapper);
            var result = await clientRepository.AddAsyncClient(new ClientDto()
            {
                Id = 2,
                Name = "Apple",
                Note = "Big client",
                Sector = "Tech"
            });

            Exception err = null;
            try
            {
                var result1 = await clientRepository.AddAsyncClient(new ClientDto()
                {
                    Id = 2,
                    Name = null,
                    Note = "Big client",
                    Sector = "Tech"
                });
            }
            catch (Exception ex)
            {
                err = ex;
            }
            Assert.NotNull(err);
        }
    }

    [Fact]
    public async Task ShouldAddProjectToDatabaseSuccessfuly()
    {
        using(var context = new TaskDbContext(_options))
        {
            var projectRepository = new ProjectRepository(_mapper, context);
            var result = projectRepository.AddAsyncProject(new ProjectDto()
            {
                Id = 1,
                Name = "Refactor of Web Apis",
                ClientId = 2,
                ProjectManagerId = ""
            });

            Assert.IsType<Exception>(result.Result);
        }
    }

    [Fact]
    public async Task ShouldAddProjectToDatabaseFailure()
    {
        using (var context = new TaskDbContext(_options))
        {
            var projectRepository = new ProjectRepository(_mapper, context);
            var result = projectRepository.AddAsyncProject(new ProjectDto()
            {
                Id = 1,
                Name = "Refactor of Web Apis",
                ClientId = 2,
                ProjectManagerId = "b2334e9a-a8d6-49b8-8a13-b77e1729d1b5"
            });


            Exception err = null;
            try
            {
                var result1 = await projectRepository.AddAsyncProject(new ProjectDto()
                {
                    Id = 1,
                    Name = "Refactor of Web Apis",
                    ClientId = 2,
                    ProjectManagerId = "b2334e9a-a8d6-49b8-8a13-b77e1729d1b5"
                });
            }
            catch (Exception ex)
            {
                err = ex;
            }
            Assert.NotNull(err);
        }
    }
}
