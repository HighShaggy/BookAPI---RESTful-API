using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookApi.Data.Interfaces;
using BookApi.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookApi.Data.Repositories;

public class UserRepository(IConfiguration _configuration) : IUserRepository
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string enteredPassword, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
    }
    
    public string GenerateJwtToken(User user)
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
}