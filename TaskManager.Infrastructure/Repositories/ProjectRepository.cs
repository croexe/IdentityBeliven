using AutoMapper;
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

    public async Task<ProjectDto> AddAsyncProject(ProjectDto dto)
    {
        try
        {
            var project = _mapper.Map<Project>(dto);
            var entry = await _context.Projects.AddAsync(project);
            
            await _context.SaveChangesAsync();
            var projectDto = _mapper.Map<ProjectDto>(project);
            return projectDto;
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