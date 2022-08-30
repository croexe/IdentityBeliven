using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.DTOs;

public sealed class TaskStateDto
{
    [Required]
    public int TaskId { get; set; }
    [Required]
    public int StateId { get; set; }
}