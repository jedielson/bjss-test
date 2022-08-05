using Bjss.PriceBasket.Offers.Common;

namespace Bjss.PriceBasket.Offers;

public class SoupOffer : Offer
{
    private readonly decimal _priceOfBread;

    public SoupOffer(int quantity, decimal price, decimal priceOfBread) : base(quantity, price)
    {
        if (priceOfBread < 1)
        {
            throw new ArgumentException(ErrorShouldBeGreaterThanZero, nameof(priceOfBread));
        }
        _priceOfBread = priceOfBread;
    }

    public override decimal CalculateOffer()
    {
        var quantity = Quantity / 2;
        return _priceOfBread * quantity / 2;
    }

    public override string PrintOffer()
    {
        return !HasDiscount()
            ? string.Empty
            : $"Bread: $ {_priceOfBread / 2:0.00}{Environment.NewLine}";
    }

    public override bool HasDiscount()
    {
        return Quantity > 1;
    }
}
