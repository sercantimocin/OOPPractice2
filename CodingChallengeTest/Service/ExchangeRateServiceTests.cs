using CodingChallenge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CodingChallenge.Service.Tests
{
    [TestClass()]
    public class ExchangeRateServiceTests
    {
        [TestMethod()]
        public async Task Should_greater_than_zero_When_from_is_same_base_currency()
        {
            var exchangeRateService = new ExchangeRateService();

            var sut = await exchangeRateService.GetRateAsync(Currency.EUR, Currency.USD);
            Assert.IsTrue(sut > 0);
        }

        [TestMethod()]
        public async Task Should_greater_than_zero_When_to_is_same_base_currency()
        {
            var exchangeRateService = new ExchangeRateService();

            var sut = await exchangeRateService.GetRateAsync(Currency.USD, Currency.EUR);
            Assert.IsTrue(sut > 0);
        }

        [TestMethod()]
        public async Task Should_greater_than_zero_When_from_and_to_are_different_base_currency()
        {
            var exchangeRateService = new ExchangeRateService();

            var sut = await exchangeRateService.GetRateAsync(Currency.TRY, Currency.GBP);
            Assert.IsTrue(sut > 0);
        }
    }
}