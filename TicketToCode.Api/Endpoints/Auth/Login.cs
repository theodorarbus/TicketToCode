using TicketToCode.Api.Services;
using static TicketToCode.Api.Endpoints.Auth.Dtos;

namespace TicketToCode.Api.Endpoints.Auth;

public class Login : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("/auth/login", Handle)
        .WithSummary("Login with username and password")
        .AllowAnonymous();

    private static Results<Ok<AuthResponse>, NotFound<string>> Handle(
        LoginRequest request,
        IAuthService authService,
        HttpContext context)
    {
        var response = authService.Login(request);
        if (response == null)
        {
            return TypedResults.NotFound("Invalid username or password");
        }

        // Set a simple auth cookie
        context.Response.Cookies.Append("auth", $"{response.Username}:{response.Role}", new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });

        return TypedResults.Ok(response);
    }
} 