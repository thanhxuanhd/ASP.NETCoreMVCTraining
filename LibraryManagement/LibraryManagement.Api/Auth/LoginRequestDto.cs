using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.Auth;

public class LoginRequestDto
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}