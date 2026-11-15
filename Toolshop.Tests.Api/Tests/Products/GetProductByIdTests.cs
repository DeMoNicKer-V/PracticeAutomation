using System.Net;
using Toolshop.Core.Api.Constants;
using Toolshop.Core.Api.Extensions;

namespace Toolshop.Tests.Api.Tests.Products;

public class GetProductByIdTests : ApiTestBase
{
    [Test]
    public async Task GetProduct_FromListById_ReturnsProduct()
    {
        //Arrange
        var response = await Api.Products.GetProductsAsync();

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Describe());
        Assert.That(response.Data, Is.Not.Null);

        var productsList = response.Data!;
        Assert.That(productsList.Data, Is.Not.Empty);
        var firstProduct = productsList.Data[0];

        //Act
        var productByIdResponse = await Api.Products.GetProductByIdAsync(firstProduct.Id);

        //Assert
        Assert.That(productByIdResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            productByIdResponse.Describe());
        Assert.That(productByIdResponse.Data, Is.Not.Null);

        var productById = productByIdResponse.Data!;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(productById.Id, Is.EqualTo(firstProduct.Id));
            Assert.That(productById.Name, Is.EqualTo(firstProduct.Name));
            Assert.That(productById.Price, Is.EqualTo(firstProduct.Price));
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
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), response.Describe());
        Assert.That(response.GetError()?.Message, Is.EqualTo(ErrorsText.ItemNotFound));
    }
}