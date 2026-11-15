using System.Net;
using Toolshop.Core.Api.Extensions;

namespace Toolshop.Tests.Api.Tests.Products;

public class GetProductsTests : ApiTestBase
{
    [Test]
    public async Task GetProducts_FirstPage_ReturnsNonEmptyPage()
    {
        // Act
        var response = await Api.Products.GetProductsAsync();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Describe());
        Assert.That(response.Data, Is.Not.Null);
        var page = response.Data!;
        Assert.That(page.Data, Is.Not.Empty);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(page.CurrentPage, Is.EqualTo(1));
            Assert.That(page.Data, Is.Not.Empty);
            Assert.That(page.Data, Has.Count.LessThanOrEqualTo(page.PerPage));
        }
    }

    [Test]
    public async Task GetProducts_LastPagePlusOne_ReturnsEmptyPage()
    {
        //Arrange
        var firstResponse = await Api.Products.GetProductsAsync();
        Assert.That(firstResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), firstResponse.Describe());
        Assert.That(firstResponse.Data, Is.Not.Null, firstResponse.ErrorMessage);
        var lastPage = firstResponse.Data!.LastPage;

        //Act
        var secondResponse = await Api.Products.GetProductsAsync(lastPage + 1);

        //Assert
        Assert.That(secondResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), secondResponse.Describe());
        Assert.That(secondResponse.Data, Is.Not.Null, secondResponse.ErrorMessage);
        Assert.That(secondResponse.Data!.Data, Is.Empty);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public async Task GetProducts_InvalidPageNumber_ReturnsFirstPage(int page)
    {
        //Act
        var response = await Api.Products.GetProductsAsync(page);

        //Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Describe());
        Assert.That(response.Data, Is.Not.Null, response.ErrorMessage);
        Assert.That(response.Data!.CurrentPage, Is.EqualTo(1));
    }
}