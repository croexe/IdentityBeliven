using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public async Task<TaskDto> AddTask(TaskDto dto)
    {
        try
        {
            var task = _mapper.Map<Domain.Entities.Task>(dto);
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return dto;
        }
        catch (Exception)
        { 
            throw;
        }
    }

    public async Task<DeveloperToTaskDto> AssignDeveloperToTask(DeveloperToTaskDto developerToTaskDto)
    {
        try
        {
            var task = await _context.Tasks.Where(t => t.Id == developerToTaskDto.TaskId).SingleAsync();
            task.Developer.Id = developerToTaskDto.DeveloperId;
            await _context.SaveChangesAsync();

            return developerToTaskDto;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<TaskStateDto> UpdateStateOfTask(TaskStateDto taskStateDto)
    {
        try
        {
            var task = await _context.Tasks.Where(t => t.Id == taskStateDto.TaskId).SingleAsync();
            task.StateId = taskStateDto.StateId;
            await _context.SaveChangesAsync();

            return taskStateDto;
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