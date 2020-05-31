using CodingChallenge.Models;
using System.Threading.Tasks;

namespace CodingChallenge.Service
{
    public interface IExchangeRateService
    {
        Task<double> GetRateAsync(Currency fromCurrency, Currency toCurrency);
    }
}
