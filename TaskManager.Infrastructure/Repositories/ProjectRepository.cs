using AutoMapper;
using Serilog;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Database;

namespace TaskManager.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository, IDisposable
{
    private readonly IMapper _mapper;
    private readonly TaskDbContext _context;
    private readonly ILogger _logger;

    public ProjectRepository(IMapper mapper, TaskDbContext context, ILogger logger)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }

    public async Task<ProjectDto> AddAsyncProject(ProjectDto dto)
    {
        ProjectDto projectDto = null;
        try
        {
            var project = _mapper.Map<Project>(dto);
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            projectDto = _mapper.Map<ProjectDto>(project);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
        }
        return projectDto;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}