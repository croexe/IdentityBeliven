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

public class ProjectRepository : IProjectRepository, IDisposable
{
    private readonly IMapper _mapper;
    private readonly TaskDbContext _context;

    public ProjectRepository(IMapper mapper, TaskDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ProjectDto> AddProject(ProjectDto dto)
    {
        try
        {
            var project = _mapper.Map<Project>(dto);
            var entry = await _context.Projects.AddAsync(project);
            entry.Member("ProjectManagerId").CurrentValue = project.UserId;
            
            await _context.SaveChangesAsync();

            return dto;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}