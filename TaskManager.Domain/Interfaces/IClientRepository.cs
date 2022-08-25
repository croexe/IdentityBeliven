using TaskManager.Domain.DTOs;

namespace TaskManager.Domain.Interfaces;

public interface IClientRepository
{
    Task<ClientDto> AddClient(ClientDto dto);
}