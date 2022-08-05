using Bjss.PriceBasket.Cli.Options;
using Bjss.PriceBasket.Goods;

namespace Bjss.PriceBasket.Cli.Actions;

public class PriceBasketAction
{
    internal int Execute(PriceBasketOptions options)
    {
        var basket = new Basket(new Catalog(), new BasketOutputWriter());
        basket.FillBasket(options.Goods.Except(new[] {"PriceBasket"}));
        Console.WriteLine(basket.Print());
        return 0;
    }
}
