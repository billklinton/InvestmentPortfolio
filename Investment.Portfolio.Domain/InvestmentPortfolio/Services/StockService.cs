
using Investment.Portfolio.Domain.Abstractions.Exceptions;
using Investment.Portfolio.Domain.InvestmentPortfolio.Dtos;
using Investment.Portfolio.Domain.InvestmentPortfolio.Repositories;
using Investment.Portfolio.Domain.YahooFinance.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Investment.Portfolio.Domain.InvestmentPortfolio.Service
{
    public class StockService : IStockService
    {
        private IStockRepository _stockRepository;
        private IYahooFinanceService _yahooFinanceService;
        public StockService(IStockRepository stockRepository, IYahooFinanceService yahooFinanceService)
        {
            _stockRepository = stockRepository;
            _yahooFinanceService = yahooFinanceService;
        }


        public async Task<Entities.Stock> SaveStock(Entities.Stock stock)
        {
            try
            {
                var quotesResponse = await SearchStockQuotesPrice(stock.Ticker);
                
                if(quotesResponse.Quotes is null || !quotesResponse.Quotes.Any())
                    throw new StockServiceException($"Any stock was found with ticker {stock.Ticker}");

                stock.Id = Guid.NewGuid();
                stock.Ticker = quotesResponse.Quotes.FirstOrDefault().Stock.Ticker;

                if(await GetStockByTicker(stock.Ticker) != null)
                    throw new StockServiceException($"Stock for ticker: {stock.Ticker} already registered");

                await _stockRepository.SaveAsync(stock);
                return stock;
            }
            catch (Exception e)
            {
                throw new StockServiceException("Error on save Stock", e);
            }
        }

        public async Task<Entities.Stock> GetStockByTicker(string ticker)
        {
            try
            {
                var stock = await _stockRepository.GetByTicker(ticker);
                return stock;
            }
            catch (Exception e)
            {
                throw new StockServiceException("Erro on find stock", e);
            }
        }

        public async Task<QuotesResponse> SearchStockQuotesPrice(string ticker)
        {
            try
            {
                var quotesResponse = new QuotesResponse();
                var quotes = await _yahooFinanceService.QueryQuotes(ticker);
                foreach (var quote in quotes.Quotes)
                {
                    var quotePrice = await _yahooFinanceService.QueryQuotePrice(quote.Symbol);
                    quotesResponse.Quotes.Add(new StockQuote
                    {
                        Quote = new Quote
                        {
                            ConsultingDate = DateTime.Now,
                            Price = quotePrice.QuoteResponse.Result.FirstOrDefault().RegularMarketPrice
                        },
                        Stock = new Stock
                        {
                            Ticker = quote.Symbol,
                            Name = quote.Shortname,
                            FullName = quote.Longname
                        }
                    });
                }

                return quotesResponse;
            }
            catch (Exception e)
            {
                throw new StockServiceException("Error on finding StockQuotesPrice", e);
            }
        }
    }
}
