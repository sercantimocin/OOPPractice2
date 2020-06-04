using CodingChallenge.Models.Assets;
using CodingChallenge.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace CodingChallenge.Models.Tests
{
    [TestClass()]
    public class AssetPortfolioTests
    {
        [TestMethod()]
        public void Should_price_is_3_and_shares_is_300_When_abc_stock_consolidate()
        {
            var assetPortfolio = new AssetPortfolio(null);
            assetPortfolio.Add(new Stock("ABC", 100, 2, Currency.USD));
            assetPortfolio.Add(new Stock("ABC", 200, 3.5, Currency.USD));

            var result = assetPortfolio.Consolidate();

            Assert.AreEqual(1, result.Portfolio.Count);
            Assert.AreEqual(3, (result.Portfolio[0] as Stock).Price);
            Assert.AreEqual(300, (result.Portfolio[0] as Stock).Shares);
        }

        [TestMethod()]
        public void Should_amount_is_1200_When_euro_consolidate()
        {
            var assetPortfolio = new AssetPortfolio(null);
            assetPortfolio.Add(new Cash(1000, Currency.EUR));
            assetPortfolio.Add(new Cash(200, Currency.EUR));

            var result = assetPortfolio.Consolidate();

            Assert.AreEqual(1, result.Portfolio.Count);
            Assert.AreEqual(Currency.EUR, (result.Portfolio[0] as Cash).Currency);
            Assert.AreEqual(1200, (result.Portfolio[0] as Cash).GetValue());
        }

        [TestMethod()]
        public void Should_price_is_3_and_shares_is_300_amount_is_1200_x_When_euro_and_abc_stock_consolidate()
        {
            var assetPortfolio = new AssetPortfolio(null);
            assetPortfolio.Add(new Stock("ABC", 100, 2, Currency.USD));
            assetPortfolio.Add(new Stock("ABC", 200, 3.5, Currency.USD));
            assetPortfolio.Add(new Cash(1000, Currency.EUR));
            assetPortfolio.Add(new Cash(200, Currency.EUR));

            var result = assetPortfolio.Consolidate();

            Assert.AreEqual(2, result.Portfolio.Count);
            Assert.AreEqual(3, (result.Portfolio[0] as Stock).Price);
            Assert.AreEqual(300, (result.Portfolio[0] as Stock).Shares);
            Assert.AreEqual(Currency.EUR, (result.Portfolio[1] as Cash).Currency);
            Assert.AreEqual(1200, (result.Portfolio[1] as Cash).GetValue());
        }

        [TestMethod()]
        public async Task Should_3940_When_call_value_for_cash_and_stocks()
        {
            Mock<IExchangeRateService> mockExchangeRateService = new Mock<IExchangeRateService>();
            mockExchangeRateService.Setup(x => x.GetRateAsync(It.IsAny<Currency>(), It.IsAny<Currency>())).Returns(Task.FromResult(2.0));

            var assetPortfolio = new AssetPortfolio(mockExchangeRateService.Object);
            assetPortfolio.Add(new Stock("ABC", 100, 5, Currency.USD));
            assetPortfolio.Add(new Stock("ABC", 200, 2, Currency.USD));
            assetPortfolio.Add(new Cash(1000, Currency.EUR));
            assetPortfolio.Add(new Cash(70, Currency.EUR));

            var result = await assetPortfolio.ValueAsync(Currency.AUD);

            Assert.AreEqual(3940, result);
        }
    }
}