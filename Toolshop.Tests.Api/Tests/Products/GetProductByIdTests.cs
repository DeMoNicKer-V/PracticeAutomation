using System.Net;
using Toolshop.Core.Api.Constants;
using Toolshop.Core.Api.Extensions;

namespace Toolshop.Tests.Api.Tests;

public class GetProductByIdTests : ApiTestBase
{
    [Test]
    public async Task GetProduct_FromListById_ReturnsProduct()
    {
        //Arrange
        var response = await Api.Products.GetProductsAsync();

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            $"response code: {response.StatusCode},  response message: {response.ErrorMessage},  content: {response.Content}");
        Assert.That(response.Data, Is.Not.Null, response.ErrorMessage);
        var productsList = response.Data!;
        Assert.That(productsList.Data, Is.Not.Empty);
        var firstProduct = productsList.Data[0];

        //Act
        var productByIdResponse = await Api.Products.GetProductByIdAsync(firstProduct.Id);
        Assert.That(productByIdResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            $"response code: {productByIdResponse.StatusCode},  response message: {productByIdResponse.ErrorMessage},  content: {productByIdResponse.Content}");
        Assert.That(productByIdResponse.Data, Is.Not.Null, productByIdResponse.ErrorMessage);

        var productById = productByIdResponse.Data!;
        //Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(productById.Id, Is.EqualTo(firstProduct.Id), "Product's ids are equal");
            Assert.That(productById.Name, Is.EqualTo(firstProduct.Name), "Product's names are not equal");
            Assert.That(productById.Price, Is.EqualTo(firstProduct.Price), "Product's prices are not equal");
        }
    }

    [Test]
    public async Task GetProduct_NonExistentId_Returns404Error()
    {
        //Arrange
        var random = TestContext.CurrentContext.Random;
        var nonExistentId = random.GetString(26);

        //Act
        var response = await Api.Products.GetProductByIdAsync(nonExistentId);

        //Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound),
            $"response code: {response.StatusCode},  response message: {response.ErrorMessage},  content: {response.Content}");
        Assert.That(response.GetError()?.Message, Is.EqualTo(ErrorsText.ItemNotFound));
    }
}