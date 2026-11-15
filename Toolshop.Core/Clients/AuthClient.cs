using RestSharp;
using Toolshop.Core.Api.Models.Auth;

namespace Toolshop.Core.Clients;

public class AuthClient
{
    private readonly RestClient _client;

    public AuthClient(RestClient client)
    {
        _client = client;
    }

    public Task<RestResponse<LoginResponse>> LoginAsync(string email, string password, CancellationToken ct = default)
    {
        var request = new RestRequest("users/login", Method.Post)
            .AddJsonBody(new LoginRequest(email, password));

        return _client.ExecuteAsync<LoginResponse>(request, ct);
    }
}