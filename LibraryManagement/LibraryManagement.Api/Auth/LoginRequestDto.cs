using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.Auth;

public class LoginRequestDto
{
    [Required]
    public required string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}