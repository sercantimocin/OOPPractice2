using CodingChallenge.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CodingChallenge.Service
{
    public class ExchangeRateService : IExchangeRateService
    {
        private HttpClient _client;
        private string URL = "https://api.exchangeratesapi.io/latest";

        public ExchangeRateService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<double> GetRateAsync(Currency fromCurrency, Currency toCurrency)
        {
            ExchangeRateResponse response = await GetExchangeRateResponseAsync();

            CheckExchangeRateExists(response, fromCurrency);
            CheckExchangeRateExists(response, toCurrency);

            if (response.Base.Equals(fromCurrency))
            {
                return response.Rates[toCurrency];
            }

            if (response.Base.Equals(toCurrency))
            {
                return 1 / response.Rates[fromCurrency];
            }

            return response.Rates[fromCurrency] / response.Rates[toCurrency];
        }

        private void CheckExchangeRateExists(ExchangeRateResponse exchangeRateResponse, Currency exchangeRate)
        {
            if (exchangeRateResponse.Base.Equals(exchangeRate) == false && exchangeRateResponse.Rates.ContainsKey(exchangeRate) == false)
            {
                throw new Exception("Cannot have exchange rates");
            }
        }

        private async Task<ExchangeRateResponse> GetExchangeRateResponseAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(URL);
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Cannot reach exchange rates");
            }

            return await response.Content.ReadAsAsync<ExchangeRateResponse>();
        }

        ~ExchangeRateService()
        {
            _client.Dispose();
        }
    }
}
