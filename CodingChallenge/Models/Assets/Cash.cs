namespace CodingChallenge.Models.Assets
{
    public class Cash : IAsset
    {
        private readonly double _amount;

        public Cash(double amount, Currency currency)
        {
            Key = currency.ToString();
            _amount = amount;
            Currency = currency;
        }

        public string Key { get; }

        public Currency Currency { get; }

        public double GetValue()
        {
            return _amount;
        }

        public IAsset Consolidate(IAsset asset)
        {
            var otherCash = asset as Cash;

            if (Key != otherCash.Key)
            {
                return null;
            }

            double totalAmount = otherCash.GetValue() + GetValue();
            return new Cash(totalAmount, Currency);
        }
    }
}
