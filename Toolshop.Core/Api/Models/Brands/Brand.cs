using System.Text.Json.Serialization;

namespace Toolshop.Core.Api.Models.Brands;

public record Brand
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}