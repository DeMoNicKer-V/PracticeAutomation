using System.Text.Json.Serialization;

namespace Toolshop.Core.Api.Models.Common;

public record ApiError
{
    [JsonPropertyName("message")]
    public string? Message { get; init; }
}