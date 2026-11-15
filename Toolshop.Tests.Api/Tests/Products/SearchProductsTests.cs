using System.Net;

namespace Toolshop.Tests.Api.Tests.Products;

public class SearchProductsTests : ApiTestBase
{
    [TestCase("Pliers")]
    [TestCase("hammer")]
    public async Task SearchProducts_ByName_ReturnsMatchingProducts(string name)
    {
        // Act
        var response = await Api.Products.SearchProductsAsync(name);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"...");
        Assert.That(response.Data, Is.Not.Null, response.ErrorMessage);

        var products = response.Data!.Data;
        Assert.That(products, Is.Not.Empty);
        Assert.That(products.Select(p => p.Name), Has.All.Contains(name).IgnoreCase);
    }

    [Test]
    public async Task SearchProducts_ByNonExistentName_ReturnsEmptyList()
    {
        //Arrange
        var response = await Api.Products.SearchProductsAsync("invalid_name");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            $"response code: {response.StatusCode},  response message: {response.ErrorMessage},  content: {response.Content}");
        Assert.That(response.Data, Is.Not.Null, response.ErrorMessage);

        //Act
        var productsList = response.Data.Data!;

        //Assert
        Assert.That(productsList, Is.Empty);
    }
}