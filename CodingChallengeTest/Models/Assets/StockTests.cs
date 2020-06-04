using CodingChallenge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingChallenge.Tests
{
    [TestClass()]
    public class StockTests
    {
        [TestMethod()]
        public void Should_null_When_keys_are_not_same_consolidate()
        {
            var stock1 = new Stock("ABC", 100, 2, Currency.USD);
            var stock2 = new Stock("ACC", 200, 3.5, Currency.USD);

            var result = stock1.Consolidate(stock2);

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void Should_price_is_3_and_shares_is_300_When_keys_are_same_consolidate()
        {
            var stock1 = new Stock("ABC", 100, 2, Currency.USD);
            var stock2 = new Stock("ABC", 200, 3.5, Currency.USD);

            var result = stock1.Consolidate(stock2);

            Assert.AreEqual(900, result.GetValue());
            Assert.AreEqual(3, (result as Stock).Price);
            Assert.AreEqual(300, (result as Stock).Shares);
        }
    }
}