namespace TicketToCode.Api.Endpoints.Auth;

public static class Dtos
{
    public record LoginRequest(string Username, string Password);
    public record RegisterRequest(string Username, string Password);
    public record AuthResponse(string Username, string Role);
}
