using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Database;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository, IDisposable
{
    private readonly IMapper _mapper;
    private readonly TaskDbContext _context;

    public TaskRepository(IMapper mapper, TaskDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<TaskDto> AddTaskAsync(TaskDto dto)
    {
        try
        {
            var task = _mapper.Map<Domain.Entities.Task>(dto);
            var entry = await _context.Tasks.AddAsync(task);
            
            var developerId = task.UserId.Any();

            if (developerId) 
            {
                entry.Member("DeveloperId").CurrentValue = task.UserId;
                entry.State = EntityState.Added;
            }

            await _context.SaveChangesAsync();

            var taskDto = _mapper.Map<TaskDto>(task);
            return taskDto;
        }
        catch (Exception)
        { 
            throw;
        }
    }

    public async Task<TaskDto> AssignDeveloperToTaskAsync(DeveloperToTaskDto developerToTaskDto)
    {
        try
        {
            var entry = await _context.Tasks.Where(t => t.Id == developerToTaskDto.TaskId).FirstAsync();
            _context.Entry(entry).Property("DeveloperId").CurrentValue = developerToTaskDto.DeveloperId;
            await _context.SaveChangesAsync();

            var taskDto = _mapper.Map<TaskDto>(entry);
            return taskDto;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<TaskDto> UpdateStateOfTaskAsync(TaskStateDto taskStateDto)
    {
        try
        {
            var task = await _context.Tasks.Where(t => t.Id == taskStateDto.TaskId).SingleAsync();
            task.StateId = taskStateDto.StateId;
            await _context.SaveChangesAsync();

            var taskDto = _mapper.Map<TaskDto>(task);
            return taskDto;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}