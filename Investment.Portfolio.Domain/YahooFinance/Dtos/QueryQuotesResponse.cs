
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Investment.Portfolio.Domain.YahooFinance.Dtos
{
    public class QueryQuotesResponse
    {
        [JsonPropertyName("quotes")]
        public List<Quote> Quotes { get; set; }
    }

    public class Quote
    {
        [JsonPropertyName("exchange")]
        public string Exchange { get; set; }

        [JsonPropertyName("shortname")]
        public string Shortname { get; set; }

        [JsonPropertyName("quoteType")]
        public string QuoteType { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("index")]
        public string Index { get; set; }

        [JsonPropertyName("score")]
        public double Score { get; set; }

        [JsonPropertyName("typeDisp")]
        public string TypeDisp { get; set; }

        [JsonPropertyName("longname")]
        public string Longname { get; set; }

        [JsonPropertyName("exchDisp")]
        public string ExchDisp { get; set; }

        [JsonPropertyName("isYahooFinance")]
        public bool IsYahooFinance { get; set; }
    }
}
