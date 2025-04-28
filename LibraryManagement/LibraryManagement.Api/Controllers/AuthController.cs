using LibraryManagement.Api.Auth;
using LibraryManagement.Domain.Models;
using LibraryManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Service.Dtos; 
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    ITokenService tokenService,
    IConfiguration configuration,
    ILogger<AuthController> logger) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if user already exists (by username or email)
        var existingUserByEmail = await userManager.FindByEmailAsync(registerDto.Email);
        if (existingUserByEmail != null)
        {
            return BadRequest(new { Message = "Email is already registered." });
        }

        var existingUserByUsername = await userManager.FindByNameAsync(registerDto.Username);
        if (existingUserByUsername != null)
        {
            return BadRequest(new { Message = "Username is already taken." });
        }

        var user = new User
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            MemberId = ""
        };

        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            // Log detailed errors for debugging
            foreach (var error in result.Errors)
            {
                logger.LogWarning("Registration failed for {Email}: {Code} - {Description}", registerDto.Email,
                    error.Code, error.Description);
            }

            // Return generic error to client
            return BadRequest(new
                { Message = "User registration failed.", Errors = result.Errors.Select(e => e.Description) });
        }

        logger.LogInformation("User {Username} registered successfully.", user.UserName);

        // Optional: Assign a default role (e.g., "Member")
        // Ensure the role exists first
        // string defaultRole = "Member";
        if (!await roleManager.RoleExistsAsync(Roles.Member))
        {
            await roleManager.CreateAsync(new Role { Name = Roles.Member });
            logger.LogInformation("Role '{RoleName}' created.", Roles.Member);
        }

        await userManager.AddToRoleAsync(user, Roles.Member);
        logger.LogInformation("User {Username} added to role '{RoleName}'.", user.UserName, Roles.Member);


        return Ok(new { Message = "User registered successfully." });
    }

    // POST: api/auth/login
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        User? user = null;
        if (loginDto.Username.Contains('@'))
        {
            user = await userManager.FindByEmailAsync(loginDto.Username);
        }
        else
        {
            user = await userManager.FindByNameAsync(loginDto.Username);
        }

        if (user == null)
        {
            logger.LogWarning("Login failed: User '{Login}' not found.", loginDto.Username);
            return Unauthorized(new LoginResponseDto()
            {
                Message = "Invalid credentials."
            });
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isPasswordValid)
        {
            logger.LogWarning("Login failed: Invalid password for user '{Username}'.", user.UserName);
            return Unauthorized(new LoginResponseDto() { Message = "Invalid credentials." });
        }

        var roles = await userManager.GetRolesAsync(user);

        // --- Generate Tokens ---
        var accessToken = await tokenService.GenerateJwtToken(user, roles);
        await userManager.UpdateAsync(user); // Save refresh token changes to DB

        logger.LogInformation("User {Username} logged in successfully.", user.UserName);

        return Ok(new LoginResponseDto
        {
            Token = accessToken.token,
            Expiration = accessToken.expiration,
            Message = "Sucess",
            Roles = roles,
            Success = true
        });
    }
}