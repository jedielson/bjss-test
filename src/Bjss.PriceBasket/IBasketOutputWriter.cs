using Bjss.PriceBasket.Offers.Common;

namespace Bjss.PriceBasket;

public interface IBasketOutputWriter
{
    string Print(Basket basket);
    string PrintInvalidGoods(IEnumerable<string> goods);
    string PrintSubtotal(decimal subtotal);
    string PrintTotal(decimal total);
    string PrintOffers(IEnumerable<IOffer> offers);
}
