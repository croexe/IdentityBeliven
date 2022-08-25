using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Models;

public sealed class RegisterModel : LoginModel
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }
}