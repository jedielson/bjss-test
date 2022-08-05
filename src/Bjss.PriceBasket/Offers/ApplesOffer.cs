using Bjss.PriceBasket.Offers.Common;

namespace Bjss.PriceBasket.Offers;

public class ApplesOffer : Offer
{
    public ApplesOffer(int quantity, decimal price) : base(quantity, price)
    {
    }

    internal decimal CalculateBaseDiscount()
    {
        return Price * 0.1m;
    }

    public override decimal CalculateOffer()
    {
        return Quantity * CalculateBaseDiscount();
    }

    public override string PrintOffer()
    {
        var output = $"Apples 10% off: $ {CalculateBaseDiscount():0.00}{Environment.NewLine}";
        return Enumerable
            .Range(1, Quantity)
            .Aggregate(string.Empty, (s, _) => s + output);
    }

    public override bool HasDiscount()
    {
        return Quantity >= 1;
    }
}
