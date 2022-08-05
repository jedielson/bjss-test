# Welcome to PriceBasket

## How to run Unit tests

Just run `make test`

The tests uses [dotnet-stryker](https://stryker-mutator.io/docs/stryker-net/introduction/) to run and perform mutation tests against codebase.

After running the tests, you can open the html report that shows in the output.


## How to run

### Mode 1: Make

`make GOODS="PriceBasket Apples Milk Bread" run`

### Mode 2: Manually

Goto `/src/Bjss.Basket` and run `dotnet run PriceBasket $ARGS`

e.g `dotnet run PriceBasket Apples Milk Bread`
