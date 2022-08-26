using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Models;

public sealed class RegisterModel : LoginModel
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Type of user is required.")]
    public string Usertype { get; set; }
}