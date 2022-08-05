using Bjss.PriceBasket.Goods;
using FluentAssertions;
using Moq;

namespace Bjss.PriceBasket.Tests;

public class BasketTests
{
    private const string Apples = "Apples";
    private const string Soup = "Soup";
    private const string Bread = "Bread";
    private const string Milk = "Milk";

    [Fact]
    public void Basket_Should_Print_Correctly()
    {
        // arrange
        var basket = new Basket(new Catalog(), new BasketOutputWriter());
        basket.FillBasket(new []{Apples, Milk, Bread});
        // act
        var output = basket.Print();
        // assert
        var expected = $"Subtotal: $ 3.10{Environment.NewLine}";
        expected += $"Apples 10% off: $ 0.10{Environment.NewLine}";
        expected += $"Total: $ 3.00{Environment.NewLine}";
        output.Should().Be(expected);
    }

    [Fact]
    public void Basket_Should_Print_Correctly_No_Offers()
    {
        // arrange
        var basket = new Basket(new Catalog(), new BasketOutputWriter());
        basket.FillBasket(new []{Milk, Bread});
        // act
        var output = basket.Print();
        // assert
        var expected = $"Subtotal: $ 2.10{Environment.NewLine}";
        expected += $"(no offers available){Environment.NewLine}";
        expected += $"Total: $ 2.10{Environment.NewLine}";
        output.Should().Be(expected);
    }

    [Fact]
    public void Basket_Should_Print_Invalid_Goods()
    {
        // arrange
        var basket = new Basket(new Catalog(), new BasketOutputWriter());
        basket.FillBasket(new []{Apples, Milk, Bread, "Chocolat", "Donuts"});
        // act
        var output = basket.Print();
        // assert
        var expected = $"The following goods are not available:{Environment.NewLine}";
        expected += $"- Chocolat{Environment.NewLine}";
        expected += $"- Donuts{Environment.NewLine}";
        expected += $"Subtotal: $ 3.10{Environment.NewLine}";
        expected += $"Apples 10% off: $ 0.10{Environment.NewLine}";
        expected += $"Total: $ 3.00{Environment.NewLine}";
        output.Should().Be(expected);
    }

    [Theory]
    [InlineData(Apples, Milk, Bread)]
    [InlineData(Apples, Milk)]
    [InlineData(Apples, Bread)]
    [InlineData(Apples, Soup)]
    [InlineData(Apples, Soup, Soup)]
    public void Basket_Should_Calculate_Subtotal_Correctly(params string[] goods)
    {
        // arrange
        var catalog = new Mock<ICatalog>();
        var outputWritter = new Mock<IBasketOutputWriter>();
        var basket = new Basket(catalog.Object, outputWritter.Object);
        basket.FillBasket(goods);
        catalog.Setup(x => x.GetPrice(It.IsAny<string>())).Returns(BasketTestsHelper.GetCatalogPrices);

        // act
        var result = basket.CalculateSubtotal();

        // assert
        var expected = goods.Sum(BasketTestsHelper.GetCatalogPrices);
        result.Should().Be(expected);
    }

    [Fact]
    public void Basket_Should_Calculate_Subtotal_If_No_Goods()
    {
        // arrange
        var catalog = new Mock<ICatalog>();
        var outputWritter = new Mock<IBasketOutputWriter>();
        var basket = new Basket(catalog.Object, outputWritter.Object);
        basket.FillBasket(Array.Empty<string>());
        catalog.Setup(x => x.GetPrice(It.IsAny<string>())).Returns(BasketTestsHelper.GetCatalogPrices);

        // act
        var result = basket.CalculateSubtotal();

        // assert
        result.Should().Be(0);
    }

    [Fact]
    public void Basket_Should_Calculate_Offers_Correctly()
    {
        // Arrange
        var appleOffer = BasketTestsHelper.SetupOfferMock(Apples);
        var soupOffer = BasketTestsHelper.SetupOfferMock(Soup);
        var catalog = new Mock<ICatalog>();
        var outputWritter = new Mock<IBasketOutputWriter>();
        var basket = new Basket(catalog.Object, outputWritter.Object);

        catalog.Setup(x => x.GetOffer(Apples, 1)).Returns(appleOffer?.Object);
        catalog.Setup(x => x.GetOffer(Soup, 1)).Returns(soupOffer?.Object);
        catalog.Setup(x => x.GetOffer(Bread, 1)).Returns(() => null);

        basket.FillBasket(new []{Apples, Soup, Bread});

        // Act
        var result = basket.CalculateDiscounts();

        // Assert
        result.Should().Be(BasketTestsHelper.GetDiscounts(Apples) + BasketTestsHelper.GetDiscounts(Soup));
    }

    [Fact]
    public void Basket_Should_Not_Have_Discounts()
    {
        // Arrange
        var catalog = new Mock<ICatalog>();
        var outputWritter = new Mock<IBasketOutputWriter>();
        var basket = new Basket(catalog.Object, outputWritter.Object);

        catalog.Setup(x => x.GetOffer(It.IsAny<string>(), It.IsAny<int>())).Returns(() => null);
        basket.FillBasket(new []{Apples, Soup, Bread});

        // Act
        var result = basket.CalculateDiscounts();

        // Assert
        result.Should().Be(decimal.Zero);
    }

    [Fact]
    public void Basket_Should_Calculate_Total_Correctly()
    {
        // Arrange
        var appleOffer = BasketTestsHelper.SetupOfferMock(Apples);
        var soupOffer =  BasketTestsHelper.SetupOfferMock(Soup);
        var catalog = new Mock<ICatalog>();
        var outputWritter = new Mock<IBasketOutputWriter>();
        var basket = new Basket(catalog.Object, outputWritter.Object);

        catalog.Setup(x => x.GetPrice(It.IsAny<string>())).Returns(BasketTestsHelper.GetCatalogPrices);
        catalog.Setup(x => x.GetOffer(Apples, 1)).Returns(appleOffer?.Object);
        catalog.Setup(x => x.GetOffer(Soup, 1)).Returns(soupOffer?.Object);
        catalog.Setup(x => x.GetOffer(Bread, 1)).Returns(() => null);

        basket.FillBasket(new []{Apples, Soup, Bread});

        // Act
        var result = basket.CalculateTotal();

        // Assert
        var subtotal = basket.CalculateSubtotal();
        var discounts = basket.CalculateDiscounts();
        result.Should().Be(subtotal - discounts);
    }
}
