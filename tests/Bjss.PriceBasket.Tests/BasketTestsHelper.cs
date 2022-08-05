using Bjss.PriceBasket.Goods;
using Bjss.PriceBasket.Offers.Common;
using Moq;

namespace Bjss.PriceBasket.Tests;

public static class BasketTestsHelper
{
    public static Mock<IOffer>? SetupOfferMock(string good)
    {
        var discount = GetDiscounts(good);
        if (discount == decimal.Zero)
        {
            return null;
        }

        var mock = new Mock<IOffer>();
        mock.Setup(x => x.HasDiscount()).Returns(discount > decimal.Zero);
        mock.Setup(x => x.CalculateOffer()).Returns(discount);
        return mock;
    }

    public static  decimal GetCatalogPrices(string good)
    {
        return good switch
        {
            Good.Apples => 11m,
            Good.Milk => 1.30m,
            Good.Bread => 0.80m,
            Good.Soup => 0.65m,
            _ => decimal.Zero
        };
    }

    public static  decimal GetDiscounts(string good)
    {
        return good switch
        {
            Good.Apples => 0.20m,
            Good.Soup => 0.35m,
            _ => decimal.Zero
        };
    }
}
