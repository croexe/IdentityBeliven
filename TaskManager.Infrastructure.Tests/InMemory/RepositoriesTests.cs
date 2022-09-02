using AutoMapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.DTOs;
using TaskManager.Infrastructure.Database;
using TaskManager.Infrastructure.Repositories;
using Xunit;

namespace TaskManager.Infrastructure.Tests.InMemory;

public class RepositoriesTests : IClassFixture<TaskWebApplicationFactory<Program>>
{

}