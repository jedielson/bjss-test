using Bjss.PriceBasket.Offers.Common;

namespace Bjss.PriceBasket;

internal class BasketOutputWriter : IBasketOutputWriter
{
    public string Print(Basket basket)
    {
        var expected = PrintInvalidGoods(basket.InvalidGoods);
        expected += PrintSubtotal(basket.CalculateSubtotal());
        expected += PrintOffers(basket.Offers);
        expected += PrintTotal(basket.CalculateTotal());
        return expected;
    }

    public string PrintInvalidGoods(IEnumerable<string> goods)
    {
        goods = goods.ToArray();
        if (!goods.Any())
        {
            return string.Empty;
        }

        var output = $"The following goods are not available:{Environment.NewLine}";
        return goods.Aggregate(output, (s, good) => s + $"- {good}{Environment.NewLine}");
    }

    public string PrintSubtotal(decimal subtotal)
    {
        return $"Subtotal: $ {subtotal:0.00}{Environment.NewLine}";
    }

    public string PrintTotal(decimal total)
    {
        return $"Total: $ {total:0.00}{Environment.NewLine}";
    }

    public string PrintOffers(IEnumerable<IOffer> offers)
    {
        offers = offers.ToArray();
        return !offers.Any()
            ? $"(no offers available){Environment.NewLine}"
            : offers.Aggregate(string.Empty, (s, offer) => s + offer.PrintOffer());
    }
}
