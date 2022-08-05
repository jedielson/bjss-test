using Bjss.PriceBasket.Goods;
using Bjss.PriceBasket.Offers.Common;
using FluentAssertions;
using Moq;

namespace Bjss.PriceBasket.Tests;

public class BasketOutputWritterTests
{
    [Fact]
    public void Basket_Should_Print_Zero_Correctly()
    {
        // Arrange
        var writter = new BasketOutputWriter();

        // Act
        var result = writter.PrintTotal(decimal.Zero);

        // Assert
        result.Should().Be($"Total: $ 0.00{Environment.NewLine}");
    }

    [Fact]
    public void Basket_Should_Print_Total_Correctly()
    {
        // Arrange
        var writter = new BasketOutputWriter();

        // Act
        var result = writter.PrintTotal(11.3m);

        // Assert
        result.Should().Be($"Total: $ 11.30{Environment.NewLine}");
    }

    [Fact]
    public void Basket_Should_Print_No_Offers_Correctly()
    {
        // Arrange
        var writter = new BasketOutputWriter();

        // Act
        var result = writter.PrintOffers(Array.Empty<IOffer>());

        // Assert
        result.Should().Be($"(no offers available){Environment.NewLine}");
    }

    [Fact]
    public void Basket_Should_Print_Offers_Correctly()
    {
        // Arrange

        var appleOutput = $"Apples 10% off: $ 0.10{Environment.NewLine}";
        var soupOutput = $"Apples one per half price: $ 0.35{Environment.NewLine}";
        var appleOffer = new Mock<IOffer>();
        var soupOffer = new Mock<IOffer>();

        appleOffer.Setup(x => x.PrintOffer()).Returns(appleOutput);
        soupOffer.Setup(x => x.PrintOffer()).Returns(soupOutput);

        var writter = new BasketOutputWriter();

        // Act
        var result = writter.PrintOffers(new []{appleOffer.Object, soupOffer.Object});

        // Assert
        result.Should().Be(appleOutput + soupOutput);
    }

    [Fact]
    public void Basket_Should_Print_Subtotal_If_No_Goods()
    {
        // arrange
        var catalog = new Mock<ICatalog>();
        catalog.Setup(x => x.GetPrice(It.IsAny<string>())).Returns(BasketTestsHelper.GetCatalogPrices);

        var writter = new BasketOutputWriter();

        // act
        var result = writter.PrintSubtotal(0);

        // assert
        result.Should().Be($"Subtotal: $ 0.00{Environment.NewLine}");
    }

    [Theory]
    [InlineData(Good.Apples, Good.Milk, Good.Bread)]
    [InlineData(Good.Apples, Good.Milk)]
    [InlineData(Good.Apples, Good.Bread)]
    [InlineData(Good.Apples, Good.Soup)]
    [InlineData(Good.Apples, Good.Soup, Good.Soup)]
    public void Basket_Should_Print_Subtotal_Correctly(params string[] goods)
    {
        // arrange
        var catalog = new Mock<ICatalog>();
        var outputWritter = new Mock<IBasketOutputWriter>();
        var basket = new Basket(catalog.Object, outputWritter.Object);
        basket.FillBasket(goods);

        catalog.Setup(x => x.GetPrice(It.IsAny<string>())).Returns(BasketTestsHelper.GetCatalogPrices);
        var subtotal = basket.CalculateSubtotal();
        var writter = new BasketOutputWriter();


        // act
        var result = writter.PrintSubtotal(subtotal);

        // assert
        var expected = $"Subtotal: $ {subtotal}{Environment.NewLine}";
        result.Should().Be(expected);
    }
}
