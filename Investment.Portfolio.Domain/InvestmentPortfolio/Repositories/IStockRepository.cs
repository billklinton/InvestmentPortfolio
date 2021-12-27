
using Investment.Portfolio.Domain.InvestmentPortfolio.Entities;
using System.Threading.Tasks;

namespace Investment.Portfolio.Domain.InvestmentPortfolio.Repositories
{
    public interface IStockRepository
    {
        Task SaveAsync(Stock stock);
        Task<Stock> GetByTicker(string ticker);
    }
}
