namespace LibraryManagement.Api.Auth;

public class LoginResponseDto
{
    public string Message { get; set; }
    
    public string? Token { get; set; }
    
    public DateTime? Expiration { get; set; }
    
    public IList<string>? Roles { get; set; }

    public bool Success {
        get;
        set;
    }
}