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
            $"{response.ErrorException}/n{response.Content}");
        Assert.That(response.Data, Is.Not.Null, response.ErrorMessage);
        Assert.That(response.Data.Data, Is.Empty);
    }

    [TestCase(0)]
    [TestCase(1)]
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