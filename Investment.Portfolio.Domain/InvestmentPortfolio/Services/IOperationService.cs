using Investment.Portfolio.Domain.InvestmentPortfolio.Entities;
using Investment.Portfolio.Domain.InvestmentPortfolio.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investment.Portfolio.Domain.InvestmentPortfolio.Services
{
    public interface IOperationService
    {
        Task<Operation> SaveOperation(string ticker, int quantity, OperationType operationType);
        Task<IEnumerable<Operation>> GetOperations(string ticker);
    }
}
