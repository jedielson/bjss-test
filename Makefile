test:
	cd $$(pwd)/tests/Bjss.PriceBasket.Tests && \
	dotnet dotnet-stryker

run:
	cd $$(pwd)/src/Bjss.PriceBasket && \
	dotnet run $(GOODS)
