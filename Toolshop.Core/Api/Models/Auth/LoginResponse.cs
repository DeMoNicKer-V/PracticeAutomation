using System.Text.Json.Serialization;

namespace Toolshop.Core.Api.Models.Auth;

public record LoginResponse
{
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("token_type")]
    public required string TokenType { get; init; }

    [JsonPropertyName("expires_in")]
    public required int TokenExpiresIn { get; init; }
}