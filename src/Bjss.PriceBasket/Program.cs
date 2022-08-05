// See https://aka.ms/new-console-template for more information

using Bjss.PriceBasket.Cli.Actions;
using Bjss.PriceBasket.Cli.Options;
using CommandLine;

CommandLine.Parser.Default.ParseArguments<PriceBasketOptions, UnknownVerb>(args)
    .MapResult(
        (PriceBasketOptions options) => new PriceBasketAction().Execute(options),
        _ => -1);
