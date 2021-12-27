using Investment.Portfolio.Domain.InvestmentPortfolio.Entities;
using Investment.Portfolio.Domain.InvestmentPortfolio.Repositories;
using Investment.Portfolio.Infra.Data.Repositories.Base;
using Investment.Portfolio.Infra.Data.SqlQueries;
using System.Threading.Tasks;

namespace Investment.Portfolio.Infra.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        private IBaseRepository _baseRepository;
        public StockRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }


        public async Task SaveAsync(Stock stock)
        {
            await _baseRepository.ExecuteAsync(StockSql.Insert, stock);
        }

        public async Task<Stock> GetByTicker(string ticker)
        {
            var stock = await _baseRepository.QueryFirstOrDefaultAsync<Stock>(StockSql.GetByTicker, new { Ticker = ticker });
            return stock;
        }
    }
}
