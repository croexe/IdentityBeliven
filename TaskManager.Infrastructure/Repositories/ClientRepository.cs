using AutoMapper;
using Serilog;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Database;

namespace TaskManager.Infrastructure.Repositories;

public class ClientRepository : IClientRepository, IDisposable
{
    private readonly TaskDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public ClientRepository(TaskDbContext context, IMapper mapper, ILogger logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ClientDto> AddAsyncClient(ClientDto dto)
    {
        ClientDto clientDto = null;
        try
        {
            var client = _mapper.Map<Client>(dto);
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            clientDto = _mapper.Map<ClientDto>(client);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
        }
        return clientDto;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}