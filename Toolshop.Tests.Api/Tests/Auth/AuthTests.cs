using System.Net;
using Toolshop.Core.Api.Extensions;
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

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Describe());
        Assert.That(response.Data, Is.Not.Null);

        var token = response.Data!;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(token.AccessToken, Is.Not.Empty);
            Assert.That(token.TokenType, Is.EqualTo("bearer"));
            Assert.That(token.TokenExpiresIn, Is.GreaterThan(0));
        }
    }

    [Test]
    public async Task Login_UnknownEmail_Returns401()
    {
        // Arrange 
        var email = $"user-{Guid.NewGuid():N}@example.com";

        //Act
        var response =
            await Api.Auth.LoginAsync(email, TestConfig.Settings.Customer.Password);

        //Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized), response.Describe());
    }
}