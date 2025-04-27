using LibraryManagement.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Domain.Models;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime MembershipStartDate { get; set; }
    public DateTime? MembershipExpiryDate { get; set; }
    public string? MemberId { get; set; }
}