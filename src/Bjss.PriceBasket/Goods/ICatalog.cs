using Bjss.PriceBasket.Offers.Common;

namespace Bjss.PriceBasket.Goods;

public interface ICatalog
{
    decimal GetPrice(string good);
    IOffer? GetOffer(string good, int quantity);

}
