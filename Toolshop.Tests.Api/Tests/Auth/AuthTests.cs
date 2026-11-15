using System.Net;
using Toolshop.Core.Config;

namespace Toolshop.Tests.Api.Tests.Auth;

public class AuthTests : ApiTestBase
{
    [Test]
    public async Task Login_WithValidCredentials_ReturnsAccessToken()
    {
        //Arrange
        var userEmail = TestConfig.Settings.Customer.Email;
        var userPassword = TestConfig.Settings.Customer.Password;

        //Act
        var response =
            await Api.Auth.LoginAsync(userEmail, userPassword);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            $"response code: {response.StatusCode}, error message: {response.ErrorMessage}, content: {response.Content}");
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data.AccessToken, Is.Not.Null);
    }

    [Test]
    public async Task Login_WithInvalidEmail_ReturnsUnauthorized()
    {
        // Arrange 
        var email = $"user-{Guid.NewGuid():N}@example.com";

        //Act
        var response =
            await Api.Auth.LoginAsync(email, TestConfig.Settings.Customer.Password);

        //Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized),
            $"response code: {response.StatusCode}, error message: {response.ErrorMessage}, content: {response.Content}");
    }
}