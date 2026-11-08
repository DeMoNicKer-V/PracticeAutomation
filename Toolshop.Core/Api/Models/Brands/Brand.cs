using System.Text.Json.Serialization;

namespace Toolshop.Core.Api.Models.Brands;

public record Brand
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}