using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookApi.Data;
using BookApi.Data.Interfaces;
using BookApi.Data.Models;
using BookApi.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookApi.Controllers;

public class AuthController(BookContext _bookContext,IConfiguration _configuration, IUserRepository _userRepository)
    : Controller
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _bookContext.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !_userRepository.VerifyPassword(request.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid username or password");
        }

        var token = _userRepository.GenerateJwtToken(user);
        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var existingUser = await _bookContext.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (existingUser != null)
        {
            return BadRequest("User with this email already exists");
        }

        var passwordHash = _userRepository.HashPassword(request.Password);
        var newUser = new User
        {
            Email = request.Email,
            PasswordHash = passwordHash,
        };
        _bookContext.Users.Add(newUser);
        await _bookContext.SaveChangesAsync();

        var token = _userRepository.GenerateJwtToken(newUser);
        return Ok(new { Token = token });
    }
}