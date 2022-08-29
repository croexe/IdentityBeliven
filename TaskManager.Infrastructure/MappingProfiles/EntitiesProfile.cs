using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.MappingProfiles;

public class EntitiesProfile : Profile
{
    public EntitiesProfile()
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

        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.Id,
            from => from.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name,
            from => from.MapFrom(src => src.Name))
            .ForMember(dest => dest.ClientId,
            from => from.MapFrom(src => src.ClientId))
            .AfterMap((src, dest) => dest.ProjectManagerId = src.UserId);

        CreateMap<ProjectDto, Project>()
            .ForMember(dest => dest.Name,
            from => from.MapFrom(src => src.Name))
            .ForMember(dest => dest.ClientId,
            from => from.MapFrom(src => src.ClientId))
            .AfterMap((src, dest) =>  dest.UserId = src.ProjectManagerId);
        
        CreateMap<Domain.Entities.Task, TaskDto>()
            .ForMember(dest => dest.Id,
            from => from.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title,
            from => from.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description,
            from => from.MapFrom(src => src.Description))
            .ForMember(dest => dest.StateId,
            from => from.MapFrom(src => src.StateId))
            .ForMember(dest => dest.PriorityId,
            from => from.MapFrom(src => src.PriorityId))
            .ForMember(dest => dest.ProjectId,
            from => from.MapFrom(src => src.ProjectId))
            .AfterMap((src, dest) => dest.DeveloperId = src.UserId);

        CreateMap<TaskDto, Domain.Entities.Task>()
            .ForMember(dest => dest.Title,
            from => from.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description,
            from => from.MapFrom(src => src.Description))
            .ForMember(dest => dest.StateId,
            from => from.MapFrom(src => src.StateId))
            .ForMember(dest => dest.PriorityId,
            from => from.MapFrom(src => src.PriorityId))
            .ForMember(dest => dest.ProjectId,
            from => from.MapFrom(src => src.ProjectId))
            .AfterMap((src, dest) => dest.UserId = src.DeveloperId);
        
    }
}