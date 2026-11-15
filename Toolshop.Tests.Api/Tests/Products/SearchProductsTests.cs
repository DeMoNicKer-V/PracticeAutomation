using System.Net;

namespace Toolshop.Tests.Api.Tests;

public class SearchProductsTests : ApiTestBase
{
    [TestCase("Pliers")]
    [TestCase("hammer")]
    public async Task SearchProducts_ByName_ReturnsProduct(string name)
    {
        //Arrange
        var response = await Api.Products.SearchProductsAsync(name);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            $"response code: {response.StatusCode},  response message: {response.ErrorMessage},  content: {response.Content}");
        Assert.That(response.Data, Is.Not.Null, response.ErrorMessage);

        //Act
        var productsList = response.Data.Data!;
        Assert.That(productsList, Is.Not.Empty);

        //Assert

        //NotOK
        /*foreach (var product in productsList) Assert.That(product.Name, Does.Contain(name).IgnoreCase);*/

        //OK
        Assert.That(productsList.Select(p => p.Name), Has.All.Contains(name).IgnoreCase);
    }

    [Test]
    public async Task SerchProducts_ByNonExistentName_ReturnsEmptyList()
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