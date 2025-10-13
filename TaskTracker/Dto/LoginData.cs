using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Dto;

public class LoginData
{
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(100)]
    [MinLength(5)]
    public string Password { get; set; }
}