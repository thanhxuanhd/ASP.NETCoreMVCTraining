using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Domain.Models;

public class Role : IdentityRole<Guid>
{
    public string? Description { get; set; }
}