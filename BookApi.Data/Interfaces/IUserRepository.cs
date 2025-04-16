using BookApi.Data.Models;

namespace BookApi.Data.Interfaces;

public interface IUserRepository
{
    public string HashPassword(string password);

    public bool VerifyPassword(string enteredPassword, string storedHash);
    public string GenerateJwtToken(User user);
}