using System.Collections.Generic;

namespace Investment.Portfolio.Domain.InvestmentPortfolio.Dtos
{
    public class QuotesResponse
    {
        public QuotesResponse()
        {
            Quotes = new List<StockQuote>();
        }
        public List<StockQuote> Quotes { get; set; }
    }
}
