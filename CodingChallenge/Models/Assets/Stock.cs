using CodingChallenge.Models;
using CodingChallenge.Models.Assets;

namespace CodingChallenge
{
    public interface IStock
    {
        string Symbol { get; }
        double Shares { get; }
        double Price { get; }
    }

    public class Stock : IAsset, IStock
    {
        public Stock(string symbol, double shares, double price, Currency currency)
        {
            Key = symbol;
            Symbol = symbol;
            Shares = shares;
            Price = price;
            Currency = currency;
        }

        public string Key { get; }

        public Currency Currency { get; }

        public string Symbol { get; }
        public double Shares { get; }
        public double Price { get; }

        public double GetValue()
        {
            return Shares * Price;
        }

        public IAsset Consolidate(IAsset asset)
        {
            Stock otherStock = asset as Stock;

            // If Key is same, Currency should same according to the real life scenios
            if (Key != otherStock.Key)
            {
                return null;
            }

            double totalValue = GetValue() + otherStock.GetValue();
            double totalShares = Shares + otherStock.Shares;
            double newPrice = totalValue / totalShares;

            return new Stock(Symbol, totalShares, newPrice, Currency);
        }
    }
}