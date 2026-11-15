using System.Text.Json;
using RestSharp;
using Toolshop.Core.Api.Models.Common;

namespace Toolshop.Core.Api.Extensions;

public static class RestResponseExtensions
{
    public static ApiError? GetError(this RestResponse response)
    {
        return string.IsNullOrWhiteSpace(response.Content)
            ? null
            : JsonSerializer.Deserialize<ApiError>(response.Content);
    }
}