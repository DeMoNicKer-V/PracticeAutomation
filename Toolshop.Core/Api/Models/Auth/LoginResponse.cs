namespace Toolshop.Core.Api.Models.Auth;

public record LoginResponse
{
    public required string AccessToken { get; init; }
    public required string TokenType { get; init; } = "Bearer";
    public required int TokenExpiresIn { get; init; } = 300;
}