using Investment.Portfolio.Domain.Abstractions.Exceptions;
using Investment.Portfolio.Domain.InvestmentPortfolio.Configs;
using Investment.Portfolio.Domain.InvestmentPortfolio.Entities;
using Investment.Portfolio.Domain.InvestmentPortfolio.Enums;
using Investment.Portfolio.Domain.InvestmentPortfolio.Repositories;
using Investment.Portfolio.Domain.InvestmentPortfolio.Service;
using Investment.Portfolio.Domain.YahooFinance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investment.Portfolio.Domain.InvestmentPortfolio.Services
{
    public class OperationService : IOperationService
    {
        private IOperationRepository _operationRepository;
        private IYahooFinanceService _yahooFinanceService;
        private IStockService _stockService;
        private OperationConfig _config;
        public OperationService(IOperationRepository operationRepository,
                                IYahooFinanceService yahooFinanceService, 
                                IStockService stockService, 
                                OperationConfig config)
        {
            _operationRepository = operationRepository;
            _yahooFinanceService = yahooFinanceService;
            _stockService = stockService;
            _config = config;
        }

        public async Task<Operation> SaveOperation(string ticker, int quantity, OperationType operationType)
        {
            try
            {
                var stock = await _stockService.GetStockByTicker(ticker);

                if (stock is null)
                    throw new OperationServiceException("Stock not found");

                var response = await _yahooFinanceService.QueryQuotePrice(stock.Ticker);

                if (response is null)
                    throw new OperationServiceException("Stock not found");

                var marketPrice = response.QuoteResponse.Result.FirstOrDefault().RegularMarketPrice;

                var totalAmount = _config.PurchaseBrokerageCost + ((marketPrice * quantity) * (_config.FeesPercentage + 100)) / 100;

                var operation = new Operation
                {
                    Id = Guid.NewGuid(),
                    StockId = stock.Id,
                    Stock = stock,
                    OperationType = operationType,
                    Price = marketPrice,
                    Quantity = quantity,
                    TotalAmount = totalAmount
                };

                await _operationRepository.SaveAsync(operation);
                return operation;
            }
            catch (Exception e)
            {
                throw new OperationServiceException("Error on save Operation", e);
            }
        }

        public async Task<IEnumerable<Operation>> GetOperations(string ticker)
        {
            try
            {
                var stock = await _stockService.GetStockByTicker(ticker);

                if (stock is null)
                    throw new OperationServiceException("Stock not found");

                return await _operationRepository.GetOperationsByStockIdAsync(stock.Id);
            }
            catch (Exception e)
            {
                throw new OperationServiceException("Error on get Operations", e);
            }
        }
    }
}
