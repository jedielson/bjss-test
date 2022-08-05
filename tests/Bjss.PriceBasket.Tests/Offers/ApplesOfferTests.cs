using Bjss.PriceBasket.Offers;
using FluentAssertions;

namespace Bjss.PriceBasket.Tests.Offers;

public class ApplesOfferTests
{
    [Theory]
    [InlineData(1, 0.1)]
    [InlineData(2, 0.2)]
    [InlineData(10, 1)]
    [InlineData(137, 13.7)]
    [InlineData(10.25, 1.025)]
    public void Offer_Should_Calculate_Base_Discount_Correctly(decimal value, decimal expected)
    {
        // Arrange
        var offer = new ApplesOffer(1, value);

        // Act
        var result = offer.CalculateBaseDiscount();

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(1, 1, 0.1)]
    [InlineData(1, 2, 0.2)]
    [InlineData(11, 1, 1.1)]
    [InlineData(11, 3, 3.3)]
    public void Offer_Should_Calculate_Offer_Correctly(decimal price, int quantity, decimal expected)
    {
        // Arrange
        var offer = new ApplesOffer(quantity, price);

        // Act
        var result = offer.CalculateOffer();

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(2, true)]
    public void Offer_Should_Validate_Discount_Correctly(int quantity, bool expected)
    {
        // Arrange
        var offer = new ApplesOffer(quantity, 1m);

        // Act
        var result = offer.HasDiscount();

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Offer_Should_Validate_Quantity(int quantity)
    {
        // Arrange
        var ctor = () => new ApplesOffer(quantity, 1m);
        // Act
        // Assert
        ctor.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Offer_Should_Validate_Price(decimal price)
    {
        // Arrange
        var ctor = () => new ApplesOffer(1, price);
        // Act
        // Assert
        ctor.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Offer_Should_Print_One_Apple_Correctly()
    {
        // Arrange
        var offer = new ApplesOffer(1, 1);

        // Act
        var result = offer.PrintOffer();

        // Assert
        var expected = $"Apples 10% off: $ 0.10{Environment.NewLine}";
        result.Should().Be(expected);
    }

    [Fact]
    public void Offer_Should_Print_Many_Apple_Correctly()
    {
        // Arrange
        var offer = new ApplesOffer(3, 1);

        // Act
        var result = offer.PrintOffer();

        // Assert
        var expected = $"Apples 10% off: $ 0.10{Environment.NewLine}";
        expected += $"Apples 10% off: $ 0.10{Environment.NewLine}";
        expected += $"Apples 10% off: $ 0.10{Environment.NewLine}";
        result.Should().Be(expected);
    }
}
