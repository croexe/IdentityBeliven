using System.ComponentModel.DataAnnotations;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.DTOs;

public sealed class ProjectDto : Entity
{
    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 1)]
    public string Name { get; set; }
    [Required(ErrorMessage = "The field {0} is required")]
    public int ClientId { get; set; }
    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(450, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 1)]
    public string ProjectManagerId { get; set; }
}