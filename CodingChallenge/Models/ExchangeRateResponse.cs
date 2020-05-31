using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CodingChallenge.Models
{
    public partial class ExchangeRateResponse
    {
        [JsonProperty("rates")]
        public Dictionary<Currency, double> Rates { get; set; }

        [JsonProperty("base")]
        public Currency Base { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }
    }
}
