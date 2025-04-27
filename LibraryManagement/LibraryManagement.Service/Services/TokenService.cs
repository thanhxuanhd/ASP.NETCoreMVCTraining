using LibraryManagement.Domain.Models;
using LibraryManagement.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagement.Service.Services;


public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
        _config = config;
        // Ensure your key is strong and stored securely (e.g., User Secrets, Key Vault)
        var keyString = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured");
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
    }

    public Task<(string token, DateTime expiration)> GenerateJwtToken(User user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Subject (standard claim for user ID)
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName ?? string.Empty), // Often used for username
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique token identifier
        };

        // Add role claims
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        // You could add custom claims here too

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature); // Use strong algorithm

        var issuer = _config["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not configured");
        var audience = _config["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not configured");
        var expiryMinutes = _config.GetValue<int>("Jwt:ExpiryMinutes", 60); // Default to 60 mins

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        var expiration = tokenDescriptor.Expires.Value; // Get the calculated expiration

        return Task.FromResult((tokenString, expiration)); // Wrap in Task.FromResult for async signature
    }
}