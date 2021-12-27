using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Investment.Portfolio.Domain.YahooFinance.Dtos
{
    public class QueryQuotePriceResponse
    {
        [JsonPropertyName("quoteResponse")]
        public QuoteResponse QuoteResponse { get; set; }
    }

    public class QuoteResponse
    {
        [JsonPropertyName("result")]
        public List<Result> Result { get; set; }

        [JsonPropertyName("error")]
        public object Error { get; set; }
    }

    public class Result
    {

        [JsonPropertyName("regularMarketPrice")]
        public double RegularMarketPrice { get; set; }
       
    }
}
