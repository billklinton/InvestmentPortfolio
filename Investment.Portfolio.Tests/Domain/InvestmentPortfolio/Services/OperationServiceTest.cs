using Investment.Portfolio.Domain.Abstractions.Exceptions;
using Investment.Portfolio.Domain.InvestmentPortfolio.Configs;
using Investment.Portfolio.Domain.InvestmentPortfolio.Entities;
using Investment.Portfolio.Domain.InvestmentPortfolio.Enums;
using Investment.Portfolio.Domain.InvestmentPortfolio.Repositories;
using Investment.Portfolio.Domain.InvestmentPortfolio.Service;
using Investment.Portfolio.Domain.InvestmentPortfolio.Services;
using Investment.Portfolio.Domain.YahooFinance.Dtos;
using Investment.Portfolio.Domain.YahooFinance.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Investment.Portfolio.Tests.Domain.InvestmentPortfolio.Services
{
    public class OperationServiceTest
    {
        public IOperationService _operationService;
        private Mock<IOperationRepository> _operationRepositoryMock;
        private Mock<IYahooFinanceService> _yahooFinanceServiceMock;
        private Mock<IStockService> _stockServiceMock;
        private Mock<OperationConfig> _configMock;

        public OperationServiceTest()
        {
            _operationRepositoryMock = new Mock<IOperationRepository>(MockBehavior.Default);
            _yahooFinanceServiceMock = new Mock<IYahooFinanceService>(MockBehavior.Strict);
            _stockServiceMock = new Mock<IStockService>(MockBehavior.Strict);
            _configMock = new Mock<OperationConfig>(MockBehavior.Strict);
            _operationService = new OperationService(_operationRepositoryMock.Object, 
                                                     _yahooFinanceServiceMock.Object, 
                                                     _stockServiceMock.Object, 
                                                     _configMock.Object);
        }

        [Fact]
        public void WhenSaveOperationShouldThrowNotFoundStockException()
        {
            //arrange
            var operation = new Operation {
                Stock = new Stock { Ticker = "test"}
            };
            _stockServiceMock.Setup(r => r.GetStockByTicker(operation.Stock.Ticker)).Returns(Task.FromResult<Stock>(null));

            //act
            var task = _operationService.SaveOperation(operation.Stock.Ticker, 2, OperationType.Buy);

            //assert
            Assert.ThrowsAsync<OperationServiceException>(() => task);
        }

        [Fact]
        public async void WhenSaveOperationShouldReturnOk()
        {
            //arrange
            var stock = new Stock { Ticker = "test", CorporateName = "Teste", Id = Guid.NewGuid() };
            var operation = new Operation
            {
                Stock = stock
            };
            var queryQuotePriceResponse = new QueryQuotePriceResponse
            {
                QuoteResponse = new QuoteResponse
                {
                    Result = new List<Result>
                    {
                        new Result
                        {
                            RegularMarketPrice = 40
                        }
                    }
                }
            };
            _stockServiceMock.Setup(r => r.GetStockByTicker(operation.Stock.Ticker)).Returns(Task.FromResult<Stock>(stock));
            _yahooFinanceServiceMock.Setup(r => r.QueryQuotePrice(stock.Ticker)).Returns(Task.FromResult<QueryQuotePriceResponse>(queryQuotePriceResponse));
            _operationRepositoryMock.Setup(r => r.SaveAsync(operation));

            //act
            var operationResponse = await _operationService.SaveOperation(operation.Stock.Ticker, 2, OperationType.Buy);

            //assert
            Assert.Equal(stock.Ticker, operationResponse.Stock.Ticker);
        }

        [Fact]
        public void WhenGetOperationsShouldThrowNotFoundStockException()
        {
            //arrange
            var operation = new Operation
            {
                Stock = new Stock { Ticker = "test" }
            };
            _stockServiceMock.Setup(r => r.GetStockByTicker(operation.Stock.Ticker)).Returns(Task.FromResult<Stock>(null));

            //act
            var task = _operationService.GetOperations(operation.Stock.Ticker);

            //assert
            Assert.ThrowsAsync<OperationServiceException>(() => task);
        }

        [Fact]
        public async void WhenGetOperationsShouldReturnOk()
        {
            //arrange
            var stock = new Stock { Ticker = "test", CorporateName = "Teste", Id = Guid.NewGuid() };
            var operations = new List<Operation>()
            {
                new Operation
                {

                },
                new Operation
                {

                }
            };
            _stockServiceMock.Setup(r => r.GetStockByTicker(stock.Ticker)).Returns(Task.FromResult<Stock>(stock));
            _operationRepositoryMock.Setup(r => r.GetOperationsByStockIdAsync(stock.Id)).Returns(Task.FromResult<IEnumerable<Operation>>(operations));

            //act
            var operationsResponse = await _operationService.GetOperations(stock.Ticker);

            //assert
            Assert.Equal(2, operationsResponse.Count());
        }
    }
}
