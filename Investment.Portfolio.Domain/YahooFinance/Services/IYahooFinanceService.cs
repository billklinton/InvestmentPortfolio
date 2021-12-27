using Investment.Portfolio.Domain.YahooFinance.Dtos;
using System.Threading.Tasks;

namespace Investment.Portfolio.Domain.YahooFinance.Services
{
    public interface IYahooFinanceService
    {
        Task<QueryQuotesResponse> QueryQuotes(string searchText);
        Task<QueryQuotePriceResponse> QueryQuotePrice(string symbol);
    }
}
