using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Database;

namespace TaskManager.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly TaskDbContext _context;
    private readonly IMapper _mapper;

    public ClientRepository(TaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ClientDto> AddClient(ClientDto dto)
    {
        var client = _mapper.Map<Client>(dto);
        try
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {

            throw;
        }
        var dtoReturn = _mapper.Map<ClientDto>(client);
        return dtoReturn;
    }
}