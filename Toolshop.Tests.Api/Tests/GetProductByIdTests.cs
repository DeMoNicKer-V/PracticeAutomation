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
        //Act
        var firstProduct = productsList.Data[0];

        var productByIdResponse = await Api.Products.GetProductByIdAsync(firstProduct.Id);
        Assert.That(productByIdResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
            $"response code: {productByIdResponse.StatusCode},  response message: {productByIdResponse.ErrorMessage},  content: {productByIdResponse.Content}");
        Assert.That(productByIdResponse.Data, Is.Not.Null, response.ErrorMessage);

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
        var response = await Api.Products.GetProductByIdAsync("invalid_id");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound),
            $"response code: {response.StatusCode},  response message: {response.ErrorMessage},  content: {response.Content}");
        Assert.That(response.GetError()?.Message, Is.EqualTo(ErrorsText.ItemNotFound));
    }
}

/*
 * Тесты (шаг 4):

Сценарий	Ожидание
Первая страница списка	200; список не пустой; товаров ≤ PerPage
Товар по id, взятому из списка	200; имя и цена совпадают
Несуществующий id	404; понятное сообщение в теле
Поиск Pliers и hammer ([TestCase])	200; каждое имя содержит запрос без учёта регистра
Поиск несуществующего слова	200; пустой список
Дополнительно: page=0 и page=-1	характеризационный тест: фактически возвращается первая страница
Дополнительно: page=999	посмотрите руками, что приходит, и зафиксируйте

Правила для тестов:

без захардкоженных id;
структура Arrange → Act → Assert;
имена в формате Метод_Условие_Ожидание;
статус проверяется первым, с ErrorMessage и Content в сообщении;
несколько проверок одного ответа — через Assert.EnterMultipleScope().

Бонус: AuthClient + тесты на успешный логин и на 401. Негативный кейс — только со случайным e-mail.
*/