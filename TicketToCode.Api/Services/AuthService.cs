using TicketToCode.Core.Data;
using TicketToCode.Core.Models;
using static TicketToCode.Api.Endpoints.Auth.Dtos;

namespace TicketToCode.Api.Services;

public interface IAuthService
{
    AuthResponse? Login(LoginRequest request);
    AuthResponse? Register(RegisterRequest request);
}

public class AuthService : IAuthService
{
    private readonly IDatabase _database;

    public AuthService(IDatabase database)
    {
        _database = database;
    }

    public AuthResponse? Login(LoginRequest request)
    {
        var user = _database.Users.FirstOrDefault(u => u.Username == request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return null;
        }

        return new AuthResponse(user.Username, user.Role);
    }

    public AuthResponse? Register(RegisterRequest request)
    {
        if (_database.Users.Any(u => u.Username == request.Username))
        {
            return null;
        }

        var user = new User(request.Username, BCrypt.Net.BCrypt.HashPassword(request.Password));

        _database.Users.Add(user);
        return new AuthResponse(user.Username, user.Role);
    }
} 