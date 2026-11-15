using System.Net;
using Toolshop.Core.Api.Extensions;

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
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Describe());
        Assert.That(response.Data, Is.Not.Null, response.ErrorMessage);

        var products = response.Data!.Data;
        Assert.That(products, Is.Not.Empty);
        Assert.That(products.Select(p => p.Name), Has.All.Contains(name).IgnoreCase);
    }

    [Test]
    public async Task SearchProducts_ByNonExistentName_ReturnsEmptyList()
    {
        //Arrange 
        var random = TestContext.CurrentContext.Random;
        var nonExistentName = random.GetString(15);

        //Act
        var response = await Api.Products.SearchProductsAsync(nonExistentName);

        //Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Describe());
        Assert.That(response.Data, Is.Not.Null, response.ErrorMessage);

        var productsList = response.Data!.Data;
        Assert.That(productsList, Is.Empty);
    }
}