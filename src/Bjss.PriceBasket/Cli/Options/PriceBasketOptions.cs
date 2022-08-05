using CommandLine;

namespace Bjss.PriceBasket.Cli.Options;

[Verb("PriceBasket", HelpText = "A PriceBasket helper")]
public class PriceBasketOptions
{
    [Value(0)]
    public IEnumerable<string> Goods { get; set; } = Array.Empty<string>();
}
