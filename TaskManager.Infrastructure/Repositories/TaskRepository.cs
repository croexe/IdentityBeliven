using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Database;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository, IDisposable
{
    private readonly IMapper _mapper;
    private readonly TaskDbContext _context;
    private readonly INotificationService _notificationService;

    public TaskRepository(IMapper mapper, TaskDbContext context, INotificationService notificationService)
    {
        _mapper = mapper;
        _context = context;
        _notificationService = notificationService;
    }

    public async Task<TaskDto> AddTaskAsync(TaskDto dto)
    {
        try
        {
            var task = _mapper.Map<Domain.Entities.Task>(dto);

            var entry = await _context.Tasks.AddAsync(task);

            if (!string.IsNullOrWhiteSpace(task.DeveloperId)) 
            {
                entry.Property(t => t.DeveloperId).CurrentValue = task.DeveloperId;
                var recipient = await _context.Users.Where(u => u.Id == task.DeveloperId).Select(u => u.Email).FirstAsync();
                var project = await _context.Projects.Include(p => p.ProjectManager).FirstAsync(p => p.Id == task.ProjectId);
                var sender = project.ProjectManager.Email;
                var title = task.Title;

                _notificationService.SendAsync(recipient, sender, title, null);
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
            var task = await _context.Tasks.Include(t => t.Project).Where(t => t.Id == developerToTaskDto.TaskId).FirstAsync();
            var recipient = await _context.Users.Where(u => u.Id == developerToTaskDto.DeveloperId).Select(u => u.Email).FirstAsync();
            var project = await _context.Projects.Include(p => p.ProjectManager).FirstAsync(p => p.Id == task.ProjectId);
            var sender = project.ProjectManager.Email;
            var title = task.Title;

            _notificationService.SendAsync(recipient, sender, title, null);
            task.DeveloperId = developerToTaskDto.DeveloperId;
            await _context.SaveChangesAsync();

            var taskDto = _mapper.Map<TaskDto>(task);
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
            var task = await _context.Tasks.Include(t => t.State).FirstAsync(t => t.Id == taskStateDto.TaskId);
            var sender = await _context.Users.Where(u => u.Id == task.DeveloperId).Select(u => u.Email).SingleAsync();
            var project = await _context.Projects.Include(p => p.ProjectManager).FirstAsync(p => p.Id == task.ProjectId);
            task.StateId = taskStateDto.StateId;

            if(task.StateId == 2) 
            {
                var recipient = project.ProjectManager.Email;
                var title = task.Title;
                var status = task.State.StateName;
                _notificationService.SendAsync(recipient, sender, title, status);
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

    public void Dispose()
    {
        _context.Dispose();
    }
}