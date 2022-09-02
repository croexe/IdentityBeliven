using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using TaskManager.Controllers;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Interfaces;
using Xunit;

namespace TaskManager.Tests;

public class ClientControllerTests
{
    private readonly Mock<IClientRepository> _mockRepo;
    private readonly ClientController _controller;

    public ClientControllerTests()
    {
        _mockRepo = new Mock<IClientRepository>();
        _controller = new ClientController(_mockRepo.Object);
    }


    [Fact]
    public async Task ShouldCreateClientSuccessfuly()
    {
        ClientDto empt = new()
        {
            Name = "Apple",
            Note = "Big client",
            Sector = "Tech"
        };

        _mockRepo.Setup(c => c.AddAsyncClient(It.IsAny<ClientDto>())).Returns(Task.FromResult<ClientDto>(empt));

        ClientDto clientDto = new()
        {
            Name = "Apple",
            Note = "Big client",
            Sector = "Tech2"
        };

        var response = await _controller.Add(clientDto);
        _mockRepo.Verify(c => c.AddAsyncClient(It.IsAny<ClientDto>()), Times.Once);

        Assert.IsType<OkObjectResult>(response);
        Assert.NotNull(response);
        Assert.NotEqual(empt, clientDto);
    }

}