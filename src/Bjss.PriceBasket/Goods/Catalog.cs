using Bjss.PriceBasket.Offers;
using Bjss.PriceBasket.Offers.Common;

namespace Bjss.PriceBasket.Goods;

public class Catalog : ICatalog
{
    private readonly Dictionary<string, decimal> _pricings;

    public Catalog()
    {
        _pricings = new Dictionary<string, decimal>
        {
            {Good.Soup, 0.65m},
            {Good.Bread, 0.80m},
            {Good.Milk, 1.30m},
            {Good.Apples, 1.00m}
        };
    }

    public decimal GetPrice(string good)
    {
        return _pricings[good];
    }

    public IOffer? GetOffer(string good, int quantity)
    {
        return good switch
        {
            Good.Soup => new SoupOffer(quantity, GetPrice(good), GetPrice(Good.Bread)),
            Good.Apples => new ApplesOffer(quantity, GetPrice(good)),
            _ => null,
        };
    }
}
