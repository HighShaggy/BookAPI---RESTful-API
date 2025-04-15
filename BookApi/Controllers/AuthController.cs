using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookApi.Data;
using BookApi.Data.Interfaces;
using BookApi.Data.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookApi.Controllers;

public class AuthController(BookContext _bookContext, IConfiguration _configuration, IUserRepository _userRepository) : Controller
{
    [HttpGet("login")]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // 1. Находим пользователя в БД
        var user = await _bookContext.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);
    
        // 2. Проверяем что пользователь существует и пароль верный
        if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid username or password");
        }
    
        // 3. Генерируем токен
        var token = GenerateJwtToken(user);
    
        // 4. Возвращаем токен
        return Ok(new { Token = token });
    }
    private bool VerifyPassword(string enteredPassword, string storedHash)
    {
        // Реализуй проверку пароля (например, с BCrypt)
        if (string.IsNullOrWhiteSpace(enteredPassword) == string.IsNullOrWhiteSpace(storedHash))
            return true;
        else return false;
    }
   
    private string GenerateJwtToken(User user)
    {
        // 1. Создаём ключ для подписи токена
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
        );
    
        // 2. Создаём учётные данные для подписи
        var credentials = new SigningCredentials(
            securityKey, 
            SecurityAlgorithms.HmacSha256
        );
    
        // 3. Создаём claims (утверждения) о пользователе
        var claims = new[]
        {
            // Идентификатор пользователя
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        
            // Уникальное имя (логин)
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
        };
    
        // 4. Создаём сам токен
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],          // Кто выдал токен
            audience: _configuration["Jwt:Audience"],      // Для кого предназначен
            claims: claims,                         // Утверждения о пользователе
            expires: DateTime.Now.AddHours(1),       // Срок действия
            signingCredentials: credentials         // Ключ подписи
        );
    
        // 5. Преобразуем токен в строку
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        // 1. Проверяем, что пользователь с таким email еще не существует
        var existingUser = await _bookContext.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);
    
        if (existingUser != null)
        {
            return BadRequest("User with this email already exists");
        }
    
        // 2. Хешируем пароль (в реальном приложении используйте BCrypt или аналоги)
        var passwordHash = _userRepository.HashPassword(request.Password);
           
    
        // 3. Создаем нового пользователя
        var newUser = new User
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            // Добавьте другие поля по необходимости
        };
    
        // 4. Сохраняем пользователя в БД
        _bookContext.Users.Add(newUser);
        await _bookContext.SaveChangesAsync();
    
        // 5. Генерируем токен для автоматического входа после регистрации
        var token = GenerateJwtToken(newUser);
    
        // 6. Возвращаем токен
        return Ok(new { Token = token });
    }
}