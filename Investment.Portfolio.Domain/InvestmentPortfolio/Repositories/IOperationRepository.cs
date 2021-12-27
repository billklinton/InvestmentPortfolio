
using Investment.Portfolio.Domain.InvestmentPortfolio.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investment.Portfolio.Domain.InvestmentPortfolio.Repositories
{
    public interface IOperationRepository
    {
        Task SaveAsync(Operation operation);
        Task<IEnumerable<Operation>> GetOperationsByStockIdAsync(Guid stockId);
    }
}
