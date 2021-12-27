using Investment.Portfolio.Domain.InvestmentPortfolio.Entities;
using Investment.Portfolio.Domain.InvestmentPortfolio.Repositories;
using Investment.Portfolio.Infra.Data.Repositories.Base;
using Investment.Portfolio.Infra.Data.SqlQueries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investment.Portfolio.Infra.Data.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private IBaseRepository _baseRepository;
        public OperationRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task SaveAsync(Operation operation)
        {
            await _baseRepository.ExecuteAsync(OperationSql.Insert, operation);
        }

        public async Task<IEnumerable<Operation>> GetOperationsByStockIdAsync(Guid stockId)
        {
            return await _baseRepository.QueryAsync<Operation>(OperationSql.GetOperationsByStockId, new { StockId = stockId });
        }
    }
}
