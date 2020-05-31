using CodingChallenge.Models;
using CodingChallenge.Models.Assets;
using CodingChallenge.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodingChallenge.Models
{
    public class AssetPortfolio
    {
        private List<IAsset> _portfolio;
        private ReaderWriterLockSlim _locker;
        private IExchangeRateService _exchangeRateService;

        public AssetPortfolio(IExchangeRateService exchangeRateService)
        {
            _locker = new ReaderWriterLockSlim();
            _portfolio = new List<IAsset>();
            _exchangeRateService = exchangeRateService;
        }

        public IReadOnlyList<IAsset> Portfolio
        {
            get
            {
                _locker.EnterReadLock();
                try
                {
                    return _portfolio.AsReadOnly();
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
        }

        public void Add(IAsset asset)
        {
            _locker.EnterWriteLock();
            _portfolio.Add(asset);
            _locker.ExitWriteLock();
        }

        public double Value()
        {
            return Portfolio.Sum(x => x.GetValue());
        }

        public async Task<double> ValueAsync(Currency currency = Currency.EUR)
        {
            double value = 0;
            IReadOnlyList<IAsset> portfolio = Portfolio;
            foreach (var asset in portfolio)
            {
                value += asset.GetValue() * await _exchangeRateService.GetRateAsync(asset.Currency, currency);
            }
            return value;
        }

        public AssetPortfolio Consolidate()
        {
            _locker.EnterUpgradeableReadLock();
            try
            {
                Dictionary<string, IAsset> consolidatedAssets = new Dictionary<string, IAsset>();

                foreach (var asset in _portfolio)
                {
                    Consolidation(consolidatedAssets, asset);
                }

                _locker.EnterWriteLock();
                _portfolio.Clear();
                _portfolio.AddRange(consolidatedAssets.Values);
            }
            finally
            {
                _locker.ExitWriteLock();
                _locker.ExitUpgradeableReadLock();
            }

            return this;
        }

        private void Consolidation(Dictionary<string, IAsset> consolidatedAssets, IAsset asset)
        {
            if (consolidatedAssets.ContainsKey(asset.Key))
            {
                IAsset consolidatedAsset = asset.Consolidate(consolidatedAssets[asset.Key]);

                consolidatedAssets.Remove(asset.Key);
                consolidatedAssets.Add(asset.Key, consolidatedAsset);
            }
            else
            {
                consolidatedAssets.Add(asset.Key, asset);
            }
        }

        ~AssetPortfolio()
        {
            _locker.Dispose();
        }
    }
}