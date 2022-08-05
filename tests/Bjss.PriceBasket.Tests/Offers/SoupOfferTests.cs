using Bjss.PriceBasket.Offers;
using FluentAssertions;

namespace Bjss.PriceBasket.Tests.Offers;

public class SoupOfferTests
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 0.5)]
    [InlineData(3, 0.5)]
    [InlineData(4, 1)]
    [InlineData(5, 1)]
    [InlineData(6, 1.5)]
    public void Offer_Should_Calculate_Offer_Correctly(int quantity, decimal expected)
    {
        // Arrange
        var offer = new SoupOffer(quantity, 1m, 1m);

        // Act
        var result = offer.CalculateOffer();

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(4, true)]
    public void Offer_Should_Validate_Discount_Correctly(int quantity, bool expected)
    {
        // Arrange
        var offer = new SoupOffer(quantity, 1m, 1m);

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
        var ctor = () => new SoupOffer(quantity, 1m, 1m);
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
        var ctor = () => new SoupOffer(1, price, 1m);
        // Act
        // Assert
        ctor.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Offer_Should_Validate_Bread_Price(decimal price)
    {
        // Arrange
        var ctor = () => new SoupOffer(1, 1m, price);
        // Act
        // Assert
        ctor.Should()
            .Throw<ArgumentException>();
    }

    [Fact]
    public void Offer_Should_Print_Discount_Correctly()
    {
        // Arrange
        var offer = new SoupOffer(2, 1m, 1m);

        // Act
        var result = offer.PrintOffer();

        // Assert
        var expected = $"Bread: $ 0.50{Environment.NewLine}";
        result.Should().Be(expected);
    }

    [Fact]
    public void Offer_Should_Print_No_Discount_Correctly()
    {
        // Arrange
        var offer = new SoupOffer(1, 1m, 1m);

        // Act
        var result = offer.PrintOffer();

        // Assert
        result.Should().Be(string.Empty);
    }
}
