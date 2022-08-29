using System.ComponentModel.DataAnnotations;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.DTOs;

public sealed class TaskDto : Entity
{
    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(170, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 1)]
    public string Title { get; set; }
    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(250, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 1)]
    public string Description { get; set; }
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public int PriorityId { get; set; }
    [Required]
    public int StateId { get; set; }
    public string DeveloperId { get; set; }
}