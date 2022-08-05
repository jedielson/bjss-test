namespace Bjss.PriceBasket.Offers.Common;

public interface IOffer
{
    decimal CalculateOffer();
    string PrintOffer();
    bool HasDiscount();
}
