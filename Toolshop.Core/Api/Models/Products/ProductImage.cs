using System.Text.Json.Serialization;

namespace Toolshop.Core.Api.Models.Products;

public record ProductImage
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("by_name")]
    public required string ByName { get; init; }
}