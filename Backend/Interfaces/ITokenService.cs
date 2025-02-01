using Backend.Models;

namespace Backend.Interfaces;

public interface ITokenService
{
    public string GenerateToken(User user);
}