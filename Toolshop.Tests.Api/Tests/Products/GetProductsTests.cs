using System.Net;

namespace Toolshop.Tests.Api.Tests;

public class GetProductsTests : ApiTestBase
{
    [Test]
    public async Task GetProducts_FirstPage_ReturnsNonEmptyPage()
    {
        // Act
        var response = await Api.Products.GetProductsAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            $"{response.ErrorMessage}\n{response.Content}");
        Assert.That(response.Data, Is.Not.Null, response.ErrorMessage);

        var page = response.Data!;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(page.CurrentPage, Is.EqualTo(1));
            Assert.That(page.Data, Is.Not.Empty);
            Assert.That(page.Data, Has.Count.LessThanOrEqualTo(page.PerPage));
        }
    }

    [Test]
    public async Task GetProducts_999Page_ReturnsEmptyPage()
    {
        //Act
        var response = await Api.Products.GetProductsAsync(999);

        //Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            $"response code: {response.StatusCode},  response message: {response.ErrorMessage},  content: {response.Content}");

        Assert.That(response.Data, Is.Not.Null, response.ErrorMessage);
        Assert.That(response.Data.Data, Is.Empty);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public async Task GetProducts_InvalidPageNumber_ReturnsFirstPage(int page)
    {
        //Act
        var response = await Api.Products.GetProductsAsync(page);

        //Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            $"{response.ErrorException}/n{response.Content}");
        Assert.That(response.Data, Is.Not.Null, response.ErrorMessage);
        Assert.That(response.Data.CurrentPage, Is.EqualTo(1));
    }
}