using TicketToCode.Api.Services;
using static TicketToCode.Api.Endpoints.Auth.Dtos;

namespace TicketToCode.Api.Endpoints.Auth;

public class Register : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("/auth/register", Handle)
        .WithSummary("Register a new user")
        .AllowAnonymous();

    private static Results<Ok<AuthResponse>, BadRequest<string>> Handle(
        RegisterRequest request,
        IAuthService authService)
    {
        var response = authService.Register(request);
        if (response == null)
        {
            return TypedResults.BadRequest("Username already exists");
        }

        return TypedResults.Ok(response);
    }
} 