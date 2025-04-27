using LibraryManagement.Domain.Models;

namespace LibraryManagement.Service.Interfaces;

public interface ITokenService
{
    Task<(string token, DateTime expiration)> GenerateJwtToken(User user, IList<string> role);
}