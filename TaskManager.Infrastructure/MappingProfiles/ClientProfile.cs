using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.MappingProfiles;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<ClientDto, Client>()
            .ForMember(dest => dest.Name,
            from => from.MapFrom(src => src.Name))
            .ForMember(dest => dest.Sector,
            from => from.MapFrom(src => src.Sector))
            .ForMember(dest => dest.Note,
            from => from.MapFrom(src => src.Note));

        CreateMap<Client, ClientDto>().ForMember(
            dest => dest.Id,
            from => from.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name,
            from => from.MapFrom(src => src.Name));

    }
}