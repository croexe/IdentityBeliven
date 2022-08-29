using System.ComponentModel.DataAnnotations;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.DTOs
{
    public sealed class ClientDto : Entity
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(80, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 1)]
        public string Sector { get; set; }
        [StringLength(250, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 1)]
        public string Note { get; set; }
    }
}