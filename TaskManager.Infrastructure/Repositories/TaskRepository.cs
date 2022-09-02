using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Database;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository, IDisposable
{
    private readonly IMapper _mapper;
    private readonly TaskDbContext _context;
    private readonly ILogger _logger;
    private readonly INotificationService _notificationService;

    public TaskRepository(IMapper mapper, TaskDbContext context, ILogger logger, INotificationService notificationService)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
        _notificationService = notificationService;
    }

    public async Task<TaskDto> AddTaskAsync(TaskDto dto)
    {
        TaskDto taskDto = null;
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

                _notificationService.Send(recipient, sender, title, null);
            }
            await _context.SaveChangesAsync();

            taskDto = _mapper.Map<TaskDto>(task);
        }
        catch (Exception ex)
        { 
            _logger.Error(ex.Message);
        }
        return taskDto;
    }

    public async Task<TaskDto> AssignDeveloperToTaskAsync(DeveloperToTaskDto developerToTaskDto)
    {
        TaskDto taskDto = null;
        try
        {
            var task = await _context.Tasks.Include(t => t.Project).Where(t => t.Id == developerToTaskDto.TaskId).FirstAsync();
            var recipient = await _context.Users.Where(u => u.Id == developerToTaskDto.DeveloperId).Select(u => u.Email).FirstAsync();
            var project = await _context.Projects.Include(p => p.ProjectManager).FirstAsync(p => p.Id == task.ProjectId);
            var sender = project.ProjectManager.Email;
            var title = task.Title;

            _notificationService.Send(recipient, sender, title, null);
            task.DeveloperId = developerToTaskDto.DeveloperId;
            await _context.SaveChangesAsync();

            taskDto = _mapper.Map<TaskDto>(task);
            
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
        }
        return taskDto;
    }

    public async Task<TaskDto> UpdateStateOfTaskAsync(TaskStateDto taskStateDto)
    {
        TaskDto taskDto = null;
        try
        {
            var task = await _context.Tasks.Include(t => t.State).FirstAsync(t => t.Id == taskStateDto.TaskId);

            if(task.DeveloperId == null)
            {
                return _mapper.Map<TaskDto>(task);
            }

            var sender = await _context.Users.Where(u => u.Id == task.DeveloperId).Select(u => u.Email).FirstAsync();
            var project = await _context.Projects.Include(p => p.ProjectManager).FirstAsync(p => p.Id == task.ProjectId);
            task.StateId = taskStateDto.StateId;

            if(task.StateId == 2) 
            {
                var recipient = project.ProjectManager.Email;
                var title = task.Title;
                var status = task.State.StateName;
                _notificationService.Send(recipient, sender, title, status);
            }

            await _context.SaveChangesAsync();

            taskDto = _mapper.Map<TaskDto>(task);
            
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
        }
        return taskDto;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}