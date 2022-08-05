using Bjss.PriceBasket.Goods;
using Bjss.PriceBasket.Offers.Common;

namespace Bjss.PriceBasket;

public class Basket
{
    private readonly ICatalog _catalog;
    private readonly IBasketOutputWriter _outputWriter;
    public List<string> InvalidGoods { get; }
    public IDictionary<string, int> Goods { get; }
    public IList<IOffer> Offers { get; }

    public Basket(ICatalog catalog, IBasketOutputWriter outputWriter)
    {
        _catalog = catalog;
        _outputWriter = outputWriter;
        Goods = new Dictionary<string, int>();
        Offers = new List<IOffer>();
        InvalidGoods = new List<string>();
    }

    public void FillBasket(IEnumerable<string> goods)
    {
        foreach (var group in goods.GroupBy(x => x))
        {
            if (!Good.Exists(group.Key))
            {
                InvalidGoods.Add(group.Key);
                continue;
            }

            Goods.Add(group.Key, group.Count());
        }

        foreach (var (key, value) in Goods)
        {
            var offer = _catalog.GetOffer(key, value);
            if (offer != null && offer.HasDiscount())
            {
                Offers.Add(offer);
            }
        }
    }

    internal decimal CalculateDiscounts()
    {
        return Offers.Sum(x => x.CalculateOffer());
    }

    internal decimal CalculateSubtotal()
    {
        return Goods.Select(x => _catalog.GetPrice(x.Key) * x.Value).Sum();
    }

    internal decimal CalculateTotal()
    {
        return CalculateSubtotal() - CalculateDiscounts();
    }

    public string Print()
    {
        return _outputWriter.Print(this);
    }
}
