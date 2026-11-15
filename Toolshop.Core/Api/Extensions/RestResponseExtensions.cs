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

    public static string Describe(this RestResponse response)
    {
        return
            $"response code: {response.StatusCode},  response message: {response.ErrorMessage},  content: {response.Content}";
    }
}