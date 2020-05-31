using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingChallenge.Models.Assets.Tests
{
    [TestClass()]
    public class CashTests
    {
        [TestMethod()]
        public void Should_null_When_currency_are_not_same_consolidate()
        {
            var cash1 = new Cash(1000, Currency.EUR);
            var cash2 = new Cash(200, Currency.GBP);

            var sut = cash1.Consolidate(cash2);

            Assert.IsNull(sut);
        }

        [TestMethod()]
        public void Should_amount_is_1200_When_euro_consolidate()
        {

            var cash1 = new Cash(1000, Currency.EUR);
            var cash2 = new Cash(200, Currency.EUR);

            var sut = cash1.Consolidate(cash2);

            Assert.AreEqual(Currency.EUR, sut.Currency);
            Assert.AreEqual(1200, sut.GetValue());
        }
    }
}