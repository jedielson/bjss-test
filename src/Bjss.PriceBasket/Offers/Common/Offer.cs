namespace Bjss.PriceBasket.Offers.Common;

public abstract class Offer : IOffer
{
    protected const string ErrorShouldBeGreaterThanZero = "Should be greather than 0";
    public int Quantity { get; }
    public decimal Price { get; }

    protected Offer(int quantity, decimal price)
    {
        if (quantity < 1)
        {
            throw new ArgumentException(ErrorShouldBeGreaterThanZero, nameof(quantity));
        }

        if (price <= decimal.Zero)
        {
            throw new ArgumentException(ErrorShouldBeGreaterThanZero, nameof(price));
        }

        Quantity = quantity;
        Price = price;
    }

    public abstract decimal CalculateOffer();
    public abstract string PrintOffer();

    public abstract bool HasDiscount();
}
