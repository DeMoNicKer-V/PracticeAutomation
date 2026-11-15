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
        foreach (var product in productsList) Assert.That(product.Name, Does.Contain(name).IgnoreCase);
    }

    [Test]
    public async Task SerchProdocts_ByNonExistentName_ReturnsEmptyList()
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
*/