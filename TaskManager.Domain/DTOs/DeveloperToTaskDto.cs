using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.DTOs;

public sealed class DeveloperToTaskDto
{
    [Required]
    public int TaskId { get; set; }
    [Required]
    public string DeveloperId { get; set; }
}