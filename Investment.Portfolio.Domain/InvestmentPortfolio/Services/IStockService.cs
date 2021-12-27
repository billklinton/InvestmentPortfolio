using Investment.Portfolio.Domain.InvestmentPortfolio.Dtos;
using System.Threading.Tasks;

namespace Investment.Portfolio.Domain.InvestmentPortfolio.Service
{
    public interface IStockService
    {
        Task<QuotesResponse> SearchStockQuotesPrice(string ticker);
        Task<Entities.Stock> SaveStock(Entities.Stock stock);
        Task<Entities.Stock> GetStockByTicker(string ticker);
    }
}
