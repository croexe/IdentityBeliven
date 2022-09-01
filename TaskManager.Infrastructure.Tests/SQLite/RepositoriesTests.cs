using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.DTOs;
using TaskManager.Infrastructure.Database;
using TaskManager.Infrastructure.Repositories;
using Xunit;

namespace TaskManager.Infrastructure.Tests.SQLite;

public class RepositoriesTests
{
    TaskDbContext _context;
    IMapper _mapper;

    public RepositoriesTests()
    {
        var helper = new TaskManagerTestHelpers();
        _context = helper.TaskManagerDbContextSQLiteInMemory();
        _mapper = AutoMapperFactory.BuildAutoMapper();
    }

    [Fact]
    public async void ClientRepositorySuccess()
    {
        var clientRepository = new ClientRepository(_context, _mapper);
        var client = await clientRepository.AddAsyncClient(new ClientDto()
        {
            Id = 1,
            Name = "Apple",
            Note = "Big client",
            Sector = "Tech"
        });

        Assert.NotNull(client);
        Assert.IsType<ClientDto>(client);
    }

    //[Fact]
    //public async
}